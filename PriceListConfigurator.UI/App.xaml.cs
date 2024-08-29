using Autofac;
using PriceListConfiguratorUI.Startup;
using System;
using System.Windows;

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
            MessageBox.Show("Произошла ошибка." +
                Environment.NewLine + e.Exception.Message + "\r\n" + e.Exception.StackTrace, "Ошибка в приложении");
            e.Handled = true;

        }
    }
}
