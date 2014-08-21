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

namespace FlightORM.UI.StoredProcedure
{
	/// <summary>
	/// Interaction logic for StoredProcSampleQuery.xaml
	/// </summary>
	public partial class SampleQuery : UserControl
	{
		public SampleQuery()
		{
			InitializeComponent();
		}

		private void TestQuery(object sender, RoutedEventArgs e)
		{
			var model = (StoredProcVM)this.DataContext;
			model.Run();

		}
	}
}
