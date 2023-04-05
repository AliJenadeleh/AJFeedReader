namespace AJFeedReader.Models
{
    public class FeedSource : IFeedSource
    {
        public string SourceLink { get; set; } = string.Empty;

        public string SiteName { get; set; } = string.Empty;

        public string SiteUrl { get; set; } = string.Empty;
    }
}
