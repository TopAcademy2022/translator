using System;

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
				Console.WriteLine("5. Find word from dictionary");

				Console.WriteLine("Выберите пункт меню:");
				ConsoleKeyInfo userInputSymbol;

				do
				{
					userInputSymbol = Console.ReadKey(true);
				}
				while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D5);

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

					case 5:
						Console.WriteLine("Type in the word to be translated");
						string searchWord = Console.ReadLine();
						if (FindWord(searchWord))
						{
                            Console.WriteLine("Translation of the word found");
                        }
						else
						{
                            Console.WriteLine("Translation of word not found");
                        }
						break;
				}
			}
		}
		private void AddWord(string newWord, string translatedWord)
		{
			this._dictionaryTranslate.Add(newWord, translatedWord);
		}

        /*!
		 * @brief Searching words in the dictionary
		 * @param[in] searchWord Word for searching
		 * @return True - word is found; False - word not found.
		 */
        private bool FindWord(string searchWord)
		{
			if (this._dictionaryTranslate.ContainsKey(searchWord))
			{
				return true;
			}

			return false;
		}
	}
}