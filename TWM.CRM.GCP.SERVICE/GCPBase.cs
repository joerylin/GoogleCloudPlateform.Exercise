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

		/// <summary>
		/// 從Google Cloud Storage 下載檔案
		/// </summary>
		/// <param name="gcp_path">Google Cloud Storage buckets下檔案的路徑</param>
		/// <param name="local_path">本地端路徑檔案的路徑</param>
		/// <exception cref="FileNotFoundException">FileNotFoundException</exception>
		public abstract void PullFile(string gcp_path, string local_path);

		/// <summary>
		/// 上傳檔案至Google Cloud Storage
		/// </summary>
		/// <param name="gcp_path">Google Cloud Storage buckets下檔案的路徑</param>
		/// <param name="local_path">本地端路徑檔案的路徑</param>
		public abstract void PushFile(string gcp_path, string local_path);

		/// <summary>
		/// 判斷Google Cloud Storage bucket內檔案是否存在
		/// </summary>
		/// <param name="gcp_path">Google Cloud Storage buckets下檔案的路徑</param>
		/// <returns></returns>
		public abstract bool ExistsFile(string gcp_path);


		/// <summary>
		///  刪除檔案
		/// <param name="gcp_path">Google Cloud Storage buckets下檔案的路徑</param>
		public abstract void DeleteFile(string gcp_path);

		/// <summary>
		/// 列出 Google Cloud Storage bucket內的物件檔案清單
		/// </summary>
		/// <param name="gcp_path">Google Cloud Storage buckets下資料夾的路徑</param>
		/// <param name="prefix">前綴字</param>
		/// <param name="extension">附檔名eg.   : csv</param>
		/// <returns>物件集合LIST</returns>
		public abstract List<Object> GetResultObjects(string gcp_path, string prefix = null, string extension = null, Boolean search_sub_dir_ind = false);


		~GCPBase()
		{
			_client.Dispose();
		}
	}

}
