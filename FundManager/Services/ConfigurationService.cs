using System;
using System.Configuration;
using FundManager.Services.Interfaces;

namespace FundManager.Services
{
  public class ConfigurationService : IConfigurationService
  {
    public int BondToleranceTransactionCost
    {
      get { return Int32.Parse(ConfigurationManager.AppSettings["BondToleranceTransactionCost"]); }
    }

    public int EquityToleranceTransactionCost
    {
      get { return Int32.Parse(ConfigurationManager.AppSettings["EquityToleranceTransactionCost"]); }
    }

    public int MarketValueTolerance
    {
      get { return Int32.Parse(ConfigurationManager.AppSettings["MarketValueTolerance"]); }
    }
  }
}