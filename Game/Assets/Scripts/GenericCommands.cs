using UnityEngine;
using System.Collections;

public class GenericCommands : MonoBehaviour {
    private static int quitStage = 0;
    //0 - not quitting
    //1 - quitting prompted / triggered
    //2 - do you want to save
    public static string clear()
    {
        return "<<Clearing>>";
    }
    public static string startQuit()
    {
        quitStage = 1;
        return "Are you sure you want to quit?";
    }
    public static string quitParser(string input)
    {
       
        if (quitStage == 1)
        {
            //cleanse output for this command
            string[] token = tokenize(input);
            if (token.Length <= 0 && token.Length > 1)
            {
                return "invalid input";
            }
            else if (token [0].ToLower().Equals("yes"))
            {
                if (ParserSelect.getPrevious() == 0)
                {
                    return quit();
                }
                else if (ParserSelect.getPrevious() == 1)
                {
                    quitStage = 2;
                    return "Do you want to save?";
                }

            }
            else if (token [0].ToLower().Equals("no"))
            {
                //if equal to game do something if not go back to menu
                if (ParserSelect.getPrevious() == 0)
                {
                    ParserSelect.parserSelect = ParserSelect.getPrevious();
                    return "<<returning to menu>>";
                }
                else if (ParserSelect.getPrevious() == 1)
                {
                    ParserSelect.parserSelect = ParserSelect.getPrevious();
                    return "<<returning to game>>";
                }
            }
            else
            {
                return "invalid input";
            }
        }
        if (quitStage == 2)
        {
            string[] token = tokenize(input);
            if (token.Length <= 0 && token.Length > 1)
            {
                return "invalid input";
            }
            else if (token [0].ToLower().Equals("yes"))
            {

                return "saving" + quit();
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
        else
        {
            return "broken code 0";
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
    private static string quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
        return "<<Quitting>>";
    }
}
