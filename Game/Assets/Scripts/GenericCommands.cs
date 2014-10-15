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
        return "Quitting...";
    }

}
