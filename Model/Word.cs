using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderHelper.Model
{
	public class Word : INotifyPropertyChanged
	{
		private string _initialWord;
		public string InitialWord
		{
			get
			{
				return _initialWord;
			}
			set
			{
				_initialWord = value;
				OnPropertyChanged(InitialWord);
			}
		}

		private string _translatedWord;
		public string TranslatedWord
		{
			get
			{
				return _translatedWord;
			}
			set
			{
				_translatedWord = value;
				OnPropertyChanged(TranslatedWord);
			}
		}

		#region INotifyPropertyChanged Members 
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}
}



