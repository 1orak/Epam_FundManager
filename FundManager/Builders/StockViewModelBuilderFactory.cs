using System;
using FundManager.Builders.Interfaces;
using FundManager.Models;

namespace FundManager.Builders
{
  public class StockViewModelBuilderFactory : IStockViewModelBuilderFactory
  {
    public IStockViewModelBuilder Create(Stock stock, decimal totalMarketValue)
    {
      switch (stock.Type)
      {
        case StockType.Bond:
          return new StockViewModelBondBuilder(stock, totalMarketValue);
        case StockType.Equity:
          return new StockViewModelEquityBuilder(stock, totalMarketValue);
        default:
          throw new NotSupportedException();
      }
    }
  }
}