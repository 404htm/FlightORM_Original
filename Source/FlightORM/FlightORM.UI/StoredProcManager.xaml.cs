using FlightORM.Core.Config;
using FlightORM.SqlServer;
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
		public StoredProcManager(string cnnStr)
		{
			InitializeComponent();

			//TODO: Replace terrible test code
			//TODO: Remove dependency on SqlServerLib

			var loader = new StoredProcAnalysis(cnnStr);
			var procs = loader.GetProcedures();

			var items = procs.Select(p => new StoredProcMapping(p)).ToList();

			SPMappingGrid.ItemsSource =  items;
		}
	}
}
