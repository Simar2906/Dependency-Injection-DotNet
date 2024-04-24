using DI_Project.Models;

namespace DI_Project.Services
{
    public class MarketForecaster
    {
        public MarketResult GetMarketPrediction()
        {
            // api to do prediction

            return new MarketResult
            {
                MarketCondition = MarketCondition.StableUp
            };
        }
    }

    public class MarketResult
    {
        public MarketCondition MarketCondition { get; set; }
    }
}
