using System.Windows;
using FundManager.Models;
using FundManager.Services;
using FundManager.Services.Interfaces;

namespace FundManager.Views
{
  /// <summary>
  /// Interaction logic for AddStockWindow.xaml
  /// </summary>
  public partial class AddStockWindow : Window
  {
    private readonly IStockService _stockService;

    private readonly StockType _stockType;

    //Todo: CalsteWindsor/Ninject
    public AddStockWindow(StockType stockType)
      : this(stockType, new StockService())
    {
    }

    public AddStockWindow(StockType stockType, IStockService stockService)
    {
      _stockType = stockType;
      _stockService = stockService;

      InitializeComponent();

      Title = string.Format("Add new {0}", _stockType);
    }

    private async void AddBtnClick(object sender, RoutedEventArgs e)
    {
      Stock stock = new Stock();

      if (IsValidData() == false)
      {
        return;
      }

      stock.Type = _stockType;
      stock.Price = PriceDecimalUpDown.Value.Value;
      stock.Quantity = QuantityIntegerUpDown.Value.Value;

      await _stockService.AddStockAsync(stock);

      Close();
    }

    //ToDo: FluentValidation
    private bool IsValidData()
    {
      if (!PriceDecimalUpDown.Value.HasValue || PriceDecimalUpDown.Value.Value < 0.01M)
      {
        MessageBox.Show("Price must be greater then 0.01");
        return false;
      }

      if (!QuantityIntegerUpDown.Value.HasValue || QuantityIntegerUpDown.Value.Value < 1)
      {
        MessageBox.Show("Quantity must be greater then 0");
        return false;
      }

      return true;
    }

    private void CancelBtnClick(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
