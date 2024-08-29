using PriceListConfigurator.Data.Repository;
using PriceListConfigurator.Model;
using PriceListConfigurator.ViewModel.Commands;
using PriceListConfigurator.ViewModel.Services;
using PriceListConfiguratorUI.UtilityClasses;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PriceListConfigurator.ViewModel
{
    public class SetupConnectionViewModel : ViewModelBase
    {
        private readonly IConnectionRepository _connectionRepository;
        private ObservableCollection<Connection> p_ConnectionList;
        private int p_ItemCount;

        public SetupConnectionViewModel(IConnectionRepository connectionRepository)
        {
            _connectionRepository = connectionRepository;
            this.Initialize();
        }

        /// <summary>
        /// Deletes the currently-selected item from the Connection List.
        /// </summary>
        public ICommand DeleteItem { get; set; }

        public ICommand AddItem { get; set; }

        /// <summary>
        /// A Connection list.
        /// </summary>
        public ObservableCollection<Connection> ConnectionList
        {
            get { return p_ConnectionList; }

            set
            {
                p_ConnectionList = value;
                base.RaisePropertyChangedEvent("ConnectionList");
            }
        }

        /// <summary>
        /// The currently-selected Connection item.
        /// </summary>
        public Connection SelectedItem { get; set; }

        /// <summary>
        /// The number of items in the Connection list.
        /// </summary>

        public int ItemCount
        {
            get { return p_ItemCount; }

            set
            {
                p_ItemCount = value;
                base.RaisePropertyChangedEvent("ItemCount");
            }
        }

        /// <summary>
        /// Updates the ItemCount Property when the ConnectionList collection changes.
        /// </summary>
        void OnConnectionListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update item count
            this.ItemCount = this.ConnectionList.Count;

            // Resequence list
            SequencingService.SetCollectionSequence(this.ConnectionList);
        }

        /// <summary>
        /// Initializes this application.
        /// </summary>
        private void Initialize()
        {
            // Initialize commands
            this.DeleteItem = new DeleteItemCommand(this);
            this.AddItem = new AddItemCommand(this);

            // Create connection list
            p_ConnectionList = new ObservableCollection<Connection>();

            // Subscribe to CollectionChanged event
            p_ConnectionList.CollectionChanged += OnConnectionListChanged;

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

            // Initialize list index
            this.ConnectionList = SequencingService.SetCollectionSequence(this.ConnectionList);

            // Update bindings
            base.RaisePropertyChangedEvent("ConnectionList");
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            _connectionRepository.UpdateAll(this.ConnectionList);
            _connectionRepository.ConnectionCollectionSave();
        }
    }
}
