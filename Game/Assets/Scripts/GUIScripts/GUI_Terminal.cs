using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Terminal : MonoBehaviour
{
    static bool mini = true;
    static bool stat = true;
    static string input = "";
    static Rect miniMap;
    static Rect stats;
    static Vector2 scrollPosition;
    public static string consoleLog = "";
    static List<string> commandHistory;
    static Texture clb;
    static GUISkin skin;
    static int commandIndex;



    public static void Start()
    {
        consoleLog = "";
        clb = Resources.Load("GUI Assets/CommandLineBackground") as Texture;
        skin = Resources.Load("GUI Assets/GameSkin") as GUISkin;
       
    }
    public static string toggleStats()
    {
        stat = !stat;
        if(!stat)
        {
            return "The stat window was turned off";
        }
        else
        {
            return "The stat window was turned on";
        }
    }
    public static string toggleMiniMap()
    {
        mini = !mini;
        if(!mini)
        {
            
            return "minimap was turned off";
        }
        else
        {
            return "minimap was turned on";
        }  
    }

    public static void Update()
    {

        miniMap = new Rect(GlobalSettings.MinimapX, GlobalSettings.MinimapY, Screen.height / 6, Screen.height / 6);
        stats = new Rect(GlobalSettings.StatsX, GlobalSettings.StatsY, Screen.width / 4, Screen.height * 99 / 200);
    }

    public static void OnGUI()
    {
        if (mini)
        {
            GUI.Box(miniMap, "Minimap Goes here");
        }
        if (stat)
        {
            GUI.Box(stats, "stats go here");
        }
        if (commandHistory == null)
        {
            commandHistory = new List<string>();
            commandHistory.Add("");
            commandIndex = 1;
        }
        GUI.skin = skin;
        GUI.DrawTexture(new Rect(0, Screen.height - (Screen.height * 256 / 768), Screen.width, Screen.height * 256 / 768), clb, ScaleMode.StretchToFill, true, 10.0F);
        GUILayout.BeginArea(new Rect(24, Screen.height - (Screen.height * 240 / 768), Screen.width - 48, (Screen.height * 240 / 768)));
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width - 48), GUILayout.Height(Screen.height * 179 / 768));
        GUILayout.Label(consoleLog);
        GUILayout.EndScrollView();
        if (GUI.GetNameOfFocusedControl() == "textField")
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
            {
                SubmitCommand();
            }
            else if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
            {
                if (commandIndex < commandHistory.Count)
                {
                    commandIndex++;
                    input = commandHistory [commandIndex - 1];
                }
            }
            else if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
            {
                if (commandIndex > 1)
                {
                    commandIndex--;
                    input = commandHistory [commandIndex - 1];
                }
            }
        }
        if (GUI.Button(new Rect(0, (Screen.height * 179 / 768), 60, (Screen.height * 40 / 768)), "Submit"))
        {
            SubmitCommand();
        }
        GUI.SetNextControlName("textField");
        input = GUI.TextField(new Rect(70, (Screen.height * 179 / 768), Screen.width - 120, (Screen.height * 40 / 768)), input);
        GUILayout.EndArea();
    }

    static void SubmitCommand()
    {
        //parser selecter
        string output = ParserSelect.Parser(input);
        //end parser swap
        commandIndex = 1;
        scrollPosition.y = Mathf.Infinity;
        if (!input.Equals(""))
        {
            consoleLog = consoleLog + input + "\n";
            commandHistory.Insert(1, input);
        }
        if (ParserSelect.parserSelect == 4) // used for dialog because it looks nicer with dialog
        {
            consoleLog = consoleLog + "\n" + output + "\n";
        }
        else
        {
            consoleLog = consoleLog + output + "\n\n";
        }
        if (output.Equals("<<Clearing>>"))
        {
            consoleLog = "<<Cleared>>\n";
            commandHistory = new List<string>();
            commandHistory.Add("");
            commandIndex = 1;
        }
        input = "";
    }
}
