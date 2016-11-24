using System;
using System.Collections.Generic;
using FundManager.DataAccess;
using FundManager.Models;
using FundManager.Services;
using FundManager.ViewModels;
using Moq;
using NUnit.Framework;

namespace FoundmanagerTests.Services
{
  [TestFixture]
  public class StockStatisticsServiceTests
  {
    private Mock<IStockRepository> _stockRepositoryFake;

    private StockStatisticsService _stockStatisticsService;

    [SetUp]
    public void SetUp()
    {
      _stockRepositoryFake = new Mock<IStockRepository>();

      _stockStatisticsService = new StockStatisticsService(_stockRepositoryFake.Object);
    }

    [TearDown]
    public void TearDown()
    {
      _stockRepositoryFake.Verify();
    }

    [Test]
    public void GetStockStatistics_StockTypeViewModel_All_return_statistics_successfully()
    {
      decimal totalMarketValue = 123.14M;
      int totalCount = 145;

      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue()).Returns(totalMarketValue).Verifiable();
      _stockRepositoryFake.Setup(x => x.GetTotalCount()).Returns(totalCount).Verifiable();

      var result = _stockStatisticsService.GetStockStatistics(StockTypeViewModel.All);

      Assert.AreEqual(totalMarketValue, result.MarketValue);
      Assert.AreEqual(totalCount, result.Count);
      Assert.AreEqual(1, result.Weight);
    }

    [Test]
    public void GetStockStatistics_StockTypeViewModel_Equity_return_statistics_successfully()
    {
      decimal totalMarketValue = 123.14M;
      decimal equityMarketValue = 9.39M;
      int totalCount = 145;

      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue(StockType.Equity)).Returns(equityMarketValue).Verifiable();
      _stockRepositoryFake.Setup(x => x.GetTotalCount(StockType.Equity)).Returns(totalCount).Verifiable();
      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue()).Returns(totalMarketValue).Verifiable();

      var result = _stockStatisticsService.GetStockStatistics(StockTypeViewModel.Equity);

      Assert.AreEqual(equityMarketValue, result.MarketValue);
      Assert.AreEqual(totalCount, result.Count);
      Assert.AreEqual(equityMarketValue/totalMarketValue, result.Weight);
    }

    [Test]
    public void GetStockStatistics_StockTypeViewModel_Bond_return_statistics_successfully()
    {
      decimal totalMarketValue = 123.14M;
      decimal equityMarketValue = 9.39M;
      int totalCount = 145;

      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue(StockType.Bond)).Returns(equityMarketValue).Verifiable();
      _stockRepositoryFake.Setup(x => x.GetTotalCount(StockType.Bond)).Returns(totalCount).Verifiable();
      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue()).Returns(totalMarketValue).Verifiable();

      var result = _stockStatisticsService.GetStockStatistics(StockTypeViewModel.Bond);

      Assert.AreEqual(equityMarketValue, result.MarketValue);
      Assert.AreEqual(totalCount, result.Count);
      Assert.AreEqual(equityMarketValue / totalMarketValue, result.Weight);
    }

    [TestCase(StockTypeViewModel.Equity)]
    [TestCase(StockTypeViewModel.Bond)]
    public void GetStockStatistics_rethrows_exception(StockTypeViewModel stockTypeViewModel)
    {
      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue(It.IsAny<StockType>())).Throws(new Exception());
      Assert.Throws<Exception>(() => _stockStatisticsService.GetStockStatistics(stockTypeViewModel));
    }

    public void GetStockStatistics_rethrows_exception()
    {
      _stockRepositoryFake.Setup(x => x.GetTotalMarketValue()).Throws(new Exception());
      Assert.Throws<Exception>(() => _stockStatisticsService.GetStockStatistics(StockTypeViewModel.All));
    }
  }
}