using Combats.Actions;
using Combats.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combats.Locations
{
    class MainLocation : BaseLocation, ILocation
    {
        private NameLocationEnum name = NameLocationEnum.Main;

        private string url = "http://dreamscity.combats.com/main.pl";

        private Bot Person { get; set; }

        public MainLocation(Bot person)
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
            switch (actionName)
            {
                case ActionsEnum.Main:
                    return new { version = "3.6.8" };
                default:
                    return new { };
            }

        }

        public void Execute(ActionsEnum actionName)
        {
            var resultJson = this.Person.engine.Call(this, ActionsEnum.Main);
            var responseJson = JsonConvert.DeserializeObject<ResponseDto>(resultJson);


 
            this.Person.maxmana = responseJson.data.user.maxmana;
            this.Person.HP = responseJson.data.user.HP;
            this.Person.maxHP = responseJson.data.user.maxHP;
        }

    }
    }
