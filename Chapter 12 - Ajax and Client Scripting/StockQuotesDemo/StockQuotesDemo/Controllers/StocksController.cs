using System.Web.Mvc;
using System.Xml.Linq;
using StockQuotesDemo.Models;

namespace StockQuotesDemo.Controllers
{
    public class StocksController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public JsonResult GetQuote(string symbol)
        {
            // You could fetch some real data here
            if(symbol == "GOOG") 
                return new JsonResult { Data = new StockData {
                    OpeningPrice = 556.94M, ClosingPrice = 558.20M, Rating = "A+" 
                }};
            else
                return null;
        }

        public ContentResult GetQuoteXML(string symbol)
        {
            // Return some XML data as a string
            if (symbol == "GOOG") {
                return Content(
                    new XDocument(new XElement("Quote",
                         new XElement("OpeningPrice", 556.94M),
                         new XElement("ClosingPrice", 558.20M),
                         new XElement("Rating", "A+")
                    )).ToString()
                , System.Net.Mime.MediaTypeNames.Text.Xml);
            } 
            else
                return null;
        }

    }

}