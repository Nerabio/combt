using Combats.Actions;
using Combats.Dto;
using Combats.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats.Locations
{
    class ZayavkaLocation : BaseLocation, ILocation
    {
        private NameLocationEnum name = NameLocationEnum.Zayavka;

        private string url = "http://dreamscity.combats.com/zayavka.pl";

        private Bot Person { get; set; }

        public ZayavkaLocation(Bot person)
        {
            this.Person = person;
        }

        public string GetUrl()
        {
            return this.url;
        }

        public NameLocationEnum GetName()
        {
            return this.name;
        }

        public object GetData(ActionsEnum actionName)
        {
            switch (actionName) {
                case ActionsEnum.GetMyIdZayavka:
                    return new { level = "fiz"  , version = "3.6.8" };
                case ActionsEnum.AddZayavka:
                    return new { level = "fiz", open = 1, version = "3.6.8", my_id = this.Person.my_id };
                default:
                    return new { };                   
            }           
        }

        public void Execute(ActionsEnum actionName)
        {
            var resultJson = this.Person.engine.Call(this, ActionsEnum.GetMyIdZayavka);
            var responseJson = JsonConvert.DeserializeObject<ResponseDto>(resultJson);
            this.Person.my_id = responseJson.data.my_id;
            resultJson = this.Person.engine.Call(this, ActionsEnum.AddZayavka);
        }

    }
}
