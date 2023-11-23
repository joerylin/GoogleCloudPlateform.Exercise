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

		//-- Reacky TEST 
		static String bucketName = "e-tec_test";
		static String credentialPath = @"GoogleCredential\mytest-405908-efc1034d9512.json";

		//-- TWM 
		//static String bucketName = "at-twm-svy-do-automation";
		//static String credentialPath = @"GoogleCredential\surveycake-twm-1f6e0a95a707.json";

		static String gcpPath, localPath;
		static GCPBase gcpService = new GoogleCloudStorageService();

		static void Main(string[] args)
		{
			Console.WriteLine("TEST START");

			gcpService.SetGCPService(bucketName, credentialPath);
			ConsoleWrite.ConsoleWriteTip("***** START... *****");

			localPath = @"D:\TEST\unfinished_list_csv\";
			gcpPath = @"upload/";

			//Push to GCP
			gcpService.PushFile($"{gcpPath}Joery_20231122100000.csv", $"{localPath}Joery_20231122100000.csv");
			//gcpService.PushFile($"{gcpPath}Joery_20231122110000.csv", $"{localPath}Joery_20231122110000.csv");
			//gcpService.PushFile($"{gcpPath}Joerz_20231122100000.csv", $"{localPath}Joerz_20231122100000.csv");
			//gcpService.PushFile($"{gcpPath}Pw3E8_20231121145700.csv", $"{localPath}Pw3E8_20231121145700.csv");


			//Pull from GCP
			//gcpService.PullFile("automation/bs/storage/twm/unfinished_list/csv/test_20231116153322.jry", "D:\\TEST\\pull_from_gcp\\test_20231116153322.jry");

			ListAndDownLoad(gcpPath);
			//ListAndDownLoad(gcpPath, @"D:\TEST\");


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

		static void ListAndDownLoad(String m_gcp_path, String m_local_path=null, String m_extension = null)
		{
			List<Object> list;
			list = gcpService.GetResultObjects(m_gcp_path, "", m_extension);
			//list = gcpService.GetResultObjects($"{gcpPath}eLGk6_20231107233807.csv");
			ConsoleWrite.ConsoleWriteTip($" 列出 \"\" 數量 = {list.Count}");
			foreach (var item in list)
			{
				ConsoleWrite.WriteLine($" NAME={item.Name} \t  SIZE={item.Size}");

				//下載
				if(!String.IsNullOrEmpty(m_local_path))
					gcpService.PullFile(item.Name, $"{m_local_path}{gcpService.getObjectFilename(item.Name)}");
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