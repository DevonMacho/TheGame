using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public GameData Data;
	int _saveSlot;

	void Awake () 
	{
		DontDestroyOnLoad(transform);
		
	}

	public void StartNewGame(int slot)
	{
		Application.LoadLevel("NewCharacter");
		Debug.Log("Starting new game in slot " + slot);
	}
	public void LoadGame(int slot)
	{
		//load data from file then load level IENumerable?
		Application.LoadLevel("Game");
		Debug.Log("Loading from slot " + slot);
	}
	public void SaveGame(int slot)
	{
		//add save funciton
		Data.BasicInfo.Location = "here"; //set location to actual world value
		Debug.Log("Saving to slot " + slot);
	}

	
}
