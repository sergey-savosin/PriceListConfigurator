using System;
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
            MessageBox.Show("SearchText is empty", "Attention");
        }
    }
}
