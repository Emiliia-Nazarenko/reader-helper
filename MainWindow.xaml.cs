using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ReaderHelper
{

	public partial class MainWindow : Window
	{
		//public string initialText;

		Dictionary<string, int> Words = new Dictionary<string, int>();
		static List<string[]> ProcessedWords = new List<string[]>();
		StringBuilder test = new StringBuilder(100);
		List<KeyValuePair<string, int>> sortedWords;


		public MainWindow()
		{
			InitializeComponent();
		}

		public string text;
		private void Upload_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text files (*.txt)|*.txt|Word documents (*.docx)|*.docx|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
				text = File.ReadAllText(openFileDialog.FileName);
			initialTextBox.Text = text;
			sortedWords = ConvertInitialText(text, out var x);
			foreach (var k in (from kvp in sortedWords select kvp.Key).ToList())
			{
				ProcessedWords.Add(new string[2] { k, null });
				test.Append(k.ToString());
				test.Append("\n");
			}
			wordsTextBox.Text = test.ToString();
			initialWord.Text = ProcessedWords[0][0];
		}

		private static List<KeyValuePair<string, int>> ConvertInitialText(string initialText, out Dictionary<string, int> Words)
		{
			Words = new Dictionary<string, int>();
			Regex pattern = new Regex("\\p{P}");
			Regex exceptionWords = new Regex("^(the|or|a|in|an|at|of|to|but|if|and|so|as|not|for)$");
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


	}
}
