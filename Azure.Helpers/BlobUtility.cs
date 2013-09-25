using System;
using System.Text;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Azure.Helpers
{
    /// <summary>
    /// Utility class for interacting with blobs including getting content, putting, copying, and deleting
    /// </summary>
    public class BlobUtility : StorageBase
    {
        /// <summary>
        /// Constructor takes a connection string as a string and a container name
        /// </summary>
        /// <param name="connectionString">The connection string for the Azure Storage Account</param>
        /// <param name="containerName">The Blob Container</param>
        public BlobUtility(string connectionString, string containerName)
        {
            ConnectionString = connectionString;
            ContainerName = containerName;
        }

        /// <summary>
        /// Tracks the container name
        /// </summary>
        public string ContainerName = string.Empty;

        /// <summary>
        /// Read-only property for retrieving the blob container
        /// </summary>
        public CloudBlobContainer BlobContainer
        {
            get
            {
                var container = BlobClient.GetContainerReference(ContainerName);
                container.CreateIfNotExists();
                return container;
            }
        }

        private CloudBlobClient BlobClient
        {
            get
            {
                return Account.CreateCloudBlobClient();
            }

        }

        #region Instance Methods
        /// <summary>
        /// Put (create or update) a blob from a memory stream.
        /// </summary>
        /// <param name="stream">Memory stream to put to a blob</param>
        /// <param name="blobPath">Path to the blob</param>
        /// <param name="contentType">Content type of the blob</param>
        /// <param name="permissions">Blob Container Permissions for the blob</param>
        /// <returns></returns>
        public string PutBlob(MemoryStream stream, string blobPath, string contentType, BlobContainerPermissions permissions)
        {
            try
            {
                var blob = BlobContainer.GetBlockBlobReference(blobPath);

                // Set proper content type on the blog
                blob.Properties.ContentType = contentType;

                // Set the permissions for the file to be accessible through the internet
                BlobContainer.SetPermissions(permissions);

                // Upload to the Blob
                blob.UploadFromStream(stream);

                // Return the blob path
                return blob.Uri.ToString();
            }
            catch (StorageException ex)
            {

                if (ex.RequestInformation.HttpStatusCode == 404)
                {
                    return string.Empty;
                }

                throw;
            }

        }

        /// <summary>
        /// Put (create or update) a blob from a string.
        /// </summary>
        /// <param name="content">Content to put to the blob</param>
        /// <param name="blobPath">Path to the blob</param>
        /// <param name="contentType">Content type of the blob</param>
        /// <param name="permissions">Blob Container Permissions for the blob</param>
        /// <returns>True on success, false if unable to create</returns>
        public string PutBlob(string content, string blobPath, string contentType, BlobContainerPermissions permissions)
        {
            try
            {
                var blob = BlobContainer.GetBlockBlobReference(blobPath);

                var bytes = Encoding.Unicode.GetBytes(content);

                var ms = new MemoryStream(bytes);

                PutBlob(ms, blobPath, contentType, permissions);

                return blob.Uri.ToString();
            }
            catch (StorageException ex)
            {
                if (ex.RequestInformation.HttpStatusCode == 404)
                {
                    return string.Empty;
                }

                throw;
            }
        }

        /// <summary>
        /// Retrieve the specified blob
        /// </summary>
        /// <param name="blobAddress">Address of blob to retrieve</param>
        /// <returns>Stream containing blob</returns>
        public MemoryStream GetBlob(string blobAddress)
        //public static Stream GetBlob(string blobAddress)
        {
            var stream = new MemoryStream();

            BlobContainer.GetBlockBlobReference(blobAddress).DownloadToStream(stream);

            return stream;
        }

        /// <summary>
        /// Copy a blob.
        /// </summary>
        /// <param name="sourceBlobName">Source blob</param>
        /// <param name="destBlobName">Destination blob</param>
        /// <returns>True on success, false if unable to create</returns>
        public bool CopyBlob(string sourceBlobName, string destBlobName)
        {
            try
            {
                var sourceBlob = BlobContainer.GetBlockBlobReference(sourceBlobName);
                var destBlob = BlobContainer.GetBlockBlobReference(destBlobName);
                destBlob.StartCopyFromBlob(sourceBlob); // async
                return true;
            }
            catch (StorageException ex)
            {
                if (ex.RequestInformation.HttpStatusCode == 404)
                {
                    return false;
                }

                throw;
            }
        }

        /// <summary>
        /// Delete a blob.
        /// </summary>
        /// <param name="blobName">Name of blob to delete from container</param>
        /// <returns>True on success, false if unable to create</returns>
        public bool DeleteBlob(string blobName)
        {
            try
            {
                var blob = BlobContainer.GetBlockBlobReference(blobName);
                blob.Delete();
                return true;
            }
            catch (StorageException ex)
            {
                if (ex.RequestInformation.HttpStatusCode == 404)
                {
                    return false;
                }

                throw;
            }
        }

        /// <summary>
        /// Checks if a blob exists in the current container
        /// </summary>
        /// <param name="blobPath">Path to the blob</param>
        /// <returns>True on success, false if unable to create</returns>
        public bool Exists(string blobPath)
        {
            var blob = BlobContainer.GetBlockBlobReference(blobPath);

            try
            {
                blob.FetchAttributes();
                return true;
            }
            catch (StorageException ex)
            {
                if (ex.RequestInformation.HttpStatusCode == 404)
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Renames a blob
        /// </summary>
        /// <param name="origBlobName">Original blob name</param>
        /// <param name="newBlobName">New blob name</param>
        /// <returns>True on success, false if unable to create</returns>
        public bool RenameBlob(string origBlobName, string newBlobName)
        {
            try
            {
                if (CopyBlob(origBlobName, newBlobName))
                {
                    return DeleteBlob(origBlobName);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        #endregion
    }

}
