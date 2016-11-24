using System;
using System.Collections.Generic;
using FundManager.Builders.Interfaces;
using FundManager.DataAccess;
using FundManager.Models;
using FundManager.Services;
using FundManager.ViewModels;
using Moq;
using NUnit.Framework;

namespace FoundmanagerTests.Services
{
  [TestFixture]
  public class StockServiceTests
  {
    private const decimal BondTransactionCostPercent = 0.02M;
    private const decimal EquityTransactionCostPercent = 0.005M;

    private Mock<IStockRepository> _stockRepositoryFake;
    private Mock<IStockViewModelBuilderFactory> _stockViewModelBuilderFactoryFake;
    private Mock<IStockViewModelBuilder> _stockViewModelBuilder;

    private StockService _stockService;

    [SetUp]
    public void SetUp()
    {
      _stockRepositoryFake = new Mock<IStockRepository>();
      _stockViewModelBuilderFactoryFake = new Mock<IStockViewModelBuilderFactory>();
      _stockViewModelBuilder = new Mock<IStockViewModelBuilder>();

      _stockService = new StockService(_stockRepositoryFake.Object, _stockViewModelBuilderFactoryFake.Object);
    }

    [TearDown]
    public void TearDown()
    {
      _stockRepositoryFake.Verify();
      _stockViewModelBuilderFactoryFake.Verify();
    }

    [Test]
    public void AddStockAsync_add_stock_type_bond_correct()
    {
      Stock stock = new Stock {Type = StockType.Bond};

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Returns(0).Verifiable();
      _stockRepositoryFake.Setup(x => x.Add(It.Is<Stock>(y => y.Type == StockType.Bond))).Verifiable();

      _stockService.AddStockAsync(stock).GetAwaiter().GetResult();
    }

    [TestCase(0, 0)]
    [TestCase(-1.13, 1)]
    [TestCase(344423.01, 1)]
    [TestCase(-1.13, 0)]
    [TestCase(344423.01, 0)]
    [TestCase(-1.13, -1)]
    [TestCase(344423.01, -1)]
    [TestCase(-1.13, 1332)]
    [TestCase(344423.01, 1123)]
    public void AddStockAsync_set_TransactionCost_bond_correct(decimal price, int quantity)
    {
      Stock stock = new Stock { Type = StockType.Bond, Price = price, Quantity = quantity };

      decimal marketValue = price*quantity;

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Returns(0);
      _stockRepositoryFake.Setup(
        x => x.Add(It.Is<Stock>(y => y.TransactionCost == marketValue*BondTransactionCostPercent))).Verifiable();

      _stockService.AddStockAsync(stock).GetAwaiter().GetResult();
    }

    [TestCase(0, 0)]
    [TestCase(-1.13, 1)]
    [TestCase(344423.01, 1)]
    [TestCase(-1.13, 0)]
    [TestCase(344423.01, 0)]
    [TestCase(-1.13, -1)]
    [TestCase(344423.01, -1)]
    [TestCase(-1.13, 1332)]
    [TestCase(344423.01, 1123)]
    public void AddStockAsync_set_MarketValue_bond_correct(decimal price, int quantity)
    {
      Stock stock = new Stock { Type = StockType.Bond, Price = price, Quantity = quantity };

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Returns(0);
      _stockRepositoryFake.Setup(
        x => x.Add(It.Is<Stock>(y => y.MarketValue == price * quantity))).Verifiable();

      _stockService.AddStockAsync(stock).GetAwaiter().GetResult();
    }

    [TestCase(0)]
    [TestCase(1230)]
    public void AddStockAsync_set_Name_bond_correct(int count)
    {
      Stock stock = new Stock { Type = StockType.Bond};

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Returns(count);
      _stockRepositoryFake.Setup(
        x => x.Add(It.Is<Stock>(y => y.Name == string.Format("Bond{0}", count+1)))).Verifiable();

      _stockService.AddStockAsync(stock).GetAwaiter().GetResult();
    }

    [Test]
    public void AddStockAsync_add_stock_type_equity_correct()
    {
      Stock stock = new Stock { Type = StockType.Equity };

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Returns(0).Verifiable();
      _stockRepositoryFake.Setup(x => x.Add(It.Is<Stock>(y => y.Type == StockType.Equity))).Verifiable();

      _stockService.AddStockAsync(stock).GetAwaiter().GetResult();
    }

    [TestCase(0, 0)]
    [TestCase(-1.13, 1)]
    [TestCase(344423.01, 1)]
    [TestCase(-1.13, 0)]
    [TestCase(344423.01, 0)]
    [TestCase(-1.13, -1)]
    [TestCase(344423.01, -1)]
    [TestCase(-1.13, 1332)]
    [TestCase(344423.01, 1123)]
    public void AddStockAsync_set_TransactionCost_equity_correct(decimal price, int quantity)
    {
      Stock stock = new Stock { Type = StockType.Equity, Price = price, Quantity = quantity };

      decimal marketValue = price * quantity;

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Returns(0);
      _stockRepositoryFake.Setup(
        x => x.Add(It.Is<Stock>(y => y.TransactionCost == marketValue * EquityTransactionCostPercent))).Verifiable();

      _stockService.AddStockAsync(stock).GetAwaiter().GetResult();
    }

    [TestCase(0, 0)]
    [TestCase(-1.13, 1)]
    [TestCase(344423.01, 1)]
    [TestCase(-1.13, 0)]
    [TestCase(344423.01, 0)]
    [TestCase(-1.13, -1)]
    [TestCase(344423.01, -1)]
    [TestCase(-1.13, 1332)]
    [TestCase(344423.01, 1123)]
    public void AddStockAsync_set_MarketValue_equity_correct(decimal price, int quantity)
    {
      Stock stock = new Stock { Type = StockType.Equity, Price = price, Quantity = quantity };

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Returns(0);
      _stockRepositoryFake.Setup(
        x => x.Add(It.Is<Stock>(y => y.MarketValue == price * quantity))).Verifiable();

      _stockService.AddStockAsync(stock).GetAwaiter().GetResult();
    }

    [TestCase(0)]
    [TestCase(1230)]
    public void AddStockAsync_set_Name_equity_correct(int count)
    {
      Stock stock = new Stock { Type = StockType.Equity };

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Returns(count);
      _stockRepositoryFake.Setup(
        x => x.Add(It.Is<Stock>(y => y.Name == string.Format("Equity{0}", count + 1)))).Verifiable();

      _stockService.AddStockAsync(stock).GetAwaiter().GetResult();
    }

    [TestCase(StockType.Bond)]
    [TestCase(StockType.Equity)]
    public void AddStockAsync_rethrows_exception(StockType stockType)
    {
      Stock stock = new Stock { Type = stockType };

      _stockRepositoryFake.Setup(x => x.GetTotalCount(stock.Type)).Throws(new Exception());

      Assert.Throws<Exception>(() =>_stockService.AddStockAsync(stock).GetAwaiter().GetResult());
    }

    [Test]
    public void GetStocksAsync_returns_stocks_successfully()
    {
      var stocks = new List<Stock> {new Stock(), new Stock()};

      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue()).Returns(124).Verifiable();
      _stockRepositoryFake.Setup(x => x.GetStocks()).Returns(stocks).Verifiable();

      _stockViewModelBuilderFactoryFake.Setup(x => x.Create(It.IsAny<Stock>(), It.IsAny<decimal>()))
        .Returns(_stockViewModelBuilder.Object);
      _stockViewModelBuilder.Setup(x => x.Build()).Returns(new StockViewModel());

      IList<StockViewModel> result = _stockService.GetStocksAsync().GetAwaiter().GetResult();

      _stockViewModelBuilder.Verify(x => x.Build(), Times.Exactly(2));

      Assert.AreEqual(2, result.Count);
    }

    [Test]
    public void GetStocksAsync_rethrows_exception()
    {
      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue()).Throws(new Exception());

      Assert.Throws<Exception>(() => _stockService.GetStocksAsync().GetAwaiter().GetResult());
    }
  }
}