using UnityEngine;
using System.Collections;

public class GenericCommands : MonoBehaviour {
    private static int quitStage = 0;
    //0 - not quitting
    //1 - quitting prompted / triggered
    //2 - are you sure
    //3 - do you want to save
    //4 - triggering shutdown
    public static string clear()
    {
        return "<<Clearing>>";
    }
    public static string startQuit()
    {
        quitStage = 1;
        return "<<Quitting>>";
    }
    public static string quitParser(string input)
    {
        if (quitStage == 1)
        {
            quitStage = 2;
            return "Are you sure you want to quit?";
        }
        else if (quitStage == 2)
        {
            //cleanse output for this command
            string[] token = tokenize(input);
            if(token.Length <= 0 && token.Length > 1)
            {
                return "invalid input";
            }
             else if (token[0].ToLower().Equals("yes"))
            {
                quitStage = 3;
            }
            else if (token[0].ToLower().Equals("no"))
            {
                //if equal to game do something if not go back to menu
                if(ParserSelect.getPrevious() == )
            }
            else
            {
                return "invalid input";
            }
        }
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
        return "<<Quitting>>";
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
}
