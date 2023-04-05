// See https://aka.ms/new-console-template for more information

using AJFeedReader;

var sources = new List<string>()
{
    "https://www.tabnak.ir/fa/rss/22",
    "https://www.mehrnews.com/rss/tp/36"
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
