using FundManager.Builders.Interfaces;
using FundManager.Models;
using FundManager.Services;
using FundManager.Services.Interfaces;
using FundManager.ViewModels;

namespace FundManager.Builders
{
  public class StockViewModelEquityBuilder : StockViewModelBuilderBase, IStockViewModelBuilder
  {
    public StockViewModelEquityBuilder(IConfigurationService configurationService, Stock stock, decimal totalMarketValue)
      : base(configurationService, stock, totalMarketValue)
    {
    }

    public StockViewModelEquityBuilder(Stock stock, decimal totalMarketValue)
      : this(new ConfigurationService(), stock, totalMarketValue)
    {
    }

    public override StockViewModel Build()
    {
      StockViewModel.Type = StockTypeViewModel.Equity;
      if (StockViewModel.TransactionCost > ConfigurationService.EquityToleranceTransactionCost)
      {
        StockViewModel.IsHighlighted = true;
      }

      return StockViewModel;
    }
  }
}