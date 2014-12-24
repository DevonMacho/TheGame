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
		//change level to new game level
		Debug.Log("Starting new game in slot " + slot);
	}
	public void LoadGame(int slot)
	{
		//load data from file then load level
		Debug.Log("Loading from slot " + slot);
	}
	public void SaveGame(int slot)
	{

		Data.BasicInfo.Location = "here"; //set location to actual world value
		Debug.Log("Saving to slot " + slot);
	}
	//add save funciton
	
}
