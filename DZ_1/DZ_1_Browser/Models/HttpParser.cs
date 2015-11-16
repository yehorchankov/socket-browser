using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script;

namespace DZ_1_Browser.Models
{
    public class HttpParser
    {
        public HttpStatusCode ResponseCode { get; private set; }
        public long ContentLength { get; private set; }
        public string Body { get; private set; }
        public string Title { get; private set; }
        
        public void ParseHttpResponse(string response)
        {
            //Trying to search for status code if exists
            Regex codeMatch = new Regex("[1-5][0-9]{2}");
            string[] tempStrings = response.Substring(0, 20).Split(' ');
            if (codeMatch.IsMatch(tempStrings[1]))
                ResponseCode = (HttpStatusCode) Enum.Parse(
                    typeof (HttpStatusCode), tempStrings[1]);

            //Trying to separate content from header
            int contentOffset = response.IndexOf("<html", StringComparison.Ordinal);
            Body = response.Substring(contentOffset);

            //Trying to read the page's title
            int titleOffset = Body.IndexOf("<title>", StringComparison.Ordinal);
            int titleEnd = Body.IndexOf("</title>", StringComparison.Ordinal);
            Title = Body.Substring(titleOffset + "<title>".Length, titleEnd - titleOffset - "<title>".Length);

            //Trying to search for content length
            tempStrings = response.Substring(0, 512).Split('\r', '\n');
            foreach (string s in tempStrings)
                if (s.StartsWith("Content-Length", true, CultureInfo.InvariantCulture))
                {
                    ContentLength = long.Parse(s.Split(' ')[1]);
                    break;
                }
            if (ContentLength <= 0)
                ContentLength = Body.Length;
        }
    }
}