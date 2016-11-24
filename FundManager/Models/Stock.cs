namespace FundManager.Models
{
  public class Stock
  {
    public StockType Type { get; set; }

    public string Name { get; set; } 

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal MarketValue{ get; set; }

    public decimal TransactionCost { get; set; }
  }
}
