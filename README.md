#Azure.Helpers

Azure.Helpers is a set of utility classes that make working with Azure Storage easier.  There are currently classes for Blob and Table storage.

##BlobUtility
###BlobUtility.#ctor(System.String,System.String)

Constructor takes a connection string as a string and a container name

####Params

&gt; **connectionString**: The connection string for the Azure Storage Account

&gt; **containerName**: The Blob Container

###BlobUtility.ContainerName

Tracks the container name

###BlobUtility.PutBlob(System.IO.MemoryStream,System.String,System.String,Microsoft.WindowsAzure.Storage.Blob.BlobContainerPermissions)

Put (create or update) a blob from a memory stream.

####Params

&gt; **stream**: Memory stream to put to a blob

&gt; **blobPath**: Path to the blob

&gt; **contentType**: Content type of the blob

&gt; **permissions**: Blob Container Permissions for the blob

###BlobUtility.PutBlob(System.String,System.String,System.String,Microsoft.WindowsAzure.Storage.Blob.BlobContainerPermissions)

Put (create or update) a blob from a string.

####Params

&gt; **content**: Content to put to the blob

&gt; **blobPath**: Path to the blob

&gt; **contentType**: Content type of the blob

&gt; **permissions**: Blob Container Permissions for the blob

###BlobUtility.GetBlob(System.String)

Retrieve the specified blob

####Params

&gt; **blobAddress**: Address of blob to retrieve

###BlobUtility.CopyBlob(System.String,System.String)

Copy a blob.

####Params

&gt; **sourceBlobName**: Source blob

&gt; **destBlobName**: Destination blob

###BlobUtility.DeleteBlob(System.String)

Delete a blob.

####Params

&gt; **blobName**: Name of blob to delete from container

###BlobUtility.Exists(System.String)

Checks if a blob exists in the current container

####Params

&gt; **blobPath**: Path to the blob

###BlobUtility.RenameBlob(System.String,System.String)

Renames a blob

####Params

&gt; **origBlobName**: Original blob name

&gt; **newBlobName**: New blob name

###BlobUtility.BlobContainer

Read-only property for retrieving the blob container


##StorageBase
###StorageBase.ConnectionString

Tracks the connection string for the Cloud Storage Account

###StorageBase.Account

Property for setting the Cloud Storage Account


##TableUtility
###TableUtility.ContainerName

Tracks the current container name for the Blob Client

###TableUtility.#ctor(System.String)

Constructor takes a connection string to an Azure Storage Account

####Params

&gt; **connectionString**: Connection string for an Azure Storage Account

###TableUtility.GetTable(System.String)

Get a table reference from the container

####Params

&gt; **tableName**: Name of table to fetch

###TableUtility.TableClient

Read-only property for accessing the Table Client. Uses the connection string passed in from the constructor


