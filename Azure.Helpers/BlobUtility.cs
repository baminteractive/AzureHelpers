using System;
using System.Text;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Azure.Helpers
{
    public class BlobUtility : StorageBase
    {
        public BlobUtility(string connectionString, string containerName)
        {
            ConnectionString = connectionString;
            ContainerName = containerName;
        }

        public string ContainerName = string.Empty;

        public CloudBlobContainer BlobContainer
        {
            get
            {
                CloudBlobContainer container = BlobClient.GetContainerReference(ContainerName);
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

        public string PutBlob(MemoryStream stream, string blobPath, string contentType, BlobContainerPermissions permissions)
        {
            try
            {
                CloudBlockBlob blob = BlobContainer.GetBlockBlobReference(blobPath);

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

        // Put (create or update) a blob.
        // Return true on success, false if unable to create, throw exception on error.

        public string PutBlob(string content, string blobPath, string contentType, BlobContainerPermissions permissions)
        {
            try
            {
                CloudBlockBlob blob = BlobContainer.GetBlockBlobReference(blobPath);

                byte[] bytes = Encoding.Unicode.GetBytes(content);

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

        // Copy a blob.
        // Return true on success, false if unable to create, throw exception on error.

        public bool CopyBlob(string sourceBlobName, string destBlobName)
        {
            try
            {
                CloudBlockBlob sourceBlob = BlobContainer.GetBlockBlobReference(sourceBlobName);
                CloudBlockBlob destBlob = BlobContainer.GetBlockBlobReference(destBlobName);
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

        // Delete a blob.
        // Return true on success, false if unable to create, throw exception on error.

        public bool DeleteBlob(string blobName)
        {
            try
            {
                CloudBlockBlob blob = BlobContainer.GetBlockBlobReference(blobName);
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

        public bool Exists(string blobPath)
        {
            CloudBlockBlob blob = BlobContainer.GetBlockBlobReference(blobPath);

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
