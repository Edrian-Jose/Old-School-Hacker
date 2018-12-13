using System;
using System.Collections.Generic;
using System.Text;

public class Game
{

	readonly Random random = new Random();

	private int StepNumber;
	private int GameLevel;
	private string JambledPassword;
	private string CurrentPassword;

	enum Locations
	{
		Classroom = 1,
		University_Hall = 2,
		Chemistry_Lab = 3
	}

	private string[,] Passwords = new string[3, 5]
	{
		{"whiteboard","notebook","armchair","table","ballpen"},				// Classroom passwords
		{"podium","spotlight","curtains","backdrop","microphone"},			// University Hall
		{"erlenmeyer","crucible","forcep","thermometer","bunsen burner"}	// Chemistry Lab	
	};


	public Game()
	{
		StepNumber = 1;
		GameLevel = 0;
	}
	

	public void DisplayMainMenu(string greeting)
	{
		this.StepNumber = 1;
		Terminal.ClearScreen();
		Terminal.WriteLine(greeting + "\n");
		Terminal.WriteLine("--------------------------------------");
		Terminal.WriteLine("What would you like to hack into? \n");
		Terminal.WriteLine("Press 1 : Classroom");
		Terminal.WriteLine("Press 2 : University Hall");
		Terminal.WriteLine("Press 3 : Chemistry Lab");		
		Terminal.WriteLine("--------------------------------------");
		Terminal.WriteLine("Enter your selection : ");
	}


	public void DisplayLogonScreen(string message)
	{  
		this.StepNumber = 2;
		var randomNum = random.Next(0, 5);
		CurrentPassword = Passwords[(GameLevel - 1), randomNum];
		JambledPassword = JambleTheWord(CurrentPassword);

		Terminal.ClearScreen();
		Terminal.WriteLine("--------------------------------------");
		Terminal.WriteLine("Level " + this.GameLevel + " : " + (Locations) GameLevel);
		Terminal.WriteLine("--------------------------------------\n");

		Terminal.WriteLine(message);
		Terminal.WriteLine("Enter the level password");
		Terminal.WriteLine("Hint : " + JambledPassword);
	}


	public void DisplayWinScreen()
	{
		this.StepNumber = 3;
		Terminal.ClearScreen();
		Terminal.WriteLine("\t------------------------------");
		Terminal.WriteLine("\t\t\t" + ((Locations) GameLevel).ToString().ToUpper());
		Terminal.WriteLine("\t\t\tACCESS GRANTED");
		Terminal.WriteLine("\t------------------------------");
		Terminal.WriteLine("\n\nenter anything...");

	}

	public void ValidateLevel(string input)
	{
		if (input == "menu")
		{
			DisplayMainMenu("Hello again ..");
		}
		else if (input == "007")
		{
			DisplayMainMenu("Hola Mr. James Bond");
		}
		else if (input == "3" || input == "2" || input == "1")
		{
			this.GameLevel = int.Parse(input);
			Terminal.WriteLine("You’ve chosen level " + input);
			DisplayLogonScreen("Welcome master");
		}
		else
		{
			Terminal.WriteLine("Please enter a valid level");
		}
	}

	public bool ValidatePassword(string input)
	{
		if (CurrentPassword == input)
		{
			return true;
		}
		return false;
	}

	public int ReturnStep()
	{
		return this.StepNumber;
	}

	


	public string JambleTheWord(string word)
	{
		
		var unJambledWord = new StringBuilder(word);
		var jambledWord = new StringBuilder();
		var replacedIndex = new List<int>();

		if (word.Contains(" "))
		{
			var words = word.Split(' ');
			foreach (var _word in words)
			{
				jambledWord.Insert(jambledWord.Length, JambleTheWord(_word) + " ");
			}
		}
		else
		{
			int indexer;

			for (int i = 0; i < unJambledWord.Length; i++)
			{
				do
				{
					indexer = random.Next(0, (unJambledWord.Length));
				} while ((replacedIndex.Contains(indexer) && replacedIndex.Count != unJambledWord.Length));

				jambledWord.Append(unJambledWord[indexer]);
				replacedIndex.Add(indexer);

			}
		}
		

		return jambledWord.ToString();
	}

}
