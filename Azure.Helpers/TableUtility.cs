using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace Azure.Helpers
{
  /// <summary>
  /// Utility class for interacting with Table Storage.
  /// </summary>
  public class TableUtility : StorageBase
  {
    /// <summary>
    /// Tracks the current container name for the Blob Client
    /// </summary>
    public string ContainerName = string.Empty;

    /// <summary>
    /// Constructor takes a connection string to an Azure Storage Account
    /// </summary>
    /// <param name="connectionString">Connection string for an Azure Storage Account</param>
    public TableUtility(string connectionString)
    {
      ConnectionString = connectionString;
      _tableClient = TableClient;
    }

    private CloudTableClient _tableClient;

    /// <summary>
    /// Read-only property for accessing the Table Client.  Uses the connection string passed in from the constructor
    /// </summary>
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

    /// <summary>
    /// Get a table reference from the container
    /// </summary>
    /// <param name="tableName">Name of table to fetch</param>
    /// <returns>CloudTable instance for the tableName</returns>
    public CloudTable GetTable(string tableName)
    {

      var table = _tableClient.GetTableReference(tableName);

      return table;
    }
  }
}
