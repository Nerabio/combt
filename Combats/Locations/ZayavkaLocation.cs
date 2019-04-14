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

        public void SetUrl(string url)
        {
            this.url = url;
        }

        public NameLocationEnum GetName()
        {
            return this.name;
        }

        public object GetData(ActionsEnum actionName)
        {
            switch (actionName) {
                case ActionsEnum.GetMyIdZayavka:
                    return new { level = "haos", version = "3.6.8" };
                case ActionsEnum.AddZayavka:
                    return new {
                        startime2= 300,
                        timeout= 1,
                        k= 0,
                        levellogin1= 3,
                        nlogin= 0,
                        closed= 1,
                        cmt= "",
                        level= "haos",
                        open= 1,
                        all= 0,
                        version= "3.6.8",
                        my_id = this.Person.my_id
                    };

                case ActionsEnum.GetMyIdZayavkaFiz:
                    return new { level = "fiz", version = "3.6.8" };
                case ActionsEnum.AddZayavkaFiz:
                    return new
                    {
                        level = "fiz",
                        open = 1,
                        my_id = this.Person.my_id,
                        version = "3.6.8"
                    };
                default:
                    return new { };                   
            }           
        }

        public void Execute(ActionsEnum actionName)
        {
            var resultJson = this.Person.engine.Call(this, ActionsEnum.GetMyIdZayavka);
            //var resultJson = this.Person.engine.Call(this, ActionsEnum.GetMyIdZayavkaFiz);
            var responseJson = JsonConvert.DeserializeObject<ResponseDto>(resultJson);

            this.Person.HP = responseJson.data.HP;
            this.Person.maxHP = responseJson.data.maxHP;


            if (responseJson.redirect_URL != null) {
                this.Person.Log.Info($"Battle start redirect_URL -> {responseJson.redirect_URL}");
                this.Person.InBattle = true;
                var location = this.Person.serviceLocation.Locations.First(l => l.GetName() == NameLocationEnum.Battle);

                var url = new Uri(responseJson.redirect_URL);
                var baseUrl = url.Scheme + "://" + url.Host + url.AbsolutePath;
                this.Person.Log.Info($"Battle url is -> {baseUrl}");
                location.SetUrl(baseUrl);

                return;
            }

            if(!this.Person.IsHealthFull()) this.Person.Log.Info($"Health not Full {this.Person.HP} from {this.Person.maxHP}");

            //Если есть признаки, что заявка не подана, то подаем заявку
            if (responseJson.data.id == null && responseJson.data.in_battle == 0 && string.IsNullOrEmpty(responseJson.data.stop) && this.Person.IsHealthFull())
            {
                this.Person.Log.Info("Get zayavka...");
                this.Person.my_id = responseJson.data.my_id;
                resultJson = this.Person.engine.Call(this, ActionsEnum.AddZayavka);
                //resultJson = this.Person.engine.Call(this, ActionsEnum.AddZayavkaFiz);
                responseJson = JsonConvert.DeserializeObject<ResponseDto>(resultJson);
            }
            if (responseJson.data.id != null && responseJson.data.in_battle == 1 && string.IsNullOrEmpty(responseJson.data.stop) && string.IsNullOrEmpty(responseJson.data.error))
            {
                this.Person.Log.Info("Zayavka already getting. Wait battle...");
            }

            if (!string.IsNullOrEmpty(responseJson.data.stop))
            {
                this.Person.Log.Info(responseJson.data.stop);
            }

            if (!string.IsNullOrEmpty(responseJson.data.error))
            {
                this.Person.Log.Info(responseJson.data.error);
            }
        

       }

    }
}
