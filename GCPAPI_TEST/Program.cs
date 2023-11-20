using CommonLibrary;
using GCP.SERVICE;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace GCPAPI_TEST
{
	internal class Program
	{
		//-- JOERY TEST 
		//static String bucketName = "test_bucket_joery";
		//static String credentialPath = @"GoogleCredential\rational-genius-400902-2e11dab77a87.json";

		//-- TWM 
		static String bucketName = "at-twm-svy-do-automation";
		static String credentialPath = @"GoogleCredential\surveycake-twm-1f6e0a95a707.json";


		static String gcpPath, localPath;
		static GCPBase gcpService = new GoogleCloudStorageService();

		static void Main(string[] args)
		{
			Console.WriteLine("TEST START");
			//DownLoadFile_1();
			//UpLoadFile2();
			// gcpService = new GoogleCloudStorageService();
			gcpService.SetGCPService(bucketName, credentialPath);
			ConsoleWrite.ConsoleWriteTip("***** START... *****");

			localPath = @"D:\TEST\unfinished_list_csv\";
			gcpPath = @"automation/bs/storage/twm/unfinished_list/csv/";

			//gcpService.PullFile("automation/bs/storage/twm/unfinished_list/csv/test_20231116153322.jry", "D:\\TEST\\pull_from_gcp\\test_20231116153322.jry");

			//gcpService.PushFile($"{gcpPath}LrWLP_20231115170533.csv", $"{localPath}LrWLP_20231115170533.csv");
			//gcpService.PushFile($"{gcpPath}LrWLP_20231117094733.csv", $"{localPath}LrWLP_20231117094733.csv");
			//gcpService.PushFile($"{gcpPath}LrWLP_20231117104733.csv", $"{localPath}LrWLP_20231117104733.csv");

			//gcpService.PushFile($"{gcpPath}eLGk6_20231107193807.csv", $"{localPath}eLGk6_20231107193807.csv");
			//gcpService.PushFile($"{gcpPath}eLGk6_20231107233807.csv", $"{localPath}eLGk6_20231107233807.csv");
			// ListFile(gcpPath);
			//TEST1();
			//gcpPath = null;
			List<Object> list = gcpService.GetResultObjects(gcpPath, "", "csv");
			ConsoleWrite.ConsoleWriteTip($" 列出 \"\" 數量 = {list.Count}");
			foreach (var item in list)
			{
				ConsoleWrite.ConsoleWriteInfo($" NAME={item.Name} \t  SIZE={item.Size}");

				//下載
				//gcpService.PullFile(item.Name, $"{localPath}{gcpService.getObjectFilename(item.Name)}");
			}


			ConsoleWrite.ConsoleWriteTip("***** END ... *****");
			Console.WriteLine("TEST  press any key to exit");
			Console.Read();
		}

		#region TEST

		static void TEST1()
		{
			String file = "automation/bs/storage/twm/unfinished_list/csv/Pw3E8_20231115170544.txt";
			String newfile = $"test_{DateTime.Now.ToString("yyyyMMddHHmmss")}.jry";
			if (gcpService.ExistsFile(file))
				gcpService.PullFile(file, $"{localPath}{newfile}");
			gcpService.PushFile($"{gcpPath}{newfile}", $"{localPath}{newfile}");
		}

		static void DownLoadFile_1()
		{
			string bucketName = "test_bucket_joery";
			string objectName = "25sprout_survey_status/202309261300.json";
			string localPath = @"D:\TEST\test.jason";
			string localUploadPath = @"C:\Users\Supei\Downloads\Script\Script\CRM_PPCG_LEAD_PROCESS.btq";
			//從資料流讀取
			GoogleCredential credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(
				System.IO.File.ReadAllText(@"D:\_WorkProject\TWM\112_CRM_SURVEY\01_DOC\rational-genius-400902-2e11dab77a87.json"));
			using (StorageClient client = StorageClient.Create(credential))
			{
				using var outputFile = File.OpenWrite(localPath);

				client.DownloadObject(bucketName, objectName, outputFile);
				Console.WriteLine($"Downloaded {objectName} to {localPath}.");

				//client.UploadObject(bucketName, localUploadPath)
			}
		}
		static void UpLoadFile()
		{
			string bucketName = "test_bucket_joery";
			string objectName = @"upload/Pw3E8.csv";
			string localPath = @"D:\TEST\sc_variable_csv\Pw3E8.csv";
			
			//從資料流讀取
			GoogleCredential credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(
				System.IO.File.ReadAllText(@"D:\_WorkProject\TWM\112_Survey\01_DOC\rational-genius-400902-2e11dab77a87.json"));
			using (StorageClient client = StorageClient.Create(credential))
			{
				using var outputFile = File.OpenRead(localPath);

				client.UploadObject(bucketName, objectName, null, outputFile);
				Console.WriteLine($"Uploadloaded {localPath} to {objectName}.");

			}
		}

		static void UpLoadFile2()
		{
			string bucketName = "test_bucket_joery";
			string objectName = gcpPath;
			string localPath = @"D:\TEST\unfinished_list_csv\eLGk6_20231107193807.csv";

			//從資料流讀取
			GoogleCredential credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(
				System.IO.File.ReadAllText(@"D:\_WorkProject\TWM\112_Survey\01_DOC\rational-genius-400902-2e11dab77a87.json"));
			using (StorageClient client = StorageClient.Create(credential))
			{
				using var outputFile = File.OpenRead(localPath);

				client.UploadObject(bucketName, objectName, null, outputFile);
				Console.WriteLine($"Uploadloaded {localPath} to {objectName}.");

			}
		}

		public static void ListFile(String gcp_path)
		{
			//從資料流讀取
			GoogleCredential credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(System.IO.File.ReadAllText(credentialPath));
			using (StorageClient client = StorageClient.Create(credential))
			{
				List<Object> objects = client.ListObjects(bucketName, gcp_path, new ListObjectsOptions() { Delimiter = "/", MatchGlob = "**.**" }).ToList();
				//var gObjects = client.ListObjects(bucketName, gcp_path, new ListObjectsOptions() { Delimiter = "/" });
				foreach (var obj in objects.ToList().FindAll(x => x.Name.Contains("2023111517")))
				{
					Console.WriteLine(obj.Name);
				}
			}
		}
		#endregion
	}
}