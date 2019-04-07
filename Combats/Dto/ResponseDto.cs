using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats.Dto
{
    public class ResponseDto
    {
        public string isJson { get; set; }
        public string img_base_URL { get; set; }
        public Cookies cookies { get; set; }
        public string destination { get; set; }
        public string form { get; set; }
        public object form_time { get; set; }
        public string cache_list { get; set; }
        public Data data { get; set; }
        public string base_URL { get; set; }
        public string redirect_URL { get; set; }
    }
}
