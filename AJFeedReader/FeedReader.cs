using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

using AJFeedReader.Models;

namespace AJFeedReader
{

    public class FeedReader
    {
        private List<FeedItem> items;

        public FeedReader()
        {
            Clear();
        }

        public void Clear()
        {
            items = new List<FeedItem>();
        }

        private void ProccessContent(string Content, string SiteLink)
        {
            var xd = XDocument.Parse(Content);

            var items = from i in xd.Descendants("item")
                        select new FeedItem(i, SiteLink);

            foreach (var i in items)
                this.items.Add(i);
        }

        private void Download(string source)
        {
            var wr = WebRequest.Create(source);
            var stream = wr.GetResponse().GetResponseStream();
            var sr = new StreamReader(stream);
            ProccessContent(sr.ReadToEnd(), source);
        }

        public IEnumerable<FeedItem> GetFeeds(IEnumerable<string> sources)
        {
            if (sources.Any())
            {
                foreach (var s in sources)
                    try
                    {
                        Download(s);
                    }
                    catch { }

                return items.OrderByDescending(i => i.PubDate);
            }

            return null;
        }

        private async Task ProccessContentAsync(string Content, string SiteLink)
        {
            await Task.Run(() =>
            {
                ProccessContent(Content, SiteLink);
            });
        }

        private async Task DownloadAsync(string source)
        {
            var wr = WebRequest.Create(source);
            var stream = (await wr.GetResponseAsync()).GetResponseStream();
            var sr = new StreamReader(stream);
            await ProccessContentAsync(await sr.ReadToEndAsync(), source);
        }

        public async Task<IEnumerable<FeedItem>> GetFeedsAsync(IEnumerable<string> sources)
        {
            if (sources.Any())
            {
                foreach (var s in sources)
                    try
                    {
                        await DownloadAsync(s);
                    }
                    catch { }

                return items.OrderByDescending(o => o.PubDate);
            }

            return null;
        }
    }
}