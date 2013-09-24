using Microsoft.WindowsAzure.Storage;

namespace Azure.Helpers
{
	 public abstract class StorageBase
	 {
	     protected string ConnectionString
		{
			get;
			set;
		}

		#region Account

        private CloudStorageAccount _account;

		public CloudStorageAccount Account
		{
			get { return _account ?? (_account = CloudStorageAccount.Parse(ConnectionString)); }
		    set { _account = value; }
		}

		#endregion
	 
     }
}