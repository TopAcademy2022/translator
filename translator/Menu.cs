using Microsoft.Extensions.Logging;

namespace translator
{
    public class Menu
    {
        private Translate _translator;
        private ILogger<Menu> _logger;

        public Menu(Translate translator)
        {
            _translator = translator;

            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<Menu>();
        }

        public void PrintMenu()
        {
            const uint NUMBER_EXIT_MENU_ELEMENT = 9;
            int numberMenuElement = 0;

            while (numberMenuElement != NUMBER_EXIT_MENU_ELEMENT)
            {
                Console.Clear();
                Console.WriteLine("Menu translator:");
                Console.WriteLine("1. Create translator");
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
                            if (_translator.СreateFile(fileName, destinationDirectory))
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
                            _translator.AddWord(newWord, translatedWord);
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
                            if (_translator.DeleteWord(deletedWord))
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
                            if (_translator.FindWord(searchWord))
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
                            if (_translator.LoadDictionaryFile(loadFileName, loadFileDirectory))
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
                            if (_translator.SaveDataToFile(saveFileName, saveFileDirectory))
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
                        _translator.PrintDictionary();
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}