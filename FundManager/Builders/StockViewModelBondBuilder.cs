using FundManager.Builders.Interfaces;
using FundManager.Models;
using FundManager.Services;
using FundManager.Services.Interfaces;
using FundManager.ViewModels;

namespace FundManager.Builders
{
  public class StockViewModelBondBuilder : StockViewModelBuilderBase, IStockViewModelBuilder
  {
    public StockViewModelBondBuilder(IConfigurationService configurationService, Stock stock, decimal totalMarketValue)
      : base(configurationService, stock, totalMarketValue)
    {
    }

    public StockViewModelBondBuilder(Stock stock, decimal totalMarketValue)
      : this(new ConfigurationService(), stock, totalMarketValue)
    {
    }

    public override StockViewModel Build()
    {
      StockViewModel.Type = StockTypeViewModel.Bond;
      if (StockViewModel.TransactionCost > ConfigurationService.BondToleranceTransactionCost)
      {
        StockViewModel.IsHighlighted = true;
      }

      return StockViewModel;
    }
  }
}