namespace FundManager.Services.Interfaces
{
  public interface IConfigurationService
  {
    int BondToleranceTransactionCost { get; }

    int EquityToleranceTransactionCost { get; }

    int MarketValueTolerance { get; }
  }
}