using System;
using System.Collections.Generic;
using System.Text;

public class Game
{
	readonly Random random = new Random();
	enum Locations
	{
		Classroom = 1,
		University_Hall = 2,
		Chemistry_Lab = 3
	}
	private string[,] Passwords = new string[3, 5]
	{
		{"blackboard","notebook","armchair","table","ballpen"},
		{"podium","spotlight","curtains","backdrop","microphone"},
		{"erlenmeyer","crucible","forcep","thermometer","bunsen burner"}
	};

	private int StepNumber;
	private int GameLevel;
	private string JambledPassword;
	private string CurrentPassword;

	public Game()
	{
		StepNumber = 1;
		GameLevel = 0;
	}
	
	public void DisplayMainMenu(string greeting)
	{
		this.StepNumber = 1;
		Terminal.ClearScreen();
		Terminal.WriteLine(greeting);
		Terminal.WriteLine("What would you like to hack into? \n");

		Terminal.WriteLine("Press 1 : to enter Classroom");
		Terminal.WriteLine("Press 2 : to enter University Hall");
		Terminal.WriteLine("Press 3 : to enter Chemistry Lab");		
		
	}
	

	public void DisplayLogonScreen(string message)
	{  
		this.StepNumber = 2;
		Terminal.ClearScreen();
		Terminal.WriteLine("Level " + this.GameLevel + " : " + (Locations) GameLevel + "\n");
		Terminal.WriteLine(message);
		Terminal.WriteLine("Enter the level password");
		Terminal.WriteLine("Hint : " + JambledPassword);
	}

	public void DisplayWinScreen()
	{
		this.StepNumber = 3;
		Terminal.ClearScreen();
		Terminal.WriteLine(" You won \n Enter anything to restart");
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
			var randomNum = random.Next(0, 5);
			CurrentPassword = Passwords[(GameLevel - 1), randomNum];
			JambledPassword = JambleTheWord(CurrentPassword);
			DisplayLogonScreen("Guess the jambled words");
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
			string[] words = word.Split(' ');
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
