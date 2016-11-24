using System;
using FundManager.Builders;
using FundManager.Models;
using NUnit.Framework;

namespace FoundmanagerTests.Builders
{
  [TestFixture]
  public class StockViewModelBuilderFactoryTests
  {
    private StockViewModelBuilderFactory _stockViewModelBuilderFactory;

    [SetUp]
    public void SetUp()
    {
      _stockViewModelBuilderFactory = new StockViewModelBuilderFactory();
    }

    [Test]
    public void Create_builder_for_bond()
    {
      Stock stock = new Stock {Type = StockType.Bond};

      var result = _stockViewModelBuilderFactory.Create(stock, 123);

      Assert.IsTrue(result is StockViewModelBondBuilder);
    }

    [Test]
    public void Create_builder_for_equity()
    {
      Stock stock = new Stock { Type = StockType.Equity };

      var result = _stockViewModelBuilderFactory.Create(stock, 123);

      Assert.IsTrue(result is StockViewModelEquityBuilder);
    }

    [Test]
    public void Create_throws_NotSupportedException_for_not_implemented_method()
    {
      Stock stock = new Stock();

      Assert.Throws<NotSupportedException>(() => _stockViewModelBuilderFactory.Create(stock, 123));
    }
  }
}