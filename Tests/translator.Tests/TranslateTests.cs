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
			test.AddWord(key, "������"); //< Adding a word

            Assert.True(test.FindWord(key) && test.DictionaryTranslate.ContainsKey(key)); //< Checking the search method
		}
	}
}