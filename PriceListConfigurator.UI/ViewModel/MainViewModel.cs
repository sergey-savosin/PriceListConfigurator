using PriceListConfigurator.Data.Repository;
using PriceListConfigurator.Model;
using PriceListConfigurator.ViewModel.Commands;
using PriceListConfiguratorUI.UtilityClasses;
using PriceListConfiguratorUI.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PriceListConfigurator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IConnectionRepository _connectionRepository;
        private ObservableCollection<Connection> p_ConnectionList;
        private Connection p_SelectedItem;

        public MainViewModel(IConnectionRepository connectionRepository)
        {
            _connectionRepository = connectionRepository;
            DoTest = new DoTestCommand(this);
            ShowSetupConnectionWindow = new ShowSetupConnectionWindowCommand(this);
            p_ConnectionList = new ObservableCollection<Connection>();
        }
        
        public ICommand DoTest { get; }

        public ICommand ShowSetupConnectionWindow { get; }

        public ObservableCollection<Connection> ConnectionList
        {
            get { return p_ConnectionList; }

            set
            {
                p_ConnectionList = value;
                base.RaisePropertyChangedEvent("ConnectionList");
            }
        }

        public Connection SelectedConnectionItem
        {
            get { return p_SelectedItem; }
            set
            {
                p_SelectedItem = value;
                base.RaisePropertyChangedEvent("SelectedConnectionItem");
            }
        }

        public int SelectedConnectionIndex { get; set; }

        internal void Load()
        {
            p_ConnectionList.Clear();

            // Add items to the list
            var items = _connectionRepository.ConnectionCollectionGetAll();
            foreach (var item in items)
            {
                p_ConnectionList.Add(
                    new Connection
                    {
                        Name = item.Name,
                        SequenceNumber = item.SequenceNumber,
                        ServerName = item.ServerName
                    });
            }

            SelectedConnectionItem = ConnectionList.FirstOrDefault(t => t.SequenceNumber == 1);
        }
    }
}
