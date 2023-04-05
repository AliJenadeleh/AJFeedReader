using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using AJFeedReader.Models;

namespace AJFeedReader
{

    public class FeedReader
    {


        private string Download(IFeedSource source)
        {
            var wr = WebRequest.Create(source.SourceLink);
            var stream = wr.GetResponse().GetResponseStream();
            var sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }

        private async Task<string> DownloadAsync(IFeedSource source)
        {
            var wr = WebRequest.Create(source.SourceLink);
            var stream = (await wr.GetResponseAsync()).GetResponseStream();
            var sr = new StreamReader(stream);
            return await sr.ReadToEndAsync();
        }


        public IEnumerable<FeedItem> GetFeeds(IEnumerable<IFeedSource> sources)
        {
            List<FeedItem> items = new List<FeedItem>();
            if (sources.Any())
            {
                foreach (var s in sources)
                    try
                    {
                        var content = Download(s);
                        items.AddRange(FeedParser.Parse(content, s));
                    }
                    catch { }

                return items.OrderByDescending(i => i.PubDate);
            }

            return items;
        }


        public async Task<IEnumerable<FeedItem>> GetFeedsAsync(IEnumerable<IFeedSource> sources)
        {
            var items = new List<FeedItem>();

            if (sources.Any())
            {
                foreach (var s in sources)
                    try
                    {
                        var tmp = await DownloadAsync(s);
                        items.AddRange(FeedParser.Parse(tmp, s));
                    }
                    catch { }

                return items.OrderByDescending(o => o.PubDate);
            }

            return null;
        }

    }
}