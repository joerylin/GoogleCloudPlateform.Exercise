using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace GCP.SERVICE
{
	public abstract class GCPBase
	{
		protected string bucketName = "your-unique-bucket-name";
		private GoogleCredential _credential;
		protected StorageClient _client;

		public GoogleCredential Credential
		{
			get
			{
				return _credential;
			}
		}



		public void SetGCPService(string _buketname, string _googleCredentialJsonFilePath = "GoogleCredential/surveycake-twm-1f6e0a95a707.json")
		{
			bucketName = _buketname;
			_credential = GoogleCredential.FromJson(File.ReadAllText(_googleCredentialJsonFilePath));
			_client = StorageClient.Create(_credential);
		}
		public string getObjectFilename(string name)
		{
			string[] array = name.Split("/", StringSplitOptions.RemoveEmptyEntries);
			return array[array.Length - 1];
		}

		public abstract void PullFile(string gcp_path, string local_path);
		public abstract void PushFile(string gcp_path, string local_path);

		public abstract bool ExistsFile(string gcp_path);

		public abstract List<Object> GetResultObjects(string gcp_path, string prefix = null, string extension = null);


		~GCPBase()
		{
			_client.Dispose();
		}
	}

}
