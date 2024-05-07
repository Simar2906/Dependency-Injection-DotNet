using DI_Project.Models;

namespace DI_Project.Services
{
    public class MarketForecasterV2 : IMarketForecaster
    {
        public MarketResult GetMarketPrediction()
        {
            // api to do prediction

            return new MarketResult
            {
                MarketCondition = MarketCondition.Volatile
            };
        }
    }
}
