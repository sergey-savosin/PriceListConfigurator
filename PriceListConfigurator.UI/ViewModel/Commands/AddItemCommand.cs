using PriceListConfigurator.Model;
using System;
using System.Windows.Input;

namespace PriceListConfigurator.ViewModel.Commands
{
    public class AddItemCommand : ICommand
    {
        // Member variables
        private readonly SetupConnectionViewModel m_ViewModel;

        public AddItemCommand(SetupConnectionViewModel viewModel)
        {
            m_ViewModel = viewModel;
        }

        /// <summary>
        /// Whether this command can be executed.
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Fires when the CanExecute status of this command changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Invokes this command to perform its intended task.
        /// </summary>
        public void Execute(object parameter)
        {
            Connection newItem = new Connection("New connection");
            m_ViewModel.ConnectionList.Add(newItem);
        }
    }
}
