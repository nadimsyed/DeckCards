using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DeckofCards.Controllers
{
    public class HomeController : Controller
    {
        string url = "";
        string id = "";
        public ActionResult Index()
        {
            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            WR.UserAgent = ".NET Framework Test Client";

            HttpWebResponse Response3;

            try
            {
                Response3 = (HttpWebResponse)WR.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response3.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response3.StatusCode;
                ViewBag.ErrorDescription = Response3.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response3.GetResponseStream());
            string CardData = reader.ReadToEnd();

            try
            {
                JObject JsonData = JObject.Parse(CardData);
                ViewBag.DeckId = JsonData["deck_id"];
                //Session["x"] = ViewBag.DeckId;
                //string sess = (string)Session["x"];
                //id = (string)JsonData["deck_id"];

                //var domain = "https://deckofcardsapi.com/api/deck/";
                //string id = ViewBag.DeckId;
                //var end = "/draw /? count = 5";
                //string full = $"{domain}{id}{end}";
                //$"{domain}{ViewBag.DeckId}{end}"

                //HttpCookie myCookie = new HttpCookie("url");
                id = ViewBag.DeckId;
                url = $"https://deckofcardsapi.com/api/deck/{ViewBag.DeckId}/draw/?count=5";
                Response.Cookies["Link"].Value = url;
                Response.Cookies["Link"].Expires = DateTime.Now.AddMinutes(2);
                //myCookie.Expires = DateTime.Now.AddHours(12);
                //HttpResponse.AppendCookie(myCookie);

                var input = Request.Cookies["Link"] != null ? Request.Cookies["Link"].Value : String.Empty;

                //if for some reason it starts throwing null exception reference again, just paste in url above and replace input and it will work again
                HttpWebRequest WR2 = WebRequest.CreateHttp($"{input}");
                WR2.UserAgent = ".NET Framework Test Client";

                HttpWebResponse Response2;

                try
                {
                    Response2 = (HttpWebResponse)WR2.GetResponse();
                }
                catch (WebException ex)
                {
                    ViewBag.Error2 = "Exception";
                    ViewBag.ErrorDescription2 = ex.Message;
                    return View();
                }

                if (Response2.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error2 = Response2.StatusCode;
                    ViewBag.ErrorDescription2 = Response2.StatusDescription;
                    return View();
                }

                StreamReader reader2 = new StreamReader(Response2.GetResponseStream());
                string Card2Data = reader2.ReadToEnd();

                try
                {
                    JObject Json2Data = JObject.Parse(Card2Data);
                    ViewBag.Cards = /*(JObject)*/Json2Data["cards"];

                    //ViewBag.Cards1 = (JObject)Json2Data["cards"][0];
                    //ViewBag.Cards2 = (JObject)Json2Data["cards"][1];
                    //ViewBag.Cards3 = (JObject)Json2Data["cards"][2];
                    //ViewBag.Cards4 = (JObject)Json2Data["cards"][3];
                    //ViewBag.Cards5 = (JObject)Json2Data["cards"][4];

                    //ViewBag.Images1 = JsonData["cards"][0]["image"];
                    //ViewBag.Images2 = JsonData["cards"][1]["image"];
                    //ViewBag.Images3 = JsonData["cards"][2]["image"];
                    //ViewBag.Images4 = JsonData["cards"][3]["image"];
                    //ViewBag.Images5 = JsonData["cards"][4]["image"];

                    //ViewBag.Value1 = JsonData["cards"][0]["value"];
                    //ViewBag.Value2 = JsonData["cards"][1]["value"];
                    //ViewBag.Value3 = JsonData["cards"][2]["value"];
                    //ViewBag.Value4 = JsonData["cards"][3]["value"];
                    //ViewBag.Value5 = JsonData["cards"][4]["value"];

                    //ViewBag.Suit1 = JsonData["cards"][0]["suit"];
                    //ViewBag.Suit2 = JsonData["cards"][1]["suit"];
                    //ViewBag.Suit3 = JsonData["cards"][2]["suit"];
                    //ViewBag.Suit4 = JsonData["cards"][3]["suit"];
                    //ViewBag.Suit5 = JsonData["cards"][4]["suit"];


                }
                catch (Exception ex)
                {
                    ViewBag.Error2 = "JSON Issue";
                    ViewBag.ErrorDescription2 = ex.Message;
                    return View();
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            return View();
        }
            
        public ActionResult Draw()
        {
            var input = Request.Cookies["Link"] != null ? Request.Cookies["Link"].Value : String.Empty;

            HttpWebRequest WR2 = WebRequest.CreateHttp($"{input}");
            WR2.UserAgent = ".NET Framework Test Client";

            HttpWebResponse Response2;

            try
            {
                Response2 = (HttpWebResponse)WR2.GetResponse();
            }
            catch (WebException ex)
            {
                ViewBag.Error2 = "Exception";
                ViewBag.ErrorDescription2 = ex.Message;
                return View("Index");
            }

            if (Response2.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error2 = Response2.StatusCode;
                ViewBag.ErrorDescription2 = Response2.StatusDescription;
                return View("Index");
            }

            StreamReader reader2 = new StreamReader(Response2.GetResponseStream());
            string Card2Data = reader2.ReadToEnd();

            try
            {
                JObject Json2Data = JObject.Parse(Card2Data);
                ViewBag.Cards = /*(JObject)*/Json2Data["cards"];
            }
            catch (Exception ex)
            {
                ViewBag.Error2 = "JSON Issue";
                ViewBag.ErrorDescription2 = ex.Message;
                return View("Index");
            }

            return View("Index");
        }
        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        //TODO: Just realized I can make another action that shuffles and shuffle same deck
    }
}