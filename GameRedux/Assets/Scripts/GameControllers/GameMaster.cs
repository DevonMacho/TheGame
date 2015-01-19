using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameMaster : MonoBehaviour {

	public GameData Data;
	public static GameMaster GM;
	int _saveSlot;
	string _saveLoc;
	void Awake () 
	{
		if(GM == null)
		{
			GM = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(transform);
		_saveLoc = Application.persistentDataPath + "/SavedGames/";
		//Debug.Log(_saveLoc);
	}

	public void StartNewGame(int slot)
	{
		_saveSlot = slot;
		Application.LoadLevel("NewCharacter");
		//Debug.Log("Starting new game in slot " + slot);

	}
	public bool LoadGame(int slot)
	{
		if(Directory.Exists(_saveLoc))
		{
			if(File.Exists(_saveLoc+"Save"+slot+".cok"))
			{
				try
				{
					BinaryFormatter formatter = new BinaryFormatter();
					FileStream stream = File.Open(_saveLoc+"Save"+slot+".cok",FileMode.Open);
					GameData NullTest = (GameData)formatter.Deserialize(stream);
					stream.Close();
					if(NullTest == null)
					{
						return false;
					}
					Data = NullTest;
					_saveSlot = slot;
					Application.LoadLevel("Game");
					//Debug.Log("Loading from slot " + slot);
					//DebugShowInfo();
					return true;
				}
				catch
				{
					return false;
				}
			}
			else
			{
				return false;
			}

		}
		else
		{
			return false;
		}

	}
	public BasicGameInfo LoadInfo(int slot)
	{
		if(Directory.Exists(_saveLoc))
		{
			//Debug.Log("Directory Exists");
			if(File.Exists(_saveLoc+"Save"+slot+".cok"))
			{
				//Debug.Log("File Exists: " + slot);
				try
				{

					BinaryFormatter formatter = new BinaryFormatter();
					FileStream stream = File.Open(_saveLoc+"Save"+slot+".cok",FileMode.Open);
					//Debug.Log("Before Deserialize");
					GameData Binfo = (GameData)formatter.Deserialize(stream);
					//Debug.Log("After Deserialize");

					stream.Close();
					return Binfo.BasicInfo;
				}
				catch
				{
					return null;
				}
			}
			else
			{
				return null;
			}
			
		}
		else
		{
			return null;
		}
	}
	public bool SaveGame(string location)
	{

		BinaryFormatter formatter = new BinaryFormatter(); 
		if(!Directory.Exists(_saveLoc))
		{
			Directory.CreateDirectory(_saveLoc);
		}
		try
		{
			Data.BasicInfo.Location = location;
			FileStream file = File.Open(_saveLoc + "Save"+_saveSlot+".cok",FileMode.Create);
			formatter.Serialize(file,GM.Data);
			file.Close();
			return true;
		}
		catch
		{
			return false;
		}
	}
	public void DebugShowInfo()
	{
		Debug.Log("Name: " + Data.BasicInfo.Name);
		Debug.Log("Class: " + Data.BasicInfo.CharClass);
		Debug.Log("Gender: " + Data.Gender);
	}

	
}
