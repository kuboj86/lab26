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
            JObject DeckJson = JObject.Parse(Shuffle);
            ViewBag.DeckId = DeckJson["deck_id"];

            HttpWebRequest NewWR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/" + ViewBag.DeckId + "/draw/?count=5");
            WR.UserAgent = ".NET Framework Test";
            HttpWebResponse NewResponse = (HttpWebResponse) NewWR.GetResponse();
            StreamReader reader2 = new StreamReader(NewResponse.GetResponseStream());
            string CardData2 = reader2.ReadToEnd();
            JObject NewDeck= JObject.Parse(CardData2);
            ViewBag.CardData2 = NewDeck["cards"];

            return View();
        }

    }
}