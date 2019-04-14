using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats.Dto
{
    public class Data
    {
        public string hour { get; set; }
        public string netname { get; set; }
        public string min { get; set; }
        public object lich_talk { get; set; }
        public string friends { get; set; }
        public string is_angel { get; set; }
        public string login { get; set; }
        public string map { get; set; }
        public string do_ping { get; set; }
        public string sec { get; set; }
        public string transfer { get; set; }
        public string full_size { get; set; }
        public string is_admin { get; set; }
        public string sd4 { get; set; }
        public string version { get; set; }
        public string my_id { get; set; }


        public int? id { get; set; }
        public int? in_battle { get; set; }
        public string maxmana { get; set; }
        public string HP { get; set; }
        public string maxHP { get; set; }


        public User user { get; set; }

        public string stop { get; set; }
        public string error { get; set; }
        
    }
}
