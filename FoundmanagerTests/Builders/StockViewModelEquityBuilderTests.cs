using FundManager.Builders;
using FundManager.Models;
using FundManager.Services.Interfaces;
using FundManager.ViewModels;
using Moq;
using NUnit.Framework;

namespace FoundmanagerTests.Builders
{
  [TestFixture]
  public class StockViewModelEquityBuilderTests
  {
    private Mock<IConfigurationService> _configurationServiceFake;

    private StockViewModelEquityBuilder _builder;

    [SetUp]
    public void SetUp()
    {
      _configurationServiceFake = new Mock<IConfigurationService>();
    }

    [TearDown]
    public void TearDown()
    {
      _configurationServiceFake.Verify();
    }

    [Test]
    public void Build_create_always_Equity_stock()
    {
      _builder = new StockViewModelEquityBuilder(_configurationServiceFake.Object, new Stock(), 12);

      var result = _builder.Build();

      Assert.AreEqual(StockTypeViewModel.Equity, result.Type);
    }

    [Test]
    public void Build_set_when_TransactionCost_is_greater_then_EquityToleranceTransactionCost_stock()
    {
      _builder = new StockViewModelEquityBuilder(_configurationServiceFake.Object, new Stock { TransactionCost = 120 }, 12);
      _configurationServiceFake.SetupGet(x => x.EquityToleranceTransactionCost).Returns(119).Verifiable();

      var result = _builder.Build();

      Assert.IsTrue(result.IsHighlighted);
    }

    [Test]
    public void Build_set_when_TransactionCost_is_lower_then_MarketValueTolerance_stock()
    {
      _configurationServiceFake.SetupGet(x => x.MarketValueTolerance).Returns(121).Verifiable();
      _builder = new StockViewModelEquityBuilder(_configurationServiceFake.Object, new Stock { MarketValue = 120 }, 12);

      var result = _builder.Build();

      Assert.IsTrue(result.IsHighlighted);
    }

    [Test]
    public void Build_map_all_fields_stock()
    {
      var totalMarketValue = 12;
      var stock = new Stock
      {
        Type = StockType.Equity,
        MarketValue = 234.01M,
        TransactionCost = 12.01M,
        Name = "name1",
        Price = 12.01M,
        Quantity = 123
      };

      _configurationServiceFake.SetupGet(x => x.EquityToleranceTransactionCost).Returns((int)(stock.TransactionCost + 1)).Verifiable();
      _builder = new StockViewModelEquityBuilder(_configurationServiceFake.Object, stock, totalMarketValue);

      var result = _builder.Build();

      Assert.IsFalse(result.IsHighlighted);
      Assert.AreEqual((int)stock.Type, (int)result.Type);
      Assert.AreEqual(stock.MarketValue, result.MarketValue);
      Assert.AreEqual(stock.Name, result.Name);
      Assert.AreEqual(stock.TransactionCost, result.TransactionCost);
      Assert.AreEqual(stock.Price, result.Price);
      Assert.AreEqual(stock.Quantity, result.Quantity);
      Assert.AreEqual(stock.MarketValue / totalMarketValue, result.StockWeight);
    }
  }
}