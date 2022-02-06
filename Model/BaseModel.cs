
using System.ComponentModel;


namespace ReaderHelper.Model
{
	public abstract class BaseModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged Members 
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}
}
