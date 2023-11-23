using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GCP.SERVICE;
using Microsoft.Win32;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace GCP.WpfApp.Exercise
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly GoogleCloudStorageService _GcpService = new GoogleCloudStorageService();
		private readonly String _gcp_path = "upload/";
		public MainWindow()
		{
			InitializeComponent();
            String bucketName = "e-tec_test";
            String credentialPath = @"GoogleCredential\mytest-405908-efc1034d9512.json";
            this._GcpService.SetGCPService(bucketName, credentialPath);
		}

        private void Window_Activated(object sender, EventArgs e)
        {
			this.DataGrid_DataBinding();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
			//this.Cursor = new Cursors().Wait;
			this.DataGrid_DataBinding();
		}

		private void buttonUpload_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
			if (openFile.ShowDialog()==true)
			{
			    this._GcpService.PushFile($"{_gcp_path}{openFile.SafeFileName}", openFile.FileName);
				MessageBox.Show("上傳檔案成功！");
				this.DataGrid_DataBinding();
			}
		}

		private void DataGrid_DataBinding()
		{
			List<Object> objects = this._GcpService.GetResultObjects(_gcp_path, this.TextKeyWord.Text);
			this.DataGridResult.ItemsSource = objects;

		}

		private void DataGridResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void buttonDownload_Click(object sender, RoutedEventArgs e)
		{
			if (!String.IsNullOrEmpty(this.TextKeyWord.Text))
			{
				this._GcpService.PullFile(@"upload\棒球記錄表.pdf", @$"D:\TEST\{this.TextKeyWord.Text}");
				//this._GcpService.PullFile(@$"{this._gcp_path}{this.TextKeyWord.Text}", @$"D:\TEST\{ this.TextKeyWord.Text}");
				MessageBox.Show("下載成功！");
			}
			else
				MessageBox.Show("請輸入檔名！");
		}
	}
}
