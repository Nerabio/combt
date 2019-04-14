using Combats.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Combats.Locations
{
    class BattleLocation : BaseLocation, ILocation
    {
        private NameLocationEnum name = NameLocationEnum.Battle;

        private string url = "http://dreamscity.combats.com/battle1.pl";

        private Bot Person { get; set; }

        private string nBattle { get; set; }
        private string enemy { get; set; }

        public BattleLocation(Bot person)
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
            switch (actionName)
            {
                case ActionsEnum.Status:
                    return new { version = "3.6.8" };
                case ActionsEnum.Kick:
                    var attack0 = this.GetSelection(1).ToString();
                    var attack1 = this.GetSelection(2).ToString();
                    var defend0 = this.GetSelection(3).ToString();
                    var defend1 = this.GetSelection(4).ToString();

                    this.Person.Log.Info($"STEP->  attack0: {attack0} attack1: {attack1} defend0: {defend0} defend1: {defend1}");

                    string json = "{ \"log\": 0, \"mycnt\": 1, \"nBattle\": \"" +this.nBattle+"\",  \"iFaceId\": 1555193827304, \"enemy\": \""+this.enemy+"\", \""+this.enemy+ "\": 1, \"attack0\": " + attack0 + ", \"attack1\": " + attack1 + ",\"defend0\": " + defend0 + ", \"version\": \"3.6.8\" }";

                    var result = JsonConvert.DeserializeObject(json);

                    return result;
                default:
                    return new { };
            }

        }

        private int GetSelection(int index) {

            System.Random random = new System.Random();
            var mas = Enumerable.Range(1, 5).OrderBy(n => random.Next()).ToArray();
            return mas[index];
           // Random rnd = new Random();
            //int value = rnd.Next(1, 6);
            //return value;
        }

        public void Execute(ActionsEnum actionName)
        {
            var resultJson = this.Person.engine.Call(this, ActionsEnum.Status);
            var responseJson = JsonConvert.DeserializeObject<RootObject>(resultJson);

            if (responseJson.gameover != null) {
                this.Person.Log.Info("Battle is END");
                this.Person.InBattle = false;
                return;
            }

            this.nBattle = responseJson.id;
            this.enemy = responseJson.user.Find(u => u.enemy == 1)?.sVirtID;

            if (!String.IsNullOrEmpty(this.nBattle) && !String.IsNullOrEmpty(this.enemy))
            {
                resultJson = this.Person.engine.Call(this, ActionsEnum.Kick);
            }
        }

    }


    
    public class User
    {
        public string sClan { get; set; }
        public string sIcon { get; set; }
        public int config_blinker { get; set; }
        public List<object> arrEffects { get; set; }
        public string sTitle { get; set; }
        public int group { get; set; }
        public string nLevel { get; set; }
        public string sName { get; set; }
        public int sAlign { get; set; }
        public int nHP { get; set; }
        public int nCount { get; set; }
        public string sVirtID { get; set; }
        public int nMaxHP { get; set; }
        public string sID { get; set; }
        public int side { get; set; }
        public int role { get; set; }
        public int? enemy { get; set; }
    }

    public class Gameover
    {
        public string location { get; set; }
    }

    public class RootObject
    {

        public List<User> user { get; set; }
        public string id { get; set; }
        public Gameover gameover { get; set; }
    }
}
