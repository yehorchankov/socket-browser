using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_1
{
    public interface IRequestBuilder
    {
        Request GetResult();
        void CreateStartingLine(string verb, string uri, string httpVersion);
        void CreateHeader();
        void CreateBody(string body);
    }
}
