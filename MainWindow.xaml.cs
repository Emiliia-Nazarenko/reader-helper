
using ReaderHelper.ViewModel;
using System.Windows;


namespace ReaderHelper
{

	public partial class MainWindow : Window
	{


		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}


	}
}
