using FundManager.Models;
using FundManager.Services.Interfaces;
using FundManager.ViewModels;

namespace FundManager.Builders
{
  public abstract class StockViewModelBuilderBase
  {
    protected StockViewModel StockViewModel = new StockViewModel();
    protected readonly IConfigurationService ConfigurationService;

    protected StockViewModelBuilderBase(IConfigurationService configurationService, Stock stock, decimal totalMarketValue)
    {
      ConfigurationService = configurationService;

      StockViewModel.Name = stock.Name;
      StockViewModel.Price = stock.Price;
      StockViewModel.Quantity = stock.Quantity;
      StockViewModel.TransactionCost = stock.TransactionCost;
      StockViewModel.MarketValue = stock.MarketValue;
      StockViewModel.StockWeight = stock.MarketValue / totalMarketValue;

      if (stock.MarketValue < configurationService.MarketValueTolerance)
      {
        StockViewModel.IsHighlighted = true;
      }
    }

    public abstract StockViewModel Build();
  }
}