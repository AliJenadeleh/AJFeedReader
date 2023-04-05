// See https://aka.ms/new-console-template for more information

using AJFeedReader;
using AJFeedReader.Models;

var sources = new List<IFeedSource>()
{
    new FeedSource()
    {
        SiteName = "tabnak",
        SiteUrl = "https://www.tabnak.ir",
        SourceLink = "https://www.tabnak.ir/fa/rss/22"
    },
    new FeedSource()
    {
        SiteName = "mehrnews",
        SiteUrl = "https://www.mehrnews.com/rss/tp/36",
        SourceLink = "https://www.mehrnews.com/rss/tp/36"
    }

};

var fr = new FeedReader();
var items = await fr.GetFeedsAsync(sources);

foreach (var item in items)
{
    Console.WriteLine("**************************************************************");

    Console.WriteLine($"Title : {item.Title}");

    Console.WriteLine($"Title : {item.Description}");

    Console.WriteLine($"Title : {item.ImgPath}");

    Console.WriteLine("**************************************************************");
}
