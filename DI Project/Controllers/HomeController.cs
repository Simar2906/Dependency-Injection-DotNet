using System.Diagnostics;
using DI_Project.Models;
using DI_Project.Models.ViewModels;
using DI_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace DI_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            MarketForecaster marketForecaster = new MarketForecaster();

            MarketResult currentMarket = marketForecaster.GetMarketPrediction();

            switch (currentMarket.MarketCondition)
            {
                case MarketCondition.StableDown:
                    homeVM.MarketForecast = "Market Shows signs of going down for the future";
                    break;
                case MarketCondition.StableUp:
                    homeVM.MarketForecast = "Market Shows signs of going up for the future";
                    break;
                case MarketCondition.Volatile:
                    homeVM.MarketForecast = "Market Shows signs of volatility";
                    break;
                default:
                    homeVM.MarketForecast = "Apply for a credit card using our App!";
                    break;

            }
            return View(homeVM);
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
