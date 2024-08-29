using PriceListConfigurator.Model.UtilityClasses;

namespace PriceListConfigurator.Model
{
    public class Connection : ObservableObject, ISequencedObject
    {
        private int _sequenceNumber;
        private string _name;
        private string _serverName;

        public int SequenceNumber
        {
            get { return _sequenceNumber; }
            set
            {
                _sequenceNumber = value;
                base.RaisePropertyChangedEvent("SequenceNumber");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                base.RaisePropertyChangedEvent("Name");
            }
        }

        public string ServerName
        {
            get { return _serverName; }
            set
            {
                _serverName = value;
                base.RaisePropertyChangedEvent("ServerName");
            }
        }

        public Connection()
        {
        }

        public Connection(string name)
        {
            Name = name;
        }

        public Connection(string name, int sequenceNumber, string serverName)
        {
            SequenceNumber = sequenceNumber;
            Name = name;
            ServerName = serverName;
        }
    }
}
