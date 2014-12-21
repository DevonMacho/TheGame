using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	public Options Opt;
	void Awake () 
	{
		#if UNITY_EDITOR
			//YOU HAVE TO PURGE PLAYERPREFS BEFORE DEPLOY!!!
			PlayerPrefs.DeleteAll();
		#endif
		if (Opt.CheckPrefs())
		{
			Opt.LoadPrefs();
		}
		else
		{
			Opt.SetPrefDefaults();
			Opt.LoadPrefs();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void StartNewGame()
	{
		Debug.Log("Starting New Game");
	}
	public void LoadGame()
	{
		Debug.Log("Loading Game");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
