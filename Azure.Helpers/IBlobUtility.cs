using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Azure.Helpers
{
  /// <summary>
  /// Utility class for interacting with blobs including getting content, putting, copying, and deleting
  /// </summary>
  public interface IBlobUtility
  {
    /// <summary>
    /// Read-only property for retrieving the blob container
    /// </summary>
    CloudBlobContainer BlobContainer { get; }

    /// <summary>
    /// Property for setting the Cloud Storage Account
    /// </summary>
    CloudStorageAccount Account { get; set; }

    /// <summary>
    /// Intializes the instance of BlobUtility
    /// </summary>
    /// <param name="connectionString">The connection string to use for Blog Storage</param>
    /// <param name="containerName">The container that will be accessed for blob storage</param>
    void Initialize(string connectionString, string containerName);

    /// <summary>
    /// Put (create or update) a blob from a memory stream.
    /// </summary>
    /// <param name="stream">Memory stream to put to a blob</param>
    /// <param name="blobPath">Path to the blob</param>
    /// <param name="contentType">Content type of the blob</param>
    /// <param name="permissions">Blob Container Permissions for the blob</param>
    /// <returns></returns>
    string PutBlob(MemoryStream stream, string blobPath, string contentType, Microsoft.WindowsAzure.StorageClient.BlobContainerPermissions permissions);

    /// <summary>
    /// Put (create or update) a blob from a string.
    /// </summary>
    /// <param name="content">Content to put to the blob</param>
    /// <param name="blobPath">Path to the blob</param>
    /// <param name="contentType">Content type of the blob</param>
    /// <param name="permissions">Blob Container Permissions for the blob</param>
    /// <returns>True on success, false if unable to create</returns>
    string PutBlob(string content, string blobPath, string contentType, Microsoft.WindowsAzure.StorageClient.BlobContainerPermissions permissions);

    /// <summary>
    /// Retrieve the specified blob
    /// </summary>
    /// <param name="blobAddress">Address of blob to retrieve</param>
    /// <returns>Stream containing blob</returns>
    MemoryStream GetBlob(string blobAddress)
      //public static Stream GetBlob(string blobAddress)
      ;

    /// <summary>
    /// Copy a blob.
    /// </summary>
    /// <param name="sourceBlobName">Source blob</param>
    /// <param name="destBlobName">Destination blob</param>
    /// <returns>True on success, false if unable to create</returns>
    bool CopyBlob(string sourceBlobName, string destBlobName);

    /// <summary>
    /// Delete a blob.
    /// </summary>
    /// <param name="blobName">Name of blob to delete from container</param>
    /// <returns>True on success, false if unable to create</returns>
    bool DeleteBlob(string blobName);

    /// <summary>
    /// Checks if a blob exists in the current container
    /// </summary>
    /// <param name="blobPath">Path to the blob</param>
    /// <returns>True on success, false if unable to create</returns>
    bool Exists(string blobPath);

    /// <summary>
    /// Renames a blob
    /// </summary>
    /// <param name="origBlobName">Original blob name</param>
    /// <param name="newBlobName">New blob name</param>
    /// <returns>True on success, false if unable to create</returns>
    bool RenameBlob(string origBlobName, string newBlobName);
  }
}