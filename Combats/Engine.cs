using Combats.Actions;
using Combats.Dto;
using Combats.Locations;
using Combats.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Combats
{
    public class Engine
    {
        private string Html { get; set; }
        private Bot Person { get; set; }

        private WebClient wc = new WebClient();

        private ServiceLocation _serviceLocation;

        public Engine(Bot person)
        {
            this.Person = person;
            _serviceLocation = Person.serviceLocation;
        }

        private bool WordIsExist(string html, string word)
        {
            if (String.IsNullOrEmpty(html)) return false;
            return html.Contains(word);
        }

        private void Auth()
        {
            string requestJson = null;
            string resultJson = null;

            var request = new RequestDto();
                request.data = new RequestData() {
                    login = Person.username,
                    psw = Person.password
                };

            requestJson = JsonConvert.SerializeObject(request);
            this.wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            resultJson = wc.UploadString(new Uri("http://capitalcity.combats.com/enter.pl"), "POST", requestJson);
            ResponseDto response = JsonConvert.DeserializeObject<ResponseDto>(resultJson);
            this.wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            resultJson = wc.UploadString(new Uri(response.redirect_URL), "POST", requestJson);
            response = JsonConvert.DeserializeObject<ResponseDto>(resultJson);
            this.Person.Cookies = response.cookies;
        }


        public string Call(ILocation location, ActionsEnum actionName)
        {
            this.Person.Log.Info($"LocationName: [{(NameLocationEnum)location.GetName()}] ActionName: [{(ActionsEnum)actionName}]");

            if (this.Person.Cookies == null) {
                this.Person.Log.Info("Not authorization. Auth()....");
                this.Auth();
            }


            
            var request = new RequestDto() {
                cookies = this.Person.Cookies,
                data = location.GetData(actionName)
            };

            var requestJson = JsonConvert.SerializeObject(request);

            //this.Person.Log.Info($"RequestJson -> : {requestJson}");

            this.wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var resultJson = wc.UploadString(new Uri(location.GetUrl()), "POST", requestJson);

            //this.Person.Log.Info($"ResponseJson <- : {resultJson}");

            return resultJson;
        }

      

    }
}
