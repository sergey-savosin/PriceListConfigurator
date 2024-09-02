using Autofac;
using PriceListConfiguratorUI.Startup;
using System;
using System.Windows;
using System.Windows.Threading;

namespace PriceListConfiguratorUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var boot = new Bootstrapper();
            var container = boot.Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ShowUnhandledException(e);
        }

        private void ShowUnhandledException(DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            string exceptionMessage = e.Exception.Message +
                "\n\n--- Inner exception: ---\n\n" +
                e.Exception.InnerException?.Message ?? string.Empty;


            MessageBox.Show(exceptionMessage, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }
}
