namespace translator
{
	public class Translate
	{
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

				Console.Write("Выберите пункт меню: ");
				ConsoleKeyInfo userInputSymbol;

				do
				{
					userInputSymbol = Console.ReadKey(true);
				} while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D5);

				numberMenuElement = Convert.ToInt32(userInputSymbol.KeyChar.ToString());

				switch (numberMenuElement)
				{
				}
			}
		}
	}
}
