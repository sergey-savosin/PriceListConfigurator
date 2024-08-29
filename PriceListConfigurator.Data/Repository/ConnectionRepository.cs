using PriceListConfigurator.Model;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PriceListConfigurator.Data.Repository
{
    public class ConnectionRepository : IConnectionRepository
    {
        private ConnectionCollection _data;
        const string settingsFileName = "ConnectionSetup.xml";

        public ConnectionRepository()
        {
            _data = new ConnectionCollection();
        }

        public IEnumerable<Connection> ConnectionCollectionGetAll()
        {
            LoadConnectionCollectionFromFile();
            return _data.Connections;
        }

        public void ConnectionCollectionSave()
        {
            SaveConnectionCollectionToFile();
        }

        public void UpdateAll(IList<Connection> items)
        {
            _data.Connections.Clear();
            _data.Connections.AddRange(items);
        }

        private void LoadConnectionCollectionFromFile()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            // ToDo: Debug.Assert(!string.IsNullOrWhiteSpace(path));

            string fullFileName = path + "\\" + settingsFileName;
            if (!File.Exists(fullFileName))
            {
                SaveConnectionCollectionToFile();
            }

            XmlSerializer xmlser = new XmlSerializer(typeof(ConnectionCollection));
            StreamReader sr = new StreamReader(fullFileName);
            _data = (ConnectionCollection)xmlser.Deserialize(sr);
            sr.Close();
        }

        private void SaveConnectionCollectionToFile()
        {
            // Сделать сохранение в файл
            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            //ToDo Debug.Assert(!string.IsNullOrWhiteSpace(path));
            string fullFileName = path + "\\" + settingsFileName;

            XmlSerializer xmlser = new XmlSerializer(typeof(ConnectionCollection));
            StreamWriter sw = new StreamWriter(fullFileName);
            xmlser.Serialize(sw, _data);
            sw.Close();
        }
    }
}
