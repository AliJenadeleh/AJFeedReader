using System.Web;

namespace AJFeedReader.Helpers
{
    public static class HemlHelper
    {
        public static string HtmlDecode(this string value) => HttpUtility.HtmlDecode(value);
    }
}