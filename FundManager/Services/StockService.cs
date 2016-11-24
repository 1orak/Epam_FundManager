using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FundManager.Builders;
using FundManager.Builders.Interfaces;
using FundManager.DataAccess;
using FundManager.Models;
using FundManager.Services.Interfaces;
using FundManager.ViewModels;
using log4net;

namespace FundManager.Services
{
  public class StockService : IStockService
  {
    private static readonly ILog _log =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly IStockRepository _stockRepository;
    private readonly IStockViewModelBuilderFactory _stockViewModelBuilderFactory;

    private const decimal BondTransactionCostPercent = 0.02M;
    private const decimal EquityTransactionCostPercent = 0.005M;

    //Todo: CalsteWindsor/Ninject (every constructor)
    public StockService() : this(new StockStaticRepository(), new StockViewModelBuilderFactory())
    {
    }

    public StockService(
      IStockRepository stockRepository, 
      IStockViewModelBuilderFactory stockViewModelBuilderFactory)
    {
      _stockRepository = stockRepository;
      _stockViewModelBuilderFactory = stockViewModelBuilderFactory;
    }

    public async Task AddStockAsync(Stock stock)
    {
      try
      {
        int count = await Task.Run(() => _stockRepository.GetTotalCount(stock.Type));
        stock.Name = string.Format("{0}{1}", stock.Type, count + 1);
        stock.MarketValue = stock.Price * stock.Quantity;

        if (stock.Type == StockType.Bond)
        {
          stock.TransactionCost = stock.MarketValue * BondTransactionCostPercent;
        }

        if (stock.Type == StockType.Equity)
        {
          stock.TransactionCost = stock.MarketValue * EquityTransactionCostPercent;
        }

        await Task.Run(() => _stockRepository.Add(stock));

        _log.Debug(string.Format("Successfully add new stock. StockName [{0}]", stock.Name));
      }
      catch (Exception ex)
      {
        _log.Error("An error occured while adding new stock.", ex);
        throw;
      }
    }

    public async Task<IList<StockViewModel>> GetStocksAsync()
    {
      try
      {
        IList<StockViewModel> stockViewModels = new List<StockViewModel>();

        decimal totalMarketValue = await Task.Run(() => _stockRepository.GetTotalMarketValue());
        IList<Stock> stocks = await Task.Run(() => _stockRepository.GetStocks());

        foreach (var stock in stocks)
        {
          IStockViewModelBuilder builder = _stockViewModelBuilderFactory.Create(stock, totalMarketValue);
          StockViewModel stockViewModel = builder.Build();

          stockViewModels.Add(stockViewModel);
        }

        _log.Debug(string.Format("Successfully returned stock list, count: [{0}]", stocks.Count));

        return stockViewModels;
      }
      catch (Exception ex)
      {
        _log.Error("An error occured while get list of stocks.", ex);
        throw;
      }
    }
  }
}