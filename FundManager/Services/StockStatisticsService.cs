using System;
using FundManager.DataAccess;
using FundManager.Models;
using FundManager.Services.Interfaces;
using FundManager.ViewModels;
using log4net;

namespace FundManager.Services
{
  public class StockStatisticsService: IStockStatisticsService
  {
    private static readonly ILog _log =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly IStockRepository _stockRepository;

    public StockStatisticsService() : this(new StockStaticRepository())
    {
    }

    public StockStatisticsService(IStockRepository stockRepository)
    {
      _stockRepository = stockRepository;
    }

    public StockStatisticsViewModel GetStockStatistics(StockTypeViewModel stockType)
    {
      try
      {
        var stats = new StockStatisticsViewModel();

        if (stockType == StockTypeViewModel.All)
        {
          stats.Count = _stockRepository.GetTotalCount();
          stats.MarketValue = _stockRepository.GetTotalMarketValue();
          stats.Weight = 1;

          return stats;
        }

        var domainStockType = (StockType)stockType;
        var totalMarketValue = _stockRepository.GetTotalMarketValue();

        stats.Count = _stockRepository.GetTotalCount(domainStockType);
        stats.MarketValue = _stockRepository.GetTotalMarketValue(domainStockType);
        stats.Weight = stats.MarketValue / totalMarketValue;

        return stats;
      }
      catch (Exception ex)
      {
        _log.Error("Error while getting stock statistics.", ex);
        throw;
      }
    }
  }
}