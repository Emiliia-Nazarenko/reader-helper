
using ReaderHelper.ViewModel;
using System.Windows;


namespace ReaderHelper
{
	public partial class MainWindow : Window
	{
		private readonly MainViewModel _vm = new MainViewModel();
		public MainWindow()
		{
			InitializeComponent();
			DataContext = _vm;
		}

		private void TranslationTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if(e.Key == System.Windows.Input.Key.Enter)
			{
				e.Handled = true;
				_vm.NextCommand.Execute(null);
			}
		}
	}
}
