
namespace ReaderHelper.Model
{
	public class Word : BaseModel
	{
		public string InitialWord { get; set; }

		public string TranslatedWord { get; set; }

		public Word(string initialWord, string? translatedWord)
		{
			InitialWord = initialWord;
			TranslatedWord = translatedWord;
		}

		public override string ToString()
		{
			return InitialWord + " - " + TranslatedWord;
		}
	}
}
