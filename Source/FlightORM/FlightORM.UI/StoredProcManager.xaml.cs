using FlightORM.Core.Config;
using FlightORM.SqlServer;
using FlightORM.UI.Models;
using System;
using System.Collections.Generic;
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

namespace FlightORM.UI
{
	/// <summary>
	/// Interaction logic for StoredProcManager.xaml
	/// </summary>
	public partial class StoredProcManager : UserControl
	{

		

		public StoredProcManager()
		{
			InitializeComponent();
			var vm = new StoredProcsVM();
			this.DataContext = vm;
		}

		public StoredProcManager(string cnnStr)
		{
			InitializeComponent();
			var vm = new StoredProcsVM(cnnStr);
			vm.Load();
			this.DataContext = vm;
		}

		private void OpenDetailWindow(object sender, RoutedEventArgs e)
		{
			var win = new SPDetailWindow();
			win.DataContext = ((FrameworkElement)sender).DataContext;
			win.Show();
			e.Handled = true;
		}


	}


}
