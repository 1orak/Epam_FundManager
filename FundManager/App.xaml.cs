using System.Windows;
using log4net;

namespace FundManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
      private static readonly ILog _log =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

      public App()
      {
        Dispatcher.UnhandledException += OnDispatcherUnhandledException;
      }

      void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
      {
        _log.Error("An unhandled exception occurred.", e.Exception);
        MessageBox.Show("An unhandled exception occurred", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
      }
    }
}
