using FundManager.ViewModels;

namespace FundManager.Services.Interfaces
{
  public interface IStockStatisticsService
  {
    StockStatisticsViewModel GetStockStatistics(StockTypeViewModel stockType);
  }
}