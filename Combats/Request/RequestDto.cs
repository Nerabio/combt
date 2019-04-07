using Combats.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats.Request
{
    public class RequestDto
    {
        public int isJson = 1;
        public Cookies cookies { get; set; }
        public object data { get; set; }
    }
}
