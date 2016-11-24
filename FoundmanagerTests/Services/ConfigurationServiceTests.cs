using FundManager.Services;
using NUnit.Framework;

namespace FoundmanagerTests.Services
{
  [TestFixture]
  public class ConfigurationServiceTests
  {
    private ConfigurationService _configurationService;

    [SetUp]
    public void SetUp()
    {
      _configurationService = new ConfigurationService();
    }

    [Test]
    public void BondToleranceTransactionCost_return_data_successfully()
    {
      var result = _configurationService.BondToleranceTransactionCost;

      Assert.AreEqual(100000, result);
    }

    [Test]
    public void EquityToleranceTransactionCost_return_data_successfully()
    {
      var result = _configurationService.EquityToleranceTransactionCost;

      Assert.AreEqual(200000, result);
    }

    [Test]
    public void MarketValueTolerance_return_data_successfully()
    {
      var result = _configurationService.MarketValueTolerance;

      Assert.AreEqual(0, result);
    }
  }
}