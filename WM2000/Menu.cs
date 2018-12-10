using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game
{
	List<string> locations = new List<string>() {"School", "Gym", "Church"};
	List<string> locationPasswords = new List<string>() { "cea3rdfloor", "barbel145", "GODiswithUS" };

	private int StepNumber = 1;
	private int GameLevel;

	public void DisplayMainMenu()
	{
		Terminal.ClearScreen();
		Terminal.WriteLine("What would you like to hack into? \n");

		for (int i = 0; i < locations.Count; i++)
		{
			Terminal.WriteLine("Press " + (i+1) + " : to enter " + locations[i]);
		}
		
	}

	public void DisplayLogonScreen()
	{
		Terminal.ClearScreen();
		Terminal.WriteLine("Level " + this.GameLevel + " : " + locations[this.GameLevel-1]);
		Terminal.WriteLine("\n Enter the level password");
	}

	public void DisplayWinScreen()
	{
		Terminal.ClearScreen();
		Terminal.WriteLine("You won");
	}

	public void ValidateLevel(string input)
	{
		if (input == "1" || input == "2" || input == "3")
		{
			Terminal.WriteLine("You’ve chosen level " + input);
			this.GameLevel = int.Parse(input);
			GoToStep(2);
			DisplayLogonScreen();
		}
		else if (input == "menu")
		{
			DisplayMainMenu();
		}
		else if (input == "007")
		{
			Terminal.WriteLine("Welcome James Bond, select a level");
		}
		else
		{
			Terminal.WriteLine("Please enter a valid level");
		}
	}

	public bool ValidatePassword(string input)
	{
		if (this.locationPasswords[this.GameLevel - 1] == input)
		{
			return true;
		}
		return false;
	}

	public void GoToStep(int step)
	{
		this.StepNumber = step;
	}

	public int ReturnStep()
	{
		return this.StepNumber;
	}



}


public class Menu : MonoBehaviour
{
	// Use this for initialization
	Game OldSchoolHacker = new Game();
	void Start()
	{
		OldSchoolHacker.DisplayMainMenu();
	}

	void OnUserInput(string input)
	{
		var stepNumber = OldSchoolHacker.ReturnStep();

		if (stepNumber == 1)
		{
			OldSchoolHacker.ValidateLevel(input);
		}

		if (stepNumber == 2)
		{
			var remark = OldSchoolHacker.ValidatePassword(input);
			if (remark)
			{
				OldSchoolHacker.DisplayWinScreen();
			}
			else if (input == "menu")
			{
				OldSchoolHacker.GoToStep(1);
				OldSchoolHacker.DisplayMainMenu();
			}
			else
			{
				OldSchoolHacker.DisplayLogonScreen();
			}
		}

	}
	// Update is called once per frame
	void Update()
	{

	}
}
