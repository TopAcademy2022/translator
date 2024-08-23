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
					case 1:
						Console.WriteLine("Set file name: ");
						string fileName = Console.ReadLine();

						Console.WriteLine("Specify the path to the folder: ");
						string destinationDirectory = Console.ReadLine();

						if (СreateFile(fileName, destinationDirectory))
						{
							Console.WriteLine($"Файл '{fileName}' создан.");
						}
						else
						{
							Console.WriteLine($"Ошибка.");
						}
						Console.ReadKey();

						break;

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
				}
			}
		}

		public bool СreateFile(string fileName, string folderPath, string format = ".lge")
		{
			try
			{
				// 1. Проверка на существование папки
				string directoryPath = Path.GetDirectoryName(folderPath);
				if (!Directory.Exists(directoryPath))
				{
					Console.WriteLine($"Папка '{directoryPath}' не существует. Файл не создан.");
				}
				else if (File.Exists(folderPath))
				{
					// 2. Проверка на существование файла
					Console.WriteLine($"Файл '{fileName}' уже существует.");
				}
				else if (string.IsNullOrEmpty(fileName))
				{
					// 3. Проверка на пустое имя файла
					Console.WriteLine("Имя файла не может быть пустым.");
				}
				else if (Path.GetInvalidFileNameChars().Any(c => fileName.Contains(c)))
				{
					// 4. Проверка на некорректные символы в имени файла
					Console.WriteLine("Имя файла содержит недопустимые символы.");
				}
				else if (string.IsNullOrWhiteSpace(format) || !format.StartsWith("."))
				{
					//5. Проверка на наличие расширения формата
					Console.WriteLine($"В формате {format} отсутствует точка.");
				}
				else if (!format.Equals(".lge"))
				{
					// 6. Проверка на правильный формат файла
					Console.WriteLine("Неверный формат файла. Допустимый формат: '.lge'.");
				}
				else
				{
                    //Создание файла и папки
                    Directory.CreateDirectory(folderPath);
                    using (FileStream file = File.Create(Path.Combine(folderPath, fileName + format))) { }
                    return true; // Файл создан успешно
                }
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ошибка при создании файла: " + ex.Message);
			}

			Console.ReadKey();
			return false; // Ошибка при создании
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
	}
}