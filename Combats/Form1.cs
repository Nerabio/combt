using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Combats
{
    public partial class Form1 : Form
    {
        public Bot bot;

        public Bot GetBot()
        {
            if (this.bot == null)
            {
                this.bot = new Bot();
                this.bot.Init();
            }
           return this.bot;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://dreamscity.combats.com/enter.pl");
            request.Method = "POST";
            string data = "login=Abio&psw=1q2w3e4r5t";
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/x-www-form-urlencoded";
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;

            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }


            HttpWebResponse responce = (HttpWebResponse)request.GetResponse();
            //using (StreamReader stream = new StreamReader(responce.GetResponseStream(), Encoding.UTF8))
            //{
            //    MessageBox.Show(stream.ReadToEnd());
            //}

            HttpWebRequest request2 = (HttpWebRequest)HttpWebRequest.Create(responce.ResponseUri.AbsoluteUri);
           

            using (StreamReader stream = new StreamReader(responce.GetResponseStream(), Encoding.UTF8))
            {
                MessageBox.Show(stream.ReadToEnd());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = "Abio";
            string password = "1q2w3e4r5t";
            string loginAdress = "http://dreamscity.combats.com/enter.pl";
            string authString = "http://dreamscity.combats.com/enter.pl&login=" + username + "&psw=" + password;
            Uri CookieHostname = new Uri("http://combats.com");
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] buffer = encoding.GetBytes(authString);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(loginAdress);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentLength = buffer.Length;
            request.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.83 Safari/535.11";
            request.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, @"ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, @"gzip,deflate,sdch");
            request.Headers.Add(HttpRequestHeader.AcceptCharset, @"Accept-Charset: utf-8;q=0.7,*;q=0.3");
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            CookieContainer cookie = new CookieContainer();
            request.CookieContainer = cookie;
            request.ContentType = "application/x-www-form-urlencoded";
            Stream newStream = request.GetRequestStream();
            newStream.Write(buffer, 0, authString.Length);
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            CookieCollection cookies = new CookieCollection();
            cookies = response.Cookies;
            Encoding responseEncoding = Encoding.GetEncoding(response.CharacterSet);
            StreamReader strReader = new StreamReader(response.GetResponseStream(), responseEncoding);
            string htmlText = strReader.ReadToEnd();
            response.Close();
            cookie.Add(CookieHostname, cookies);

            WebClient wc = new WebClient();
            wc.Headers.Add("Cookie", cookie.GetCookieHeader(CookieHostname));
            Uri uri = new Uri("http://dreamscity.combats.com/main.pl");
            string answer = wc.DownloadString(uri);

            Uri uri2 = new Uri("http://dreamscity.combats.com/zayavka.pl?level=fiz&my_id=725806086&open=1&from_quick_butt=1");

            string answer2 = wc.DownloadString(uri2);
            // Создаём коллекцию параметров
            //var pars = new NameValueCollection();

            //var myId = "612699337";
            //// Добавляем необходимые параметры в виде пар ключ, значение
            //pars.Add("level", "fiz");
            //pars.Add("my_id", myId);
            //pars.Add("open", "%CF%EE%E4%E0%F2%FC+%E7%E0%FF%E2%EA%F3");


        }

        private void button3_Click(object sender, EventArgs e)
        {

            


            if (this.GetBot().InBattle) {
                this.GetBot().Kick();
                timer1.Interval = 1000;
            }
            else{
                this.GetBot().GotoZayavka();
                timer1.Interval = 6000;
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {

            
            string name = "http://dreamscity.combats.com/battle2.pl?json_data=HASH(0x10f80070)&version=3.6.8&isJson=1&0.993503447703553";
            var dd = new Uri(name);

            var result = dd.Scheme + "://" + dd.Host + dd.AbsolutePath;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://dreamscity.combats.com/index.html");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = webBrowser1.DocumentText;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i = int.Parse(textBox1.Text);
            richTextBox1.Text = webBrowser1.Document.Window.Frames[i].Document.Body.OuterHtml;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
                button8.Text = "Off";
            }
            else {
                timer1.Enabled = false;
                button8.Text = "On";
            }
        }
    }
}
