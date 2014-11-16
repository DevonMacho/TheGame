using UnityEngine;
using System.Collections;
using System.IO;

public class GenericCommands : MonoBehaviour
{
    private static int quitStage = 0;
    //0 - not quitting
    //1 - quitting prompted / triggered
    //2 - do you want to save
    public static string clear(string[] token)
    {
        if (token.Length > 1)
        {
            return "too many args";
        }
        return "<<Clearing>>";
    }

    static int gameState;

    public static string startQuit(string[] token)
    {
        if (token.Length > 1)
        {
            return "Too many args";
        }
        quitStage = 1;
        gameState = ParserSelect.parserSelect;
        return "Are you sure you want to quit?";
    }

    public static string quitParser(string input)
    {
       
        if (quitStage == 1)
        {
            //always returns to main menu -- fixthat
            string[] token = tokenize(input);

            if (token.Length <= 0 || token.Length > 1)
            {
                return "invalid input\nDo you want to quit?";
            }
            else if (token [0].ToLower().Equals("yes"))
            {
                if (gameState == 0)
                {
                    return quit();
                }
                else if (gameState == 1)
                {
                    quitStage = 2;
                    return "Do you want to save?";
                }

            }
            else if (token [0].ToLower().Equals("no"))
            {
                //if equal to game do something if not go back to menu
                if (gameState == 0)
                {
                    ParserSelect.parserSelect = gameState;
                    return "<<returning to menu>>";
                }
                else if (gameState == 1)
                {
                    ParserSelect.parserSelect = gameState;
                    return "<<returning to game>>";
                }
                else
                {
                    return "Guru Meditation x0000004";
                }
            }
            else
            {
                quitStage = 1;
                return "invalid input\nDo you want to quit?";
            }
        }
        if (quitStage == 2)
        {
            string[] token = tokenize(input);
            if (token.Length <= 0 || token.Length > 1)
            {
                return "invalid input\nDo you want to save?";
            }
            else if (token [0].ToLower().Equals("yes"))
            {
                quitStage = 3;
                return GameData.startSave(token);

            }
            else if (token [0].ToLower().Equals("no"))
            {
                return quit();
            }
            else
            {
                return "invalid input";
            }
        }
        if (quitStage == 3)
        {
            return quit();
        }
        else
        {
            return "Guru Meditation x0000003";
        }

    }

    public static string[] tokenize(string tkn)
    {
        string[] tokens;
        tkn = tkn.ToLower();
        tokens = tkn.Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length <= 0)
        {
            return new string[0];
        }
        return tokens;
    }

    public static string[] tokenizeKeepCase(string tkn)
    {
        string[] tokens;
        tokens = tkn.Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length <= 0)
        {
            return new string[0];
        }
        return tokens;
    }

    public static string quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
        return "<<Quitting>>";
    }

    public static bool checkForFiles(string extension, string path)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/" + path + "/"))
        {
            return false;
        }
        int fileCount = 0;
        string[] UncleanScenarios = Directory.GetFiles(Application.persistentDataPath + "/" + path + "/");
        foreach (string a in UncleanScenarios)
        {
            
            string[] cleaner = a.Trim().Replace("/", " ").Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
            if (cleaner [cleaner.Length - 1].ToLower().Contains(extension))
            {
                fileCount ++;
            }
            
        }
       
        if (fileCount > 0)
        {
            return true;
        }
        return false;
        
    }

    static int menustage = 0;

    public static string startMainMenu(string[] input)
    {
        menustage = 0;
        if (input.Length <= 0)
        {
            return "invalid input";
        }
        else if (input.Length > 1)
        {
            return "too many args";
        }
        else if (input.Length == 1)
        {
            ParserSelect.parserSelect = 6;
            menustage = 1;
            return "Are you sure you want to return to the menu?";
        }
        else
        {
            return "Guru Mediation x0000009";
        }

    }

    public static string returnMenuParser(string input)
    {
        string[] token = tokenize(input);
        if (token.Length <= 0)
        {
            return "invalid input";
        }
        else if (token.Length > 1)
        {
            return "too many args";
        }
        else if (menustage == 1)
        {
            if (token [0] == "yes")
            {


                if (WorldData.inBattle())
                {
                    returnToMenu("You can not save in battle, returning to the main menu");
                    return "<<Returning to menu>>";
                }
                else
                {
                    menustage = 2;
                    return "would you like to save?";
                }
                   
            }
            else if (token [0] == "no")
            {
                menustage = 0;
                ParserSelect.parserSelect = 1;
                return "<<returning to game>>";
            }
            else
            {
                return "invalid input";
            }

        }
        else if (menustage == 2)
        {
            if (token [0] == "yes")
            {
                return GameData.startSave(token);
            }
            else if (token [0] == "no")
            {

                menustage = 0;
                GenericCommands.returnToMenu("you are being returned to the main menu");
                return "<<returning to the menu>>";
            }
            else
            {
                return "invalid input";
            }
        }
        return "Guru Mediation x000000A";
    }

    public static void returnToMenu(string input)
    {
        WorldData.gameData = null;
        GUISelector.message = input;
        GUISelector.Gui = 3;
        GUISelector.PreviousGui = 0;
    }


}
