using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Terminal : MonoBehaviour
{

    void Start()
    {

    }

    protected string input = "";
    public Vector2 scrollPosition;
    protected string consoleLog = "";
    private static List<string> commandHistory;
    public Texture clb; //Command Line Background
    public GUISkin skin;
    private static int commandIndex;

    void OnGUI()
    {

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
    void SubmitCommand()
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

        consoleLog = consoleLog + output + "\n";
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
