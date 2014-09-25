using UnityEngine;
using System.Collections;

public class GUI_Terminal : MonoBehaviour {

	void Start () 
	{
	}
	protected string input = "";
	public Vector2 scrollPosition;
	protected string consoleLog = "";
	protected ArrayList commandHistory = new ArrayList();
	public Texture clb; //Command Line Background
	public GUISkin skin;
	void OnGUI () 
	{
		GUI.skin = skin;
		GUI.DrawTexture(new Rect(0,  Screen.height - (Screen.height * 256/768), Screen.width, Screen.height * 256/768), clb, ScaleMode.StretchToFill, true, 10.0F);


		GUILayout.BeginArea(new Rect(16, Screen.height -  (Screen.height * 240/768), Screen.width - 28, (Screen.height * 240/768)));
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width - 28), GUILayout.Height (Screen.height * 179/768));
		GUILayout.Label (consoleLog);
		GUILayout.EndScrollView();

		if(GUI.GetNameOfFocusedControl() == "textField")
		{
			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) 
			{
				SubmitCommand();
			}
		}
		if(GUI.Button(new Rect(0, (Screen.height * 179/768), 60,(Screen.height * 40/768) ),"Submit"))
		{
			SubmitCommand();
		}
		GUI.SetNextControlName("textField");
		input = GUI.TextField(new Rect(70,  (Screen.height * 179/768), Screen.width - 100,(Screen.height * 40/768)), input);
		GUILayout.EndArea();

	}
	void SubmitCommand()
	{

		string output = Parser.Parse(input);
		if(!input.Equals(""))
		{
			consoleLog =  consoleLog + input +"\n";
			scrollPosition.y = Mathf.Infinity;
		}

		consoleLog = consoleLog + output + "\n" ;
		if(output.Equals("<===Clearing===>"))
		{
			consoleLog = "<===Cleared===>\n";
		}
		input = "";
	}
}
