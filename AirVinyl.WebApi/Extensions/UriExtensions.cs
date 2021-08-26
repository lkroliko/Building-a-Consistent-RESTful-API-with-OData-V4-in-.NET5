using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AirVinyl.WebApi.Extensions
{
    public static class UriExtensions
    {
        public static string GetOdataKey(this Uri link)
        {
            var match = Regex.Match(link.Segments.Last(), "\\(\\d*\\)$");
            return match.Value.Substring(1, match.Value.Length - 2);
        }

        public static int GetOdataIntKey(this Uri link)
        {
            return int.Parse(link.GetOdataKey());
        }
    }
}
