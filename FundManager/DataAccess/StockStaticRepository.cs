using System.Collections.Generic;
using System.Linq;
using FundManager.Models;

namespace FundManager.DataAccess
{
  public class StockStaticRepository : IStockRepository
  {
    private static List<Stock> _stocks = new List<Stock>();

    public void Add(Stock stock)
    {
      _stocks.Add(stock);
    }

    public IList<Stock> GetStocks()
    {
      return _stocks;
    }

    public decimal GetTotalMarketValue()
    {
      return _stocks.Sum(x => x.MarketValue);
    }

    public int GetTotalCount()
    {
      return _stocks.Count;
    }

    public decimal GetTotalMarketValue(StockType stockType)
    {
      return _stocks.Where(x => x.Type == stockType).Sum(x => x.MarketValue);
    }

    public int GetTotalCount(StockType stockType)
    {
      return _stocks.Count(x => x.Type == stockType);
    }
  }
}