namespace FundManager.ViewModels
{
  public class StockViewModel
  {
    public StockTypeViewModel Type { get; set; }

    public string Name { get; set; } 

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal MarketValue{ get; set; }

    public decimal TransactionCost { get; set; }

    public decimal StockWeight { get; set; }

    public bool IsHighlighted { get; set; }
  }
}
