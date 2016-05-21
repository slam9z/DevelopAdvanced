using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CachingTasks
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			GetInternetContent();
		}


		private async Task GetInternetContent()
		{

			for (int i = 0; i < 50; i++)
			{
				Task.Run(async () =>
				{
					if (i % 10 == 0)
					{
						Thread.Sleep(1000);
					}
					var str = await ConcurrentHttpClient.GetContentsAsync("http://globalbo.ioffice100.net/SignUpApi/AccountTypes/1/AccountStatus?accountName=weili1%40iblue100.com");
					Console.WriteLine("GetContentsAsync: {0}", str);
					
				});
			}
		}

	}
}
