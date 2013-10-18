using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using CloudStorageAccount = Microsoft.WindowsAzure.Storage.CloudStorageAccount;

namespace Azure.Helpers
{
  /// <summary>
  /// Utility class for interacting with Table Storage.
  /// </summary>
  public class TableUtility : StorageBase, ITableUtility
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

    /// <summary>
    /// Insert a record into Table Storage
    /// </summary>
    /// <param name="table">A CloudTable instance</param>
    /// <param name="entity">The entity to insert</param>
    /// <returns>Boolean representing success of the operation</returns>
    public bool Insert(CloudTable table, ITableEntity entity)
    {
      // Create the TableOperation that inserts the customer entity.
      var insertOperation = TableOperation.Insert(entity);

      // Execute the insert operation.
      var result = table.Execute(insertOperation);

      return (result.HttpStatusCode == 200);
    }

    /// <summary>
    /// Query a Table Storage table
    /// </summary>
    /// <param name="table">An instance of Cloud Table</param>
    /// <param name="queryString">A query string to execute against the table</param>
    /// <typeparam name="T">A type that is derived from ITableEntity</typeparam>
    /// <returns>An enumerable of T</returns>
    public IEnumerable<T> Query<T>(CloudTable table, String queryString) where T : ITableEntity, new()
    {
      // Construct the query operation for all customer entities where PartitionKey="Smith".
      var query = new TableQuery<T>().Where(queryString);

      return table.ExecuteQuery(query);
    }

    /// <summary>
    /// Find one entry from a CloudTable instance
    /// </summary>
    /// <param name="table">An instance of CloudTable</param>
    /// <param name="partitionKey">The partition key to retrieve the record from</param>
    /// <param name="rowKey">The row key to retrieve the record from</param>
    /// <typeparam name="T">A type derived from ITableEntity</typeparam>
    /// <returns>An object if successful, null if it is not</returns>
    public object FineOne<T>(CloudTable table, String partitionKey, String rowKey) where T : ITableEntity, new()
    {
      // Create a retrieve operation that takes a customer entity.
      var retrieveOperation = TableOperation.Retrieve<T>(partitionKey,rowKey);

      // Execute the retrieve operation.
      var retrievedResult = table.Execute(retrieveOperation);

      // Print the phone number of the result.
      if (retrievedResult.Result != null)
      {
        return (T)retrievedResult.Result;
      }

      return null;
    }

    /// <summary>
    /// Update an entty stored in TableStorage
    /// </summary>
    /// <param name="table">An instance of CloudTable where the entity is stored</param>
    /// <param name="entity">The entity to update</param>
    /// <typeparam name="T">A type derived from ITableEntity</typeparam>
    /// <returns>If successful, the updated object, on failure null</returns>
    public object Update<T>(CloudTable table, T entity) where T : ITableEntity, new()
    {
      // Create the InsertOrReplace TableOperation
      var updateOperation = TableOperation.Replace(entity);

      // Execute the operation.
      var result = table.Execute(updateOperation);

      if (result.HttpStatusCode == 200)
      {
        return entity;
      }

      return null;
    }

    /// <summary>
    /// Delete an entity from Table Storage
    /// </summary>
    /// <param name="table">An instance of CloudTable where the entity is stored</param>
    /// <param name="entity">The entity to delete</param>
    /// <typeparam name="T">A derived type from ITableEntity</typeparam>
    /// <returns>Successfulness of Delete operation</returns>
    public Boolean Delete<T>(CloudTable table, T entity) where T : ITableEntity, new()
    {
      var deleteOperation = TableOperation.Delete(entity);

      // Execute the operation.
      var result = table.Execute(deleteOperation);

      return (result.HttpStatusCode == 200);
    }
  }
}
