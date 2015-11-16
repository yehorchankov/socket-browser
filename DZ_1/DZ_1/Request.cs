using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_1
{
    public class Request
    {
        public Uri Uri { get; set; }
        public string StartingLine { get; set; }
        public string Body { get; set; }
        public string Header { get; set; }

        public override string ToString()
        {
            return StartingLine + Header + Body;
        }
    }
}
