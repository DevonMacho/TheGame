using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSelector : MonoBehaviour {

	public bool Loading;
	public bool NewGame;
	public bool Deleting;
	public bool Copying;
	public GameObject[] Files;

	void Start () 
	{
	
	}

	public void PullSaveData()
	{
		//get data from the files and populate the file button text

		//foreach file, generate text for them
	}
	public void LoadState()
	{
		Loading = true;
		NewGame = false;
		Deleting = false;
		Copying = false;
	}
	public void NewGameState()
	{
		Loading = false;
		NewGame = true;
		Deleting = false;
		Copying = false;
	}
	void Update () 
	{
	
	}
}
