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
using Object = Google.Apis.Storage.v1.Data.Object;

namespace GCP.WpfApp.Exercise
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly GoogleCloudStorageService _GcpService = new GoogleCloudStorageService();
		public MainWindow()
		{
			InitializeComponent();
            String bucketName = "e-tec_test";
            String credentialPath = @"GoogleCredential\mytest-405908-efc1034d9512.json";
            this._GcpService.SetGCPService(bucketName, credentialPath);
		}

        private void Window_Activated(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Object> objects =  this._GcpService.GetResultObjects(null, this.TextKeyWord.Text , null);
            
        }
    }
}
