using FundManager.Models;

namespace FundManager.Builders.Interfaces
{
  public interface IStockViewModelBuilderFactory
  {
    IStockViewModelBuilder Create(Stock stock, decimal totalMarketValue);
  }
}