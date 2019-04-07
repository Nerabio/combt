using Combats.Actions;
using Combats.Dto;
using Combats.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Combats
{
    public class Bot
    {
        public string username = "Abio";

        public string password = "1q2w3e4r5t";

        public string my_id { get; set; }

        public string in_battle { get; set; }
        public string maxmana { get; set; }
        public string HP { get; set; }
        public string maxHP { get; set; }

        public Cookies Cookies { get; set; }

        public ServiceLocation serviceLocation;
        public Engine engine { get; set; }


        public void Init() {
            serviceLocation = new ServiceLocation(this);
            engine = new Engine(this);
        }



        public void GotoZayavka()
        {
            var location = serviceLocation.Locations.First(l => l.GetName() == NameLocationEnum.Zayavka);
            location.Execute(ActionsEnum.AddZayavka);
        }

        public void GetStatus()
        {
            var location = serviceLocation.Locations.First(l => l.GetName() == NameLocationEnum.Main);
            location.Execute(ActionsEnum.Main);
        }

    }
}
