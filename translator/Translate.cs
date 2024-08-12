namespace translator
{
	public class Translate
	{
		private Dictionary<string, string> _dictionaryTranslate;

		public Translate()
		{
			this._dictionaryTranslate = new Dictionary<string, string>();
		}

		public void PrintMenu()
		{
			const uint NUMBER_EXIT_MENU_ELEMENT = 6;
			int numberMenuElement = 0;

			while (numberMenuElement != NUMBER_EXIT_MENU_ELEMENT)
			{
				Console.Clear();
				Console.WriteLine("Menu translator:");
				Console.WriteLine("1. Create tranlator");
				Console.WriteLine("2. Add word to dictionary");
				Console.WriteLine("3. Replace word in dictionary");
				Console.WriteLine("4. Delete word from dictionary");
				Console.WriteLine("5. Delete word from dictionary");

				Console.Write("Выберите пункт меню: \n");
				ConsoleKeyInfo userInputSymbol;

				do
				{
					userInputSymbol = Console.ReadKey(true);
				} while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D5);

				numberMenuElement = Convert.ToInt32(userInputSymbol.KeyChar.ToString());

				switch (numberMenuElement)
				{ 
					case 2:
						Console.WriteLine("Set new word:");
						string newWord = Console.ReadLine();
						Console.WriteLine("Set translation of new word:");
						string translatedWord = Console.ReadLine();
						this.AddWord(newWord, translatedWord);
						break;
				}
			}
		}

		private void AddWord(string newWord, string translatedWord)
		{
			this._dictionaryTranslate.Add(newWord, translatedWord);
		}
	}
}
