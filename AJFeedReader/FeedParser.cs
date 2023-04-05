using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using AJFeedReader.Models;

namespace AJFeedReader
{
    public static class FeedParser
    {
        public static List<FeedItem> Parse(string Content, IFeedSource source)
        {
            var items = new List<FeedItem>();

            var xd = XDocument.Parse(Content);

            var tmpItems = from i in xd.Descendants("item")
                           select new FeedItem(i, source);

            foreach (var i in tmpItems)
                items.Add(i);

            return items;
        }

    }
}