using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats.Request
{
    public class RequestData
    {
        public string login { get; set; }
        public string psw { get; set; }

        public string version = "3.6.8";

        public string os = "android";
    }
}
