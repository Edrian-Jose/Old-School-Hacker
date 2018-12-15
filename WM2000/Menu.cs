using System.Linq;
using UnityEngine;


public class Menu : MonoBehaviour
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
