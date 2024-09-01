using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace PriceListConfigurator.ViewModel.Commands
{
    public class DoTestCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        public DoTestCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            string serverName = _viewModel.SelectedConnectionItem.ServerName;
            string databaseName = _viewModel.SelectedConnectionItem.DatabaseName;

            var con = GetOpenConnection(serverName, databaseName);

            MessageBox.Show($"Connection state: {con.State}", "Attention");
        }

        public static IDbConnection GetOpenConnection(string serverName, string databaseName)
        {
            var b = new SqlConnectionStringBuilder();
            b.DataSource = serverName;
            b.InitialCatalog = databaseName;
            b.IntegratedSecurity = true;

            var connection = new SqlConnection(b.ToString());
            connection.Open();
            return connection;
        }
    }
}
