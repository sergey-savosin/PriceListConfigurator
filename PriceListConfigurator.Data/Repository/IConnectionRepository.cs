using PriceListConfigurator.Model;
using System.Collections.Generic;

namespace PriceListConfigurator.Data.Repository
{
    public interface IConnectionRepository
    {
        IEnumerable<Connection> ConnectionCollectionGetAll();

        void ConnectionCollectionSave();

        void UpdateAll(IList<Connection> items);
    }
}
