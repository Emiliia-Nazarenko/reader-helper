
using ReaderHelper.ViewModel;
using System.Windows;


namespace ReaderHelper
{
	public partial class MainWindow : Window
	{
		private readonly MainViewModel _vm;

		public MainWindow()
		{
			InitializeComponent();
			DataContext = _vm = new MainViewModel();
		}

		private void TranslationTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			// Jump to next word when pressing ENTER
			if (e.Key == System.Windows.Input.Key.Enter)
			{
				e.Handled = true;
				_vm.NextCommand.Execute(null);
			}
		}
	}
}
