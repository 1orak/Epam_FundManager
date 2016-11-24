using System.Collections.Generic;
using System.Reflection;
using FundManager.DataAccess;
using FundManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace FoundmanagerTests.DataAccess
{
  [TestFixture]
  public class StockStaticRepositoryTests
  {
    private StockStaticRepository _stockStaticRepository;

    [SetUp]
    public void SetUp()
    {
      _stockStaticRepository = new StockStaticRepository();
    }

    [TearDown]
    public void TearDown()
    {
      //simulate clearing db
      typeof(StockStaticRepository).GetField("_stocks",
          BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
        .SetValue(_stockStaticRepository, new List<Stock>());
    }

    [Test]
    [TestCategory("Integration")]
    public void Add_add_succesfully_stock()
    {
      _stockStaticRepository.Add(new Stock());

      var result = _stockStaticRepository.GetStocks();

      Assert.AreEqual(1, result.Count);
    }

    [Test]
    [TestCategory("Integration")]
    public void GetStocks_return_empty_list()
    {
      var result = _stockStaticRepository.GetStocks();

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Count);
    }

    [Test]
    [TestCategory("Integration")]
    public void GetTotalMarketValue_return_sum_of_MarketValue()
    {
      var stock1 = new Stock {MarketValue = 1.01M};
      var stock2 = new Stock {MarketValue = 123.12M};

      _stockStaticRepository.Add(stock1);
      _stockStaticRepository.Add(stock2);

      var result = _stockStaticRepository.GetTotalMarketValue();

      Assert.AreEqual(stock1.MarketValue + stock2.MarketValue, result);
    }

    [Test]
    [TestCategory("Integration")]
    public void GetTotalCount_return_total_count()
    {
      var stock1 = new Stock ();

      _stockStaticRepository.Add(stock1);
      _stockStaticRepository.Add(stock1);

      var result = _stockStaticRepository.GetTotalCount();

      Assert.AreEqual(2, result);
    }

    [Test]
    [TestCategory("Integration")]
    public void GetTotalMarketValue_for_bond_return_sum_of_MarketValue()
    {
      var stock1 = new Stock { Type = StockType.Bond, MarketValue = 1.01M };
      var stock2 = new Stock { Type = StockType.Equity, MarketValue = 123.12M };

      _stockStaticRepository.Add(stock1);
      _stockStaticRepository.Add(stock2);

      var result = _stockStaticRepository.GetTotalMarketValue(StockType.Bond);

      Assert.AreEqual(stock1.MarketValue, result);
    }

    [Test]
    [TestCategory("Integration")]
    public void GetTotalMarketValue_for_equity_return_sum_of_MarketValue()
    {
      var stock1 = new Stock { Type = StockType.Bond, MarketValue = 1.01M };
      var stock2 = new Stock { Type = StockType.Equity, MarketValue = 123.12M };
      var stock3 = new Stock { Type = StockType.Equity, MarketValue = 13.12M };

      _stockStaticRepository.Add(stock1);
      _stockStaticRepository.Add(stock2);
      _stockStaticRepository.Add(stock3);

      var result = _stockStaticRepository.GetTotalMarketValue(StockType.Equity);

      Assert.AreEqual(stock2.MarketValue + stock3.MarketValue, result);
    }

    [TestCase(StockType.Bond)]
    [TestCase(StockType.Equity)]
    [TestCategory("Integration")]
    public void GetTotalCount_return_total_count(StockType stockType)
    {
      var stock1 = new Stock { Type = StockType.Bond };
      var stock2 = new Stock { Type = StockType.Equity };

      _stockStaticRepository.Add(stock1);
      _stockStaticRepository.Add(stock2);

      var result = _stockStaticRepository.GetTotalCount(stockType);

      Assert.AreEqual(1, result);
    }
  }
}