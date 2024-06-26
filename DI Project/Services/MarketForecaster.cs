﻿using DI_Project.Models;

namespace DI_Project.Services
{
    public class MarketForecaster: IMarketForecaster
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
}
