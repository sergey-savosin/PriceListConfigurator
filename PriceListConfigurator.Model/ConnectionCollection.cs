using System.Collections.Generic;

namespace PriceListConfigurator.Model
{
    public class ConnectionCollection
    {
        List<Connection> connectionCollection = new List<Connection>();

        public List<Connection> Connections
        {
            get { return connectionCollection; }
            set { connectionCollection = value; }
        }
    }
}
