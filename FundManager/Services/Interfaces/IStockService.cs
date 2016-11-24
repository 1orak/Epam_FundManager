using System.Collections.Generic;
using System.Threading.Tasks;
using FundManager.Models;
using FundManager.ViewModels;

namespace FundManager.Services.Interfaces
{
  public interface IStockService
  {
    Task AddStockAsync(Stock stock);

    Task<IList<StockViewModel>> GetStocksAsync();
  }
}