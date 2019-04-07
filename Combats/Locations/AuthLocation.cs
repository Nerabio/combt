using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats.Locations
{
    public class AuthLocation : BaseLocation
    {
        public NameLocationEnum Name = NameLocationEnum.Auth;
        public string Url = "capitalcity.combats.com/enter.pl";
    }
}
