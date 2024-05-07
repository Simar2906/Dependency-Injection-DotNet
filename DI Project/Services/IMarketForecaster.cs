using DI_Project.Models;

namespace DI_Project.Services
{
    public interface IMarketForecaster
    {
        MarketResult GetMarketPrediction();
    }
}