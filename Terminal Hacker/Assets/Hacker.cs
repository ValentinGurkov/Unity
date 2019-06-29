using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game configuration
    private const string menuHint = "You can type menu at any time";

    private const string playAgain = "Play again for a greater challenge!";
    private string[] levelOnePasswords = { "shelf", "books", "history", "aisle", "password", "borrow", "font" };
    private string[] levelTwoPasswords = { "police", "prisoner", "law", "handcuffs", "holster", "arrest", "uniform" };
    private string[] levelThreePasswords = { "moon", "space", "star", "rocket", "astronaut", "scientist", "physics" };

    //Game state
    private int level;

    private string password;

    private enum Screen { MainMenu, Password, Win };

    private Screen currentScreen;

    // Start is called before the first frame update
    private void Start() {
        showMainMenu();
    }

    private void showMainMenu() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("Enter your selection: ");
    }

    private void OnUserInput(string input) {
        if (input == "menu") {
            showMainMenu();
        } else if (input == "quit" || input == "close") {
            Terminal.WriteLine("If on the web, close the tab.");
            Application.Quit();
        } else {
            switch (currentScreen) {
                case Screen.MainMenu:

                    RunMainMenu(input);
                    break;

                case Screen.Password:

                    CheckPassword(input);
                    break;

                case Screen.Win:
                    break;

                default:
                    Debug.LogError("Invalid screen: " + currentScreen);
                    break;
            }
        }
    }

    private void RunMainMenu(string input) {
        bool isValidNumber = input == "1" || input == "2" || input == "3";
        if (isValidNumber) {
            level = int.Parse(input);
            AskForPassword();
        } else {
            Terminal.WriteLine("Please choose a valid level.");
        }
    }

    private void AskForPassword() {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter your password. Hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    private void SetRandomPassword() {
        switch (level) {
            case 1:
                password = levelOnePasswords[Random.Range(0, levelOnePasswords.Length)];
                break;

            case 2:
                password = levelTwoPasswords[Random.Range(0, levelTwoPasswords.Length)];
                break;

            case 3:
                password = levelThreePasswords[Random.Range(0, levelThreePasswords.Length)];
                break;

            default:
                Debug.LogError("Invalid level number: " + level);
                break;
        }
    }

    private void CheckPassword(string input) {
        if (input == password) {
            DisplayWinScreen();
        } else {
            AskForPassword();
        }
    }

    private void DisplayWinScreen() {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    private void ShowLevelReward() {
        switch (level) {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(playAgain);
                Terminal.WriteLine(@"

           _.-\
       _.-      \
    , -          \
   ( \            \
    \ \            \
     \ \            \
      \ \         _.-;
       \ \    _.-    :
        \ \,-    _.-
         \(_.-
          `--
                    ");
                break;

            case 2:
                Terminal.WriteLine("You got the prison cells key!");
                Terminal.WriteLine(playAgain);
                Terminal.WriteLine(@"

     ,o.          8 8
    d   bzzzzzzzza8o8b
     `o'

                ");
                break;

            case 3:
                Terminal.WriteLine("You got into space!");
                Terminal.WriteLine("You completed the game on the hardest difficulty!");
                Terminal.WriteLine(@"
 _ __   __ _ ___  __ _
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___/\__,_|

                ");
                break;

            default:
                Debug.LogError("Invalid level number: " + level);
                break;
        }
    }

    // Update is called once per frame
    private void Update() {
    }
}