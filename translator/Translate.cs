using Microsoft.Extensions.Logging;

namespace translator
{
	public class Translate
	{
		private ILogger<Translate> _logger;

		private readonly Dictionary<string, string> _dictionaryTranslate;

		public IReadOnlyDictionary<string, string> DictionaryTranslate => this._dictionaryTranslate;

		public Translate()
		{
			using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
			this._logger = factory.CreateLogger<Translate>();

			this._dictionaryTranslate = new Dictionary<string, string>();
		}

		public void PrintMenu()
		{
			const uint NUMBER_EXIT_MENU_ELEMENT = 9;
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
				Console.WriteLine("8. Print a dictionary to the console");
				Console.WriteLine("9. Exit");

				Console.WriteLine("\nВыберите пункт меню:");
				ConsoleKeyInfo userInputSymbol;

				do
				{
					userInputSymbol = Console.ReadKey(true);
				}
				while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D9);

				numberMenuElement = Convert.ToInt32(userInputSymbol.KeyChar.ToString());

				switch (numberMenuElement)
				{
					case 1:
						Console.WriteLine("Set file name:");
						string? fileName = Console.ReadLine();

						Console.WriteLine("Specify the path to the folder:");
						string? destinationDirectory = Console.ReadLine();

						if (!String.IsNullOrEmpty(fileName) && !String.IsNullOrEmpty(destinationDirectory))
						{
							if (СreateFile(fileName, destinationDirectory))
							{
								Console.WriteLine($"File '{fileName}' has been created.");
							}
							else
							{
								Console.WriteLine("Error, file not created.");
							}
						}

						Console.ReadKey();
						break;

					case 2:
						Console.WriteLine("Set new word:");
						string? newWord = Console.ReadLine();

						Console.WriteLine("Set translation of new word:");
						string? translatedWord = Console.ReadLine();

						if (!String.IsNullOrEmpty(newWord) && !String.IsNullOrEmpty(translatedWord))
						{
							this.AddWord(newWord, translatedWord);
						}
						else
						{
							Console.WriteLine("Error, word not adding.");
						}

						Console.ReadKey();
						break;


                    case 4:
						Console.WriteLine("Enter delete world:");
						string? deletedWord = Console.ReadLine();

						if (!String.IsNullOrEmpty(deletedWord))
						{
							if (this.DeleteWord(deletedWord))
							{
								Console.WriteLine("The required word has been deleted.");
							}
							else
							{
								Console.WriteLine("The required word was not found.");
							}
						}

						Console.ReadKey();
						break;

					case 5:
						Console.WriteLine("Type in the word to be translated");
						string? searchWord = Console.ReadLine();

						if (!String.IsNullOrEmpty(searchWord))
						{
							if (this.FindWord(searchWord))
							{
								Console.WriteLine("Translation of the word found");
							}
							else
							{
								Console.WriteLine("Translation of word not found");
							}
						}

						Console.ReadKey();
						break;

					case 6:
						Console.WriteLine("Enter the path to the file:");
						string? loadFileName = Console.ReadLine();

						Console.WriteLine("Specify the path to the folder:");
						string? loadFileDirectory = Console.ReadLine();

						if (!String.IsNullOrEmpty(loadFileName) && !String.IsNullOrEmpty(loadFileDirectory))
						{
							if (LoadDictionaryFile(loadFileName, loadFileDirectory))
							{
								Console.WriteLine("File has been loading.");
							}
							else
							{
								Console.WriteLine("File not found.");
							}
						}

						Console.ReadKey();
						break;

					case 7:
						Console.WriteLine("Enter the save file name:");
						string? saveFileName = Console.ReadLine();

						Console.WriteLine("Specify the path to the save folder:");
						string? saveFileDirectory = Console.ReadLine();

						if (!String.IsNullOrEmpty(saveFileName) && !String.IsNullOrEmpty(saveFileDirectory))
						{
							if (this.SaveDataToFile(saveFileName, saveFileDirectory))
							{
								Console.WriteLine($"The file {saveFileName} was saved successfully.");
							}
							else
							{
								Console.WriteLine($"Error in saving.");
							}
						}

						Console.ReadKey();
						break;

					case 8:
						this.PrintDictionary();

						Console.ReadKey();
						break;
                }
			}
		}

		public bool СreateFile(string fileName, string directory = "./DictionaryTranslate", string format = ".lge")
		{
			try
			{
				if (Path.GetInvalidPathChars().Any(symbol => directory.Contains(symbol)))
				{
					throw new Exception("Directory name contains invalid characters.");
				}
				else if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}
				if (string.IsNullOrEmpty(fileName))
				{
					throw new Exception("File name is empty.");
				}
				else if (Path.GetInvalidFileNameChars().Any(symbol => fileName.Contains(symbol)))
				{
					throw new Exception("File name contains invalid characters.");
				}
				else if (string.IsNullOrWhiteSpace(format) || !format.StartsWith("."))
				{
					throw new Exception("Incorrect file format.");
				}
				else if (File.Exists(Path.Combine(directory, fileName + format)))
				{
					throw new Exception("File already exists.");
				}
				else
				{
					using (FileStream file = File.Create(Path.Combine(directory, fileName + format))) { }
					return true;
				}
			}
			catch (Exception exception)
			{
				this._logger.LogError(exception.Message);
			}

			return false;
		}

		public void AddWord(string newWord, string translatedWord)
		{
			this._dictionaryTranslate.Add(newWord, translatedWord);
		}

		/*! 
		* @brief Remove word from dictionaty.
		* @param[in] deletedWord Word who deleting from dictionary.
		* @return True - word was deleted; False - word not deleted.
		*/
		public bool DeleteWord(string deletedWord)
		{
			if (this._dictionaryTranslate.ContainsKey(deletedWord))
			{
				return this._dictionaryTranslate.Remove(deletedWord);
			}

			return false;
		}

		/*! 
		* @brief Output a dictionary to the console.
		*/
		public void PrintDictionary()
		{
			if (this._dictionaryTranslate.Count() > 0)
			{
				foreach (KeyValuePair<string, string> oneRow in this._dictionaryTranslate)
				{
					Console.WriteLine($"{oneRow.Key}-{oneRow.Value}");
				}
			}
			else
			{
				Console.WriteLine("Dictionary is empty.");
			}
		}

		/*!
		 * @brief Searching words in the dictionary
		 * @param[in] searchWord Word for searching
		 * @return True - word is found; False - word not found.
		 */
		public bool FindWord(string searchWord)
		{
			if (this._dictionaryTranslate.ContainsKey(searchWord))
			{
				return true;
			}

			return false;
		}

		/*!
		* @brief Load dictionary from file
		* @param[in] fileName File name
		* @param[in] directory Directory with file
		* @param[in] format File format
		* @return True - file found and uploaded to dictionary; False -file not found.
		*/
		public bool LoadDictionaryFile(string fileName, string directory = "./DictionaryTranslate", string format = ".lge")
		{
			string filePath = Path.Combine(directory, fileName + format);

			if (File.Exists(filePath)) //!< File opening check
			{
				foreach (string[] item in File.ReadAllLines(filePath).Select(line => line.Split('-')))
				{
					this._dictionaryTranslate.Add(item[0], item[1]);
				}

				return true;
			}

			return false;
		}

		/*!
		 * @brief Saving dictionary to file.
		 * @param[in] fileName Including fileName as new name of the saved file.
		 * @param[in] directory Setting folderName and format as default manually.
		 * @param[in] format Setting file format as default manually.
		 * @return True - File was successfully saved; False - Error in saving file.
		 */
		public bool SaveDataToFile(string fileName, string directory = "./DictionaryTranslate", string format = ".lge")
		{
			string filePath = Path.Combine(directory, fileName + format);

			if (!File.Exists(filePath))
			{
				this.СreateFile(filePath, directory, format);

				using (StreamWriter writer = new StreamWriter(filePath))
				{
					foreach (KeyValuePair<string, string> i in this._dictionaryTranslate)
					{
						writer.WriteLine($"{i.Key}-{i.Value}");
					}

					writer.Close();
				}

				return true;
			}

			return false;
		}

        public void SaveWordToFile(string fileName, string directory = "./DictionaryTranslate", string format = ".lge", string newWord, string translatedWord)
        {
            string filePath = Path.Combine(directory, fileName + format);

            if (!File.Exists(filePath))
            {
                this.СreateFile(filePath, directory, format);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"{newWord}-{translatedWord}");

                writer.Close();
            }
        }
    }
}