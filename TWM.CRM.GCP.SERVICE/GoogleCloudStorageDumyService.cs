using Object = Google.Apis.Storage.v1.Data.Object;

namespace GCP.SERVICE
{
	public class GoogleCloudStorageDumyService : GCPBase
	{
		public override void PullFile(string gcp_path, string local_path)
		{
			try
			{
				File.Copy(gcp_path, local_path, true);
			}
			catch (FileNotFoundException fex)
			{
				Console.WriteLine($"找不到檔案{gcp_path}");
				throw fex;
			}

		}

		public override void PushFile(string gcp_path, string local_path)
		{
			try
			{
				File.Copy(gcp_path, local_path, true);
			}
			catch (FileNotFoundException fex)
			{
				Console.WriteLine($"找不到檔案{gcp_path}");
				throw fex;
			}
		}


		public override bool ExistsFile(string gcp_path)
		{
			return File.Exists(gcp_path);
		}

		public override List<Object> GetResultObjects(string gcp_path, string prefix = null, string extension = null)
		{
			throw new NotImplementedException();
		}
	}
}