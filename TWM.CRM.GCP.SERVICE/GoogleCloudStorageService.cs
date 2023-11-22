using CommonLibrary;
using Google;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace GCP.SERVICE
{
	public class GoogleCloudStorageService : GCPBase
	{

		/// <summary>
		/// 從Google Cloud Storage 下載檔案
		/// </summary>
		/// <param name="gcp_path">Google Cloud Storage buckets下檔案的路徑</param>
		/// <param name="local_path">本地端路徑檔案的路徑</param>
		/// <exception cref="FileNotFoundException">FileNotFoundException</exception>
		public override void PullFile(string gcp_path, string local_path)
		{
			if (ExistsFile(gcp_path))
			{
				//using StorageClient client = StorageClient.Create(base.Credential);
				using var outputFile = File.OpenWrite(local_path);
				_client.DownloadObject(bucketName, gcp_path, outputFile);
				Console.WriteLine($"Downloaded GCP:{gcp_path} to {local_path}");
			}
			else
			{
				ConsoleWrite.ConsoleWriteError($" Error！檔案不存在：{gcp_path}\n");
				throw new FileNotFoundException($"Error！檔案不存在：{gcp_path}");
			}
		}

		/// <summary>
		/// 上傳檔案至Google Cloud Storage
		/// </summary>
		/// <param name="gcp_path">Google Cloud Storage buckets下檔案的路徑</param>
		/// <param name="local_path">本地端路徑檔案的路徑</param>
		public override void PushFile(string gcp_path, string local_path)
		{
			//using StorageClient client = StorageClient.Create(base.Credential);
			using var file = File.OpenRead(local_path);
			_client.UploadObject(bucketName, gcp_path, null, file);
			Console.WriteLine($"Upload {local_path} to GCP:{gcp_path}");
		}

		/// <summary>
		/// 判斷Google Cloud Storage bucket內檔案是否存在
		/// </summary>
		/// <param name="gcp_path">Google Cloud Storage buckets下檔案的路徑</param>
		/// <returns></returns>
		public override bool ExistsFile(string gcp_path)
		{
			bool flag;
			try
			{				
				var obj = this.GetResultObjects(gcp_path);
				if (obj.Count > 0)
					flag = true;
				else
					flag = false;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}



		/// <summary>
		/// 列出 Google Cloud Storage bucket內的物件檔案清單
		/// </summary>
		/// <param name="gcp_path">Google Cloud Storage buckets下資料夾的路徑</param>
		/// <param name="prefix">前綴字</param>
		/// <param name="extension">附檔名eg.   : csv</param>
		/// <returns>物件集合LIST</returns>
		public override List<Object> GetResultObjects(string gcp_path, string prefix = null, string extension = null)
		{
			List<Object> objects = new();
			if (string.IsNullOrEmpty(extension))
				extension = "**.**";
			else
				extension = $"**.{extension.ToLower()}";

			ListObjectsOptions options = new ListObjectsOptions() { Delimiter = "/", MatchGlob = extension };
			if (string.IsNullOrEmpty(prefix))
				objects = _client.ListObjects(bucketName, gcp_path, options).ToList();
			else
				objects = _client.ListObjects(bucketName, gcp_path, options).ToList().FindAll(x => x.Name.Contains(prefix));
			return objects;
		}


	}
}