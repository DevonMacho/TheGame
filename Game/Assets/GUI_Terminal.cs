using UnityEngine;
using System.Collections;

public class GUI_Terminal : MonoBehaviour {

	void Start () 
	{
		Debug.Log ("Height: "+Screen.height);
		Debug.Log ("Width: "+Screen.width);
		//768
	}
	protected string input = "";
	protected Vector2 scrollPosition;
	protected string consoleLog = "";
	protected ArrayList commandHistory = new ArrayList();

	void OnGUI () 
	{

		GUILayout.BeginArea(new Rect(10, Screen.height -  (Screen.height * 123/768), Screen.width - 20, (Screen.height * 123/768)));
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width - 20), GUILayout.Height (Screen.height * 98/768));

		GUILayout.Label (consoleLog);
		GUILayout.EndScrollView();

		if(GUI.GetNameOfFocusedControl() == "textField")
		{
			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) 
			{
				SubmitCommand();
			}
		}
		if(GUI.Button(new Rect(0, (Screen.height * 98/768), (Screen.height * 60/768),(Screen.height * 20/768) ),"Submit"))
		{
			SubmitCommand();
		}
		GUI.SetNextControlName("textField");
		input = GUI.TextField(new Rect(70,  (Screen.height * 98/768), Screen.width - 90,(Screen.height * 20/768)), input);
		GUILayout.EndArea();

	}
	void SubmitCommand()
	{

		string output = Parser.Parse(input);
		if(!input.Equals(""))
		{
			consoleLog = input + "\n" + consoleLog;
		}

		consoleLog = output + "\n" + consoleLog;
		if(output.Equals("<===Clearing===>"))
		{
			consoleLog = "" + "<===Cleared===>";
		}
		input = "";
	}
}
