using Combats.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats
{
    public class ServiceLocation
    {
        public IList<ILocation> Locations { get; set; }
        private Bot Person { get; set; }
        public ServiceLocation(Bot person)
        {
            this.Person = person;
            IList<ILocation> locationList = new List<ILocation>();

            locationList.Add(new ZayavkaLocation(Person));
            locationList.Add(new MainLocation(Person));
            locationList.Add(new BattleLocation(Person));

            this.Locations = locationList;
        }
 
    }
}
