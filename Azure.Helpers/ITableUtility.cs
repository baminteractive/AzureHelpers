using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Azure.Helpers
{
  /// <summary>
  /// Interface for TableUtility
  /// </summary>
  public interface ITableUtility
  {
    /// <summary>
    /// Read-only property for accessing the Table Client.  Uses the connection string passed in from the constructor
    /// </summary>
    CloudTableClient TableClient { get; }

    /// <summary>
    /// Property for setting the Cloud Storage Account
    /// </summary>
    CloudStorageAccount Account { get; set; }

    /// <summary>
    /// Get a table reference from the container
    /// </summary>
    /// <param name="tableName">Name of table to fetch</param>
    /// <returns>CloudTable instance for the tableName</returns>
    CloudTable GetTable(string tableName);

    /// <summary>
    /// Insert a record into Table Storage
    /// </summary>
    /// <param name="table">A CloudTable instance</param>
    /// <param name="entity">The entity to insert</param>
    /// <returns>Boolean representing success of the operation</returns>
    bool Insert(CloudTable table, ITableEntity entity);

    /// <summary>
    /// Query a Table Storage table
    /// </summary>
    /// <param name="table">An instance of Cloud Table</param>
    /// <param name="queryString">A query string to execute against the table</param>
    /// <typeparam name="T">A type that is derived from ITableEntity</typeparam>
    /// <returns>An enumerable of T</returns>
    IEnumerable<T> Query<T>(CloudTable table, String queryString) where T : ITableEntity, new();

    /// <summary>
    /// Find one entry from a CloudTable instance
    /// </summary>
    /// <param name="table">An instance of CloudTable</param>
    /// <param name="partitionKey">The partition key to retrieve the record from</param>
    /// <param name="rowKey">The row key to retrieve the record from</param>
    /// <typeparam name="T">A type derived from ITableEntity</typeparam>
    /// <returns>An object if successful, null if it is not</returns>
    object FineOne<T>(CloudTable table, String partitionKey, String rowKey) where T : ITableEntity, new();

    /// <summary>
    /// Update an entty stored in TableStorage
    /// </summary>
    /// <param name="table">An instance of CloudTable where the entity is stored</param>
    /// <param name="entity">The entity to update</param>
    /// <typeparam name="T">A type derived from ITableEntity</typeparam>
    /// <returns>If successful, the updated object, on failure null</returns>
    object Update<T>(CloudTable table, T entity) where T : ITableEntity, new();

    /// <summary>
    /// Delete an entity from Table Storage
    /// </summary>
    /// <param name="table">An instance of CloudTable where the entity is stored</param>
    /// <param name="entity">The entity to delete</param>
    /// <typeparam name="T">A derived type from ITableEntity</typeparam>
    /// <returns>Successfulness of Delete operation</returns>
    Boolean Delete<T>(CloudTable table, T entity) where T : ITableEntity, new();
  }
}