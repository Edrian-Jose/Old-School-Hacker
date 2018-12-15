using UnityEngine;
using System;
using System.Text;
using System.Linq;

public class Hacker : MonoBehaviour
{
	// Use this for initialization
	Game OldSchoolHacker = new Game();
	void Start()
	{
		OldSchoolHacker.DisplayWelcomeScreen();
	}

	void OnUserInput(string input)
	{
		print(input);
		var stepNumber = OldSchoolHacker.ReturnStep();
		if (stepNumber == 0 && input == "menu")
		{
			OldSchoolHacker.DisplayMenuScreen("Hi barry");
		}
		else if (stepNumber == 1)
		{
			OldSchoolHacker.ValidateLevel(input);
		}

		else if (stepNumber == 2)
		{
			var remark = OldSchoolHacker.ValidatePassword(input);
			if (remark)
			{
				OldSchoolHacker.DisplayWinScreen();
			}
			else if (OldSchoolHacker.Screens.Contains(input))
			{
				OldSchoolHacker.DisplayScreen(input);
			}
			else
			{
				OldSchoolHacker.DisplayLogonScreen("Wrong password, Try again");
			}
		}
		else
		{
			OldSchoolHacker.DisplayWelcomeScreen();
		}
	}
	// Update is called once per frame
	void Update()
	{

	}
}

public class Game
{

	readonly System.Random random = new System.Random();

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
		{"white board","notebook","armchair","table","ballpen"},			// Classroom passwords
		{"podium","spotlight","curtains","backdrop","microphone"},			// University Hall
		{"erlenmeyer","crucible","forcep","thermometer","bunsen burner"}	// Chemistry Lab	
	};

	public string[] Screens = new string[] { "help", "menu", "exit", "?" };

	public Game()
	{
		StepNumber = 0;
		GameLevel = 0;
	}

	public void DisplayWelcomeScreen()
	{
		this.StepNumber = 0;
		this.GameLevel = 0;
		Terminal.ClearScreen();
		Terminal.WriteLine("      WELCOME TO HACKER GAME ONLINE!");
		Terminal.WriteLine("	      ____________________	");
		Terminal.WriteLine("	     |	 ____      ____   |	");
		Terminal.WriteLine("	     |  |    |    |    |  |	");
		Terminal.WriteLine("	     |  |____|    |____|  |	");
		Terminal.WriteLine("	     |                    |	");
		Terminal.WriteLine("	     |        ____        |	");
		Terminal.WriteLine("	     |        \\__/        |	");
		Terminal.WriteLine("	     |____________________|	");
		Terminal.WriteLine("\n     Enter \"menu\" to start the game");

	}

	public void DisplayMenuScreen(string greeting)
	{
		this.StepNumber = 1;
		Terminal.ClearScreen();
		Terminal.WriteLine(greeting + "\n");
		Terminal.WriteLine("--------------------------------------");
		Terminal.WriteLine("What would you like to hack into? \n");
		Terminal.WriteLine("Enter 1 : Classroom");
		Terminal.WriteLine("Enter 2 : University Hall");
		Terminal.WriteLine("Enter 3 : Chemistry Lab");
		Terminal.WriteLine("--------------------------------------");
		Terminal.WriteLine("Enter \"menu\" or \"?\" for help ");
		Terminal.WriteLine("Enter your selection below: ");
	}


	public void DisplayLogonScreen(string message)
	{
		this.StepNumber = 2;
		var randomNum = random.Next(0, 5);
		CurrentPassword = Passwords[(GameLevel - 1), randomNum];
		JambledPassword = JambleTheWord(CurrentPassword);

		Terminal.ClearScreen();
		Terminal.WriteLine("--------------------------------------");
		Terminal.WriteLine("Level " + this.GameLevel + " : " + (Locations)GameLevel);
		Terminal.WriteLine("--------------------------------------\n");

		Terminal.WriteLine(message);
		Terminal.WriteLine("Enter the level password");
		Terminal.WriteLine("Hint : " + JambledPassword);
		Terminal.WriteLine("Enter \"menu\" or \"?\" for help ");

	}


	public void DisplayWinScreen()
	{
		this.StepNumber = 3;
		Terminal.ClearScreen();
		Terminal.WriteLine("             _______________       ");
		Terminal.WriteLine("        ____|              |____  ");
		Terminal.WriteLine("        \\   |              |   /   ");
		Terminal.WriteLine("         \\__|    YOU WON   |__/    ");
		Terminal.WriteLine("            \\              /       ");
		Terminal.WriteLine("             \\____________/         ");
		Terminal.WriteLine("   	 	         |    |              ");
		Terminal.WriteLine("	          ___|____|___         ");
		Terminal.WriteLine("	          |          |          ");
		Terminal.WriteLine("	          |__________|          ");
		Terminal.WriteLine("      enter anything to restart...");

	}

	public void DisplayHelpScreen()
	{

		Terminal.ClearScreen();
		Terminal.WriteLine("List of selection you can enter \n");
		Terminal.WriteLine("Help / ? : Go to Help Screen");
		Terminal.WriteLine("menu     : Go to Menu Screen");
		Terminal.WriteLine("exit     : Exit the Game");
		if (StepNumber == 1)
		{
			Terminal.WriteLine("1 / 2 / 3: Go to selected level");
		}
	}

	public void DisplayScreen(string input)
	{
		if (input == "menu")
		{
			DisplayMenuScreen("Hello again ..");
		}
		else if (input == "help" || input == "?")
		{
			DisplayHelpScreen();
		}
		else if (input == "exit")
		{
			DisplayWelcomeScreen();
		}
	}
	public void ValidateLevel(string input)
	{
		if (Screens.Contains(input))
		{
			DisplayScreen(input);
		}
		else if (input == "007")
		{
			DisplayMenuScreen("Hola Mr. James Bond");
		}
		else if (input == "3" || input == "2" || input == "1")
		{
			this.GameLevel = int.Parse(input);
			DisplayLogonScreen("Welcome master");
		}
		else
		{
			Terminal.WriteLine("Please enter a valid selection");
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

		var jambledWord = new StringBuilder();

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
			jambledWord.Insert(0, word.Anagram());
		}


		return jambledWord.ToString();
	}

}

