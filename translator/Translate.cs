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
				Console.WriteLine("6. Load dictionary from file");
				Console.WriteLine("7. Save dictionary to file");

				Console.WriteLine("Выберите пункт меню:");
				ConsoleKeyInfo userInputSymbol;

				do
				{
					userInputSymbol = Console.ReadKey(true);
				}
				while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D8);

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

					case 4:
						Console.WriteLine("Enter delete world:");
						string deletedWord = Console.ReadLine();

						if (this.DeleteWord(deletedWord))
						{
							Console.WriteLine("The required word has been deleted");
						}
						else
						{
							Console.WriteLine("The required word was not found");
						}

						break;

					case 5:
						Console.WriteLine("Type in the word to be translated");
						string searchWord = Console.ReadLine();

						if (this.FindWord(searchWord))
						{
							Console.WriteLine("Translation of the word found");
						}
						else
						{
							Console.WriteLine("Translation of word not found");
						}

						break;

					case 6:
						Console.WriteLine("Enter the path to the file");
						string filePath = Console.ReadLine();
						if (LoadDictionaryFile(filePath))
						{
							Console.WriteLine("File found.");
						}
						else
						{
							Console.WriteLine("File not found.");
						}
		 
						break;

					case 7:
						Console.WriteLine("Set file name:");
						string fileName = Console.ReadLine();

						if (this.SaveFile(fileName))
						{
							Console.WriteLine($"The File {fileName} was saved successfully.");
						}
						else
						{
							Console.WriteLine($"Error in saving.");
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
		* @brief Remove word from dictionaty.
		* @param[in] Word who deleting from dictionary.
		* @return True - word was deleted; False - word not deleted.
		*/
		private bool DeleteWord(string deletedWord)
		{
			if (this._dictionaryTranslate.ContainsKey(deletedWord))
			{
				return this._dictionaryTranslate.Remove(deletedWord);
			}

			return false;
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

		/*!
		* @brief Load dictionary from file
		* @param[in] filePath File path
		* @return True - file found and uploaded to dictionary; False -file not found.
		*/
		public bool LoadDictionaryFile(string filePath)
		{
			if (File.Exists(filePath)) //!< File opening check
			{ 
				foreach (string[] item in File.ReadAllLines(filePath).Select(line => line.Split('-')))
				{  
					this._dictionaryTranslate.Add(item[0], item[1]); 
				} //!< Dictionary entry

				return true;
			}

			return false;
		}

		/*!
		 * @brief Saving dictionary to file.
		 * @param[in] Including fileName as new name of the saved file.
		 * @param[in] Setting folderName and format as default manually.
		 * @return True - File was successfully saved; False - Error in saving file.
		 */
		private bool SaveFile(string fileName, string folderName = "DictionaryTranslate", string format = ".lge")
		{
			// Getting Path of File
			string filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), folderName), fileName + format);

			try
			{
				// Write dictionary to file
				using (StreamWriter writer = new StreamWriter(filePath)) 
				{
					foreach (KeyValuePair<string, string> i in _dictionaryTranslate)
					{
						writer.WriteLine($"{i.Key}-{i.Value}");
					}
				}

				return true;
			}
			catch (Exception problem)
			{
				Console.WriteLine($"Something went wrong in saving {fileName}. Error: {problem}.");
			}

			return false;
		}
	}
}