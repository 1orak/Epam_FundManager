using System.Collections.Generic;
using FundManager.Models;

namespace FundManager.DataAccess
{
  public interface IStockRepository
  {
    void Add(Stock stock);

    IList<Stock> GetStocks();

    decimal GetTotalMarketValue();

    int GetTotalCount();

    decimal GetTotalMarketValue(StockType stockType);

    int GetTotalCount(StockType stockType);
  }
}