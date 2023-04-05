namespace AJFeedReader.Models
{
    public interface IFeedSource
    {

        public string SourceLink { get; }

        public string SiteName { get; }

        public string SiteUrl { get; }

    }
}
