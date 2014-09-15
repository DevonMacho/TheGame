using UnityEngine;
using System.Collections;

public class GUI_Terminal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public string input = "Enter a command";
	public Vector2 scrollPosition;
	public string consoleLog = "logity log log\nLOGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGASDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDQQQQQQQQQQQQQQWWWWWWWWWWWWWWWWWWWWWW\naASDAs\nAsdasdweqwe";
	void OnGUI () 
	{
		//todo... add in a box to contain both the input and the log.
		// "Submit command" button as well as getting enter to work properly
		// after submit add to log, if clear command is entered, clear the log
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width - 20), GUILayout.Height (Screen.height- 120));
		GUILayout.Label (consoleLog);

		GUILayout.EndScrollView();
		input = GUI.TextField(new Rect(10, Screen.height- 25, Screen.width - 20,20), input);
	}
}
