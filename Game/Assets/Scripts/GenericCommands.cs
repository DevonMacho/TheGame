using UnityEngine;
using System.Collections;

public class GenericCommands : MonoBehaviour {

    public static string clear()
    {
        return "<===Clearing===>";
    }
    public static string quit()
    {
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
