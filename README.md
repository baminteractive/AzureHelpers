#Azure.Helpers

![alt text][logo]

Azure.Helpers is a set of utility classes that make working with Azure Storage easier.  There are currently classes for Blob and Table storage.

##BlobUtility
###BlobUtility.#ctor(System.String,System.String)

Constructor takes a connection string as a string and a container name

####Params

> **connectionString**: 

> **containerName**: 

###BlobUtility.ContainerName

Tracks the container name

###BlobUtility.PutBlob(System.IO.MemoryStream,System.String,System.String,Microsoft.WindowsAzure.Storage.Blob.BlobContainerPermissions)

Put (create or update) a blob from a memory stream.

####Params

> **stream**: Memory stream to put to a blob

> **blobPath**: Path to the blob

> **contentType**: Content type of the blob

> **permissions**: Blob Container Permissions for the blob

###BlobUtility.PutBlob(System.String,System.String,System.String,Microsoft.WindowsAzure.Storage.Blob.BlobContainerPermissions)

Put (create or update) a blob from a string.

####Params

> **content**: Content to put to the blob

> **blobPath**: Path to the blob

> **contentType**: Content type of the blob

> **permissions**: Blob Container Permissions for the blob

###BlobUtility.GetBlob(System.String)

Retrieve the specified blob

####Params

> **blobAddress**: Address of blob to retrieve

###BlobUtility.CopyBlob(System.String,System.String)

Copy a blob.

####Params

> **sourceBlobName**: Source blob

> **destBlobName**: Destination blob

###BlobUtility.DeleteBlob(System.String)

Delete a blob.

####Params

> **blobName**: Name of blob to delete from container

###BlobUtility.Exists(System.String)

Checks if a blob exists in the current container

####Params

> **blobPath**: Path to the blob

###BlobUtility.RenameBlob(System.String,System.String)

Renames a blob

####Params

> **origBlobName**: Original blob name

> **newBlobName**: New blob name

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

> **connectionString**: Connection string for an Azure Storage Account

###TableUtility.GetTable(System.String)

Get a table reference from the container

####Params

> **tableName**: Name of table to fetch

###TableUtility.TableClient

Read-only property for accessing the Table Client. Uses the connection string passed in from the constructor

[logo]: http://teamcity.bamads.com:8111/app/rest/builds/buildType:(id:AzureHelpers_BuildAndPublishToNuget)/statusIcon&v=1
