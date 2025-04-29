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

        public bool CreateFile(string fileName, string directory = "./DictionaryTranslate", string format = ".lge")
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

        public bool DeleteWord(string deletedWord)
        {
            if (this._dictionaryTranslate.ContainsKey(deletedWord))
            {
                return this._dictionaryTranslate.Remove(deletedWord);
            }

            return false;
        }

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

        public bool FindWord(string searchWord)
        {
            if (this._dictionaryTranslate.ContainsKey(searchWord))
            {
                return true;
            }

            return false;
        }

        public bool LoadDictionaryFile(string fileName, string directory = "./DictionaryTranslate", string format = ".lge")
        {
            string filePath = Path.Combine(directory, fileName + format);

            if (File.Exists(filePath))
            {
                foreach (string[] item in File.ReadAllLines(filePath).Select(line => line.Split('-')))
                {
                    this._dictionaryTranslate.Add(item[0], item[1]);
                }

                return true;
            }

            return false;
        }

        public bool SaveDataToFile(string fileName, string directory = "./DictionaryTranslate", string format = ".lge")
        {
            string filePath = Path.Combine(directory, fileName + format);

            if (!File.Exists(filePath))
            {
                this.CreateFile(filePath, directory, format);

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
    }
}