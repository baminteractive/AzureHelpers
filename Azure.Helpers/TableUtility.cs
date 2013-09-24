using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace Azure.Helpers
{
    public class TableUtility : StorageBase
    {
        public string ContainerName = string.Empty;

        public TableUtility(string connectionString)
        {
            ConnectionString = connectionString;
            _tableClient = TableClient;
        }

        private CloudTableClient _tableClient;

        public CloudTableClient TableClient
        {
            get
            {
                var storageAccount = CloudStorageAccount.Parse(ConnectionString);

                _tableClient = storageAccount.CreateCloudTableClient();

                // Set retry policy
                _tableClient.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 4);

                return _tableClient;
            }
        }

        public CloudTable GetTable(string tableName)
        {

            var table = _tableClient.GetTableReference(tableName);
            /*if (!table.Exists())
            {
                table.Create();
            }*/

            return table;
        }
    }
}
