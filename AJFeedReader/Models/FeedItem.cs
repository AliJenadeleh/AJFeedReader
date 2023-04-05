using System;
using System.Xml.Linq;

using AJFeedReader.Helpers;

namespace AJFeedReader.Models
{

    [Serializable]
    public class FeedItem
    {

        //----------------------------------------------
        // Fields
        //----------------------------------------------
        public string SiteLink { get; set; }
        public string SiteName { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? PubDate { get; set; }
        //----------------------------------------------

        //----------------------------------------------
        // Methods
        //----------------------------------------------

        public FeedItem() { }
        public FeedItem(XElement xelement, string SiteLink = "")
        {
            try
            {
                this.PubDate = DateTime.Parse(xelement.Element("pubDate")?.Value);
            }
            catch { }

            this.Category = xelement.Element(Tags.Category)?.Value;
            this.SiteLink = SiteLink;
            this.Title = xelement.Element(Tags.Title)?.Value;
            this.Link = xelement.Element(Tags.Link)?.Value;
            this.Description = xelement.Element(Tags.Description)?.Value.HtmlDecode();

            ValidateImageSRC(xelement);
        }

        //----------------------------------------------
        // 
        //----------------------------------------------

        private void ValidateImageSRC(XElement xelement)
        {
            var enclosure = xelement.Element(Tags.Enclosure);
            if (enclosure != null)
            {
                string url = enclosure.Attribute(Tags.UrlATR)?.Value;
                if (!string.IsNullOrEmpty(url))
                {
                    ImgPath = url;
                    return;
                }
            }

            int start, end;
            start = Description.IndexOf("src=");
            if (start > 0)
            {
                start += 5;
                end = Description.IndexOf("\"", start);
                if (end > 0)
                {
                    ImgPath = Description.Substring(start, end - 1);
                    if (ImgPath.IndexOf(" ") > 0)
                    {
                        ImgPath = ImgPath.Split(' ')[0].Replace("\"", "");
                    }
                }

            }
        }

        //----------------------------------------------
        // 
        //----------------------------------------------

        public string GetPubDate
        {
            get
            {
                try
                {
                    DateTime d = ((DateTime)PubDate);
                    //var ary = d.ToShortDateString().Split('/');
                    return d.ToString("yyyy-mm-ddTHH:mm:ss");
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public string ImgPath { get; set; }
    }

    //----------------------------------------------
    // Extender Class
    //----------------------------------------------

    public static class FeedItemExtender
    {
        private static string ClearTags(string Content)
        {
            int start, end;
            string content = Content;

            try
            {
                while ((start = content.IndexOf("<")) >= 0)
                {
                    end = content.IndexOf(">", start + 1);
                    content = content.Remove(start, (end - start) + 1);
                }
            }
            catch { }

            return ClearAnds(content);
        }


        private static string ClearAnds(string Content)
        {
            string content = Content;
            if (Content.Contains("&"))
            {
                int start, end;
                try
                {
                    while ((start = content.IndexOf("&")) >= 0)
                    {
                        end = content.IndexOf(";", start + 1);
                        content = content.Remove(start, (end - start) + 1);
                    }
                }
                catch { }

            }
            return content;
        }

        private static string Clear(string Content)
        {
            if (Content.Contains("<"))
                return ClearTags(Content);

            return ClearAnds(Content);
        }

        public static string SafeDescription(this FeedItem item)
                                                => Clear(item.Description);

        public static bool HasImage(this FeedItem item)
                            => !string.IsNullOrEmpty(item.ImgPath);
    }//{}

}