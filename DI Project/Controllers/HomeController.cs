using System.Diagnostics;
using DI_Project.Data;
using DI_Project.Data.Repository;
using DI_Project.Data.Repository.IRepository;
using DI_Project.Models;
using DI_Project.Models.ViewModels;
using DI_Project.Services;
using DI_Project.Utility.AppSettingsClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DI_Project.Controllers
{
    public class HomeController : Controller
    {
        public HomeVM homeVM { get; set; }
        private readonly IMarketForecaster _marketForecaster;
        private readonly ICreditValidator _creditValidator;
        private readonly WazeForecastSettings _wazeOptions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        [BindProperty]
        public CreditApplication CreditModel { get; set; }
        public HomeController(IMarketForecaster marketForecaster, 
            IOptions<WazeForecastSettings> wazeOptions, 
            ICreditValidator creditValidator,
            IUnitOfWork unitOfWork,
            ILogger<HomeController> logger) {
            homeVM = new HomeVM();
            _marketForecaster = marketForecaster;
            _wazeOptions = wazeOptions.Value;
            _creditValidator = creditValidator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Index Started");
            MarketResult currentMarket = _marketForecaster.GetMarketPrediction();

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
            _logger.LogInformation("Index Ended");
            return View(homeVM);
        }

        public IActionResult AllConfigSettings(
            [FromServices] IOptions<StripeSettings> stripeOptions,
            [FromServices] IOptions<SendGridSettings> sendGridOptions,
            [FromServices] IOptions<TwilioSettings> twilioOptions)
        {
            List<string> messages = new List<string>();
            messages.Add($"Waze config - Forecast Tracker: " + _wazeOptions.ForecastTrackerEnabled);
            messages.Add($"Stripe Publishable Key: " + stripeOptions.Value.PublishableKey);
            messages.Add($"Stripe Secret Key: " + stripeOptions.Value.SecretKey);
            messages.Add($"SendGrid Key: " + sendGridOptions.Value.SendGridKey);
            messages.Add($"Twilio Phone: " + twilioOptions.Value.PhoneNumber);
            messages.Add($"Twilio SID: " + twilioOptions.Value.AccountSid);
            messages.Add($"TWilio Token: " + twilioOptions.Value.AuthToken);

            return View(messages);
        }

        public IActionResult CreditApplication()
        {
            CreditModel = new CreditApplication();
            return View(CreditModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("CreditApplication")]
        public async Task<IActionResult> CreditApplicationPOST(
            [FromServices] Func<CreditApprovedEnum, ICreditApproved> _creditService)
        {
            if(ModelState.IsValid)
            {
                var (validationPassed, errorMessages) = await _creditValidator.PassAllValidations(CreditModel);

                CreditResult creditResult = new CreditResult
                {
                    ErrorList = errorMessages,
                    CreditID = 0,
                    Success = validationPassed

                };
                if(validationPassed)
                {
                    CreditModel.CreditApproved = _creditService(CreditModel.Salary > 50000 
                        ? CreditApprovedEnum.High 
                        : CreditApprovedEnum.Low)
                        .GetCreditApproved(CreditModel);
                    //add record to DB
                    _unitOfWork.CreditApplicationRepository.Add(CreditModel);
                    _unitOfWork.Save();
                    creditResult.CreditID = CreditModel.Id;
                    creditResult.CreditApproved = CreditModel.CreditApproved;
                    return RedirectToAction(nameof(CreditResult), creditResult);
                }
                else
                {
                    return RedirectToAction(nameof(CreditResult), creditResult);

                }
            }
            return View(CreditModel);
        }

        public IActionResult CreditResult(CreditResult creditResult)
        {
            return View(creditResult);
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
