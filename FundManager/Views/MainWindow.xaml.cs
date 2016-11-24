using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using FundManager.Models;
using FundManager.Services;
using FundManager.Services.Interfaces;
using FundManager.ViewModels;

namespace FundManager.Views
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private readonly IStockService _stockService;
    private readonly IStockStatisticsService _stockStatisticsService;

    private StockTypeViewModel _stockTypeFilter;

    public MainWindow() : this(new StockService(), new StockStatisticsService())
    {
    }

    public  MainWindow(IStockService stockService, IStockStatisticsService stockStatisticsService)
    {
      _stockService = stockService;
      _stockStatisticsService = stockStatisticsService;

      InitializeComponent();
    }

    private async void AddEquityBtnClick(object sender, RoutedEventArgs e)
    {
      AddStockWindow addStockWindow = new AddStockWindow(StockType.Equity);

      addStockWindow.ShowDialog();
      StocksDataGrid.ItemsSource = new ObservableCollection<StockViewModel>(await _stockService.GetStocksAsync());
      RefreshStatistics();
    }

    private async void AddBondBtnClick(object sender, RoutedEventArgs e)
    {
      AddStockWindow addStockWindow = new AddStockWindow(StockType.Bond);

      addStockWindow.ShowDialog();
      StocksDataGrid.ItemsSource = new ObservableCollection<StockViewModel>(await _stockService.GetStocksAsync());
      RefreshStatistics();
    }

    private void RefreshStatistics()
    {
      var stats = _stockStatisticsService.GetStockStatistics(_stockTypeFilter);

      TotalMarketValueLabel.Text = stats.MarketValue.ToString("N");
      TotalNumberLabel.Text = stats.Count.ToString("D");
      TotalStockWeightLabel.Text = stats.Weight.ToString("P");
    }

    private void SummaryFilterCmbSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      _stockTypeFilter = (StockTypeViewModel)(sender as ComboBox).SelectedIndex;

      RefreshStatistics();
    }

    private async void MainWindowLoadedEvent(object sender, RoutedEventArgs e)
    {
      StocksDataGrid.ItemsSource = new ObservableCollection<StockViewModel>(await _stockService.GetStocksAsync());

      SummaryFilterCmb.DataContext = new List<string> { "All", "Equity", "Bond" };
      _stockTypeFilter = StockTypeViewModel.All;
      SummaryFilterCmb.SelectedIndex = (int)_stockTypeFilter;
    }
  }
}
