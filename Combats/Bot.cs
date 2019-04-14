using Combats.Actions;
using Combats.Dto;
using Combats.Locations;
using NLog;
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
        //public string username = "Abio";
        public string username =  "Akrom";

        public string password = "1q2w3e4r5t";

        public string my_id { get; set; }

        public bool InBattle { get; set; }

        public string maxmana { get; set; }
        public string HP { get; set; }
        public string maxHP { get; set; }

        public Cookies Cookies { get; set; }

        public ServiceLocation serviceLocation;
        public Engine engine { get; set; }
        public Logger Log { get; set; }

        public void Init() {
            this.Log = LogManager.GetCurrentClassLogger();
            serviceLocation = new ServiceLocation(this);
            engine = new Engine(this);
            this.Log.Info("Init bot");
        }

        public bool IsHealthFull()
        {
            if (this.HP == this.maxHP) return true;
            return false;
        }

        public void GotoZayavka()
        {
            var location = serviceLocation.Locations.First(l => l.GetName() == NameLocationEnum.Zayavka);
            location.Execute(ActionsEnum.AddZayavka);
            //location.Execute(ActionsEnum.AddZayavkaFiz);
        }

        public void GetStatus()
        {
            var location = serviceLocation.Locations.First(l => l.GetName() == NameLocationEnum.Main);
            location.Execute(ActionsEnum.Main);
        }

        public void Kick()
        {
            var location = serviceLocation.Locations.First(l => l.GetName() == NameLocationEnum.Battle);
            location.Execute(ActionsEnum.Kick);
        }

    }
}
