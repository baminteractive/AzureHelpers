using Microsoft.WindowsAzure.Storage;

namespace Azure.Helpers
{
  /// <summary>
  /// Base class for interacting with Azure Cloud Storage
  /// </summary>
  public abstract class StorageBase
  {
    /// <summary>
    /// Tracks the connection string for the Cloud Storage Account
    /// </summary>
    protected string ConnectionString
    {
      get;
      set;
    }

    #region Account

    private CloudStorageAccount _account;

    /// <summary>
    /// Property for setting the Cloud Storage Account
    /// </summary>
    public CloudStorageAccount Account
    {
      get { return _account ?? (_account = CloudStorageAccount.Parse(ConnectionString)); }
      set { _account = value; }
    }

    #endregion

  }
}