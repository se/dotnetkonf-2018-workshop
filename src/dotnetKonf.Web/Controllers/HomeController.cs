using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetKonf.Web.Models;

namespace dotnetKonf.Web.Controllers
{
    public class HomeController : Controller
    {
        public PriceModel PriceModel { get; set; }
        public HomeController()
        {
            Console.WriteLine("Home Controller created.");

            PriceModel = new PriceModel();
            PriceModel.Pricings = new List<Pricing>();
            PriceModel.Pricings.Add(new Pricing { });
            PriceModel.Pricings.Add(new Pricing { });
            PriceModel.Pricings.Add(new Pricing { });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
