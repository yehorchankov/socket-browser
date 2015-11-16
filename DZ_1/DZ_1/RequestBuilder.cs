using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DZ_1
{
    public class RequestBuilder : IRequestBuilder
    {
        private readonly Request _request = new Request();

        public Request GetResult()
        {
            return _request;
        }

        public void CreateStartingLine(string verb, string uri, string httpVersion = "1.1") //Some logic to check whether starting line characters are correct
        {
            if (String.IsNullOrEmpty(verb) || String.IsNullOrEmpty(uri))
                throw new ArgumentNullException("verb", "Verb or uri are incorrect");
            _request.Uri = new Uri(uri);
            Regex verbMatch = new Regex("[A-Z][A-Za-z]{2,5}");
            Regex versionMatch = new Regex("[0-2][.][0-9]");
            if (Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute) &&
                verbMatch.IsMatch(verb) &&
                versionMatch.IsMatch(httpVersion))
            {
                _request.StartingLine = String.Format("{0} {1} HTTP/{2}\r\n", verb, _request.Uri.PathAndQuery, httpVersion);
            }
            else 
                throw new ArgumentException("One or more parameters incorrect");
        }

        public void CreateHeader()
        {
            _request.Header = string.Format("Host: {0}\r\nConnection: close\r\nAccept: text/html" +
                              "\r\ntUser-Agent: MyBrowser\r\n\r\n", _request.Uri.Host);
        }

        public void CreateBody(string body)
        {
            if (body.Contains("<html>") && body.EndsWith("</html>"))
                _request.Body = body;
        }
    }
}
