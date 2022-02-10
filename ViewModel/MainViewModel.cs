using Microsoft.Win32;
using ReaderHelper.Commands;
using ReaderHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReaderHelper.ViewModel
{
	public class MainViewModel : BaseModel
	{
		public string InputText { get; set; }
		public Word CurrentWord { get; set; }
		public static ObservableCollection<Word> ProcessedWords { get; set; } = new ObservableCollection<Word>();
		public ICommand UploadCommand { get; set; }
		public ICommand DeleteCommand { get; set; }
		public ICommand NextCommand { get; set; }
		public ICommand BackCommand { get; set; }
		public ICommand DownloadCommand { get; set; }
		List<KeyValuePair<string, int>> sortedWords;

		public MainViewModel()
		{
			UploadCommand = new RelayCommand(Upload);
			DeleteCommand = new RelayCommand(Delete);
			NextCommand = new RelayCommand(Next);
			BackCommand = new RelayCommand(Back);
			DownloadCommand = new RelayCommand(Download);
		}

		private void Upload(object o)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
				InputText = File.ReadAllText(openFileDialog.FileName);

			if (InputText is not null)
			{
				sortedWords = ConvertInitialText(InputText, out var x);
				foreach (var k in (from kvp in sortedWords select kvp.Key).ToList())
				{
					ProcessedWords.Add(new Word(k.ToString(), null));
				}
				CurrentWord = ProcessedWords.First();
			}
		}

		private static List<KeyValuePair<string, int>> ConvertInitialText(string initialText, out Dictionary<string, int> Words)
		{
			Words = new Dictionary<string, int>();
			Regex pattern = new Regex("\\p{P}");
			Regex exceptionWords = new Regex("^(the|or|a|in|an|at|of|to|but|if|are|is|and|so|as|not|for)$");
			string cleanedText = pattern.Replace(initialText, "").ToLower();
			string[] initialWords = cleanedText.Split(" ");
			foreach (string word in initialWords)
			{
				if (!string.IsNullOrEmpty(word) && !exceptionWords.IsMatch(word))
				{
					if (Words.Keys.Contains(word))
					{
						Words[word] += 1;
					}
					else
					{
						Words.Add(word, 1);
					}
				}
			}
			return Words.OrderByDescending(x => x.Value).ToList();
		}

		private void Delete(object o)
		{
			int id = ProcessedWords.IndexOf(CurrentWord);
			ProcessedWords.Remove(CurrentWord);
			if (id == -1 || id + 1 >= ProcessedWords.Count - 1)
			{
				CurrentWord = ProcessedWords.First();
			}
			else if (ProcessedWords.Count - 1 == 0)
			{
				CurrentWord = null;
			}
			else
			{
				CurrentWord = ProcessedWords[++id];
			}
		}

		private void Next(object o)
		{
			int id = ProcessedWords.IndexOf(CurrentWord);
			CurrentWord = id == ProcessedWords.Count - 1 ? ProcessedWords[0] : ProcessedWords[++id];
		}

		private void Back(object o)
		{
			int id = ProcessedWords.IndexOf(CurrentWord);
			CurrentWord = id == 0 ? ProcessedWords[ProcessedWords.Count - 1] : ProcessedWords[--id];
		}

		private void Download(object o)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Text (*.txt)|*.txt";
			if (saveFileDialog.ShowDialog() == true)
			{
				string fileName = saveFileDialog.FileName;
				using (StreamWriter sw = new StreamWriter(fileName))
				{
					foreach (Word w in ProcessedWords)
						sw.WriteLine(w.ToString());
				}
			}
		}
	}
}
