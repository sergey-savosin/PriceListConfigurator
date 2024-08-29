using PriceListConfiguratorUI.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PriceListConfiguratorUI.ViewModel.Commands
{
    public class ShowSetupConnectionWindowCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        public ShowSetupConnectionWindowCommand(MainViewModel viewModel)
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
            var boot = new Bootstrapper();
            var container = boot.Bootstrap();

            var setupConnectionWindow = container.Resolve<SetupConnectionWindow>();

            setupConnectionWindow.ShowDialog();

            _viewModel.Load();
        }

    }
}
