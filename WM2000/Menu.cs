using UnityEngine;


public class Menu : MonoBehaviour
{
	// Use this for initialization
	Game OldSchoolHacker = new Game();
	void Start()
	{
		OldSchoolHacker.DisplayMainMenu("Hi Barry");
	}

	void OnUserInput(string input)
	{
		print(input);
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
				OldSchoolHacker.DisplayMainMenu("Hi to you ...");
			}
			else
			{
				OldSchoolHacker.DisplayLogonScreen("Wrong password, Try again");
			}
		}

		if (stepNumber == 3)
		{
			OldSchoolHacker.DisplayMainMenu("Hello Edrian");
		}


	}
	// Update is called once per frame
	void Update()
	{

	}
}
