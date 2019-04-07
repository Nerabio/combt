using Combats.Actions;
using Combats.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats.Locations
{
    public interface ILocation
    {
        string GetUrl();

        NameLocationEnum GetName();

        object GetData(ActionsEnum actionName);

        void Execute(ActionsEnum actionName);
    }
}
