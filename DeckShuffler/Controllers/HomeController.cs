using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using WebGrease.Css.ImageAssemblyAnalysis;

namespace DeckShuffler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShuffleDeck()
        {


            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            WR.UserAgent = ".NET Framework Test";
            HttpWebResponse Response = (HttpWebResponse) WR.GetResponse();
            
            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string Shuffle = reader.ReadToEnd();
            JObject ShuffledCards = JObject.Parse(Shuffle);
            string draw = ShuffledCards["deck_id"].ToString();

            WR = WebRequest.CreateHttp($"https://deckofcardsapi.com/api/deck/{draw}/draw/?count=1");
            Response = (HttpWebResponse) WR.GetResponse();
            StreamReader reader2 = new StreamReader(Response.GetResponseStream());
            string CardData2 = reader2.ReadToEnd();
            ShuffledCards = JObject.Parse(CardData2);
            draw = ShuffledCards["deck_id"].ToString();


            ViewBag.Card1 = ShuffledCards["cards"][0];
            ViewBag.Card2 = ShuffledCards["cards"]["1"]["images"];
            ViewBag.Card3 = ShuffledCards["cards"]["2"]["images"];
            ViewBag.Card4 = ShuffledCards["cards"]["3"]["images"];
            ViewBag.Card5 = ShuffledCards["cards"]["4"]["images"];

            return View();


        }

    }
}