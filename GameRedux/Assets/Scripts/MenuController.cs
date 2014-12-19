using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
		Debug.Log("Quitting Game");
	}
}
