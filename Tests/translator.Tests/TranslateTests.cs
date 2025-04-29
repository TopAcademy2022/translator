namespace translator.Tests
{
	public class TranslateTests
	{
		[Fact]
		/*! 
		* @brief Checking the word search method.
		*/
		public void FindWordTest()
		{
			Translate test = new Translate(); //< The creation of our class

			string key = "hello";
			test.AddWord(key, "привет"); //< Adding a word

			Assert.True(test.FindWord(key) && test.DictionaryTranslate.ContainsKey(key)); //< Checking the search method
		}

		[Fact]
		/*!
		 * @brief Checking the addword method
		 */
		public void AddWordTest()
		{
			Translate test = new Translate(); ///< The creation of our class

			string word = "hello";
			string translatedWord = "привет";

			test.AddWord(word, translatedWord); ///< Adding a word

			Assert.True(test.DictionaryTranslate.Count > 0); ///< Checking if word was added
		}
		[Fact]
		/*! 
		* @brief Checking the Add test for DeleteWord method.
		*/
		public void DeleteWordTest()
		{
            Translate test = new Translate();///< The creation of our class

			string word = "hello";
            string translatedWord = "привет";

			test.AddWord(word, translatedWord);///< Adding a word to dictionary

            test.DeleteWord(word);///< Deleting the word
            Assert.Empty(test.DictionaryTranslate);///< Checking if word was deleted
        }

    }
}