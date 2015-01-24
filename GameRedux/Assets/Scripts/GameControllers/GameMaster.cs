using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameMaster : MonoBehaviour
{

	GameData _data;
	Location[] _world;
	public static GameMaster GM;
	int _saveSlot;
	string _saveLoc;
	bool _newGame = true;
	void Awake()
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
		_newGame = true;
		_world = _testWorld;
		Application.LoadLevel("NewCharacter");
		//Debug.Log("Starting new game in slot " + slot);

	}

	public bool LoadGame(int slot)
	{
		if(Directory.Exists(_saveLoc))
		{
			if(File.Exists(_saveLoc + "Save" + slot + ".cok"))
			{
				try
				{
					BinaryFormatter formatter = new BinaryFormatter();
					FileStream stream = File.Open(_saveLoc + "Save" + slot + ".cok", FileMode.Open);
					GameData NullTest = (GameData)formatter.Deserialize(stream);
					stream.Close();
					if(NullTest == null)
					{
						return false;
					}
					_data = NullTest;
					_world =_testWorld;
					_saveSlot = slot;
					if(_data.Node != 0)
					{
						_newGame = false;
					}
					else
					{
						_newGame = true;
					}
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
			if(File.Exists(_saveLoc + "Save" + slot + ".cok"))
			{
				//Debug.Log("File Exists: " + slot);
				try
				{

					BinaryFormatter formatter = new BinaryFormatter();
					FileStream stream = File.Open(_saveLoc + "Save" + slot + ".cok", FileMode.Open);
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
			_data.BasicInfo.Location = location;
			FileStream file = File.Open(_saveLoc + "Save" + _saveSlot + ".cok", FileMode.Create);
			formatter.Serialize(file, _data);
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
		Debug.Log("Name: " + _data.BasicInfo.Name);
		Debug.Log("Class: " + _data.BasicInfo.CharClass);
		Debug.Log("Gender: " + _data.Gender);
	}

	
	public GameData Data
	{
		get
		{
			return _data;
		}
		set
		{
			_data = value;
		}
	}

	public Location[] World
	{
		get
		{
			return _world;
		}
		set
		{
			_world = value;
		}
	}

	public bool NewGame
	{
		get
		{
			return _newGame;
		}
		set
		{
			_newGame = value;
		}
	}

	Location[] _testWorld = new Location[]
	{
		new Location(0,"A","Node A / 0", new int[]{1,2}, new string[]{"southwest", "southeast"}), 
		new Location(1,"B","Node B / 1", new int[]{0,2,3,4}, new string[]{"northeast", "east", "south", "southeast"}), 
		new Location(2,"C","Node C / 2", new int[]{0,1,3,4}, new string[]{"northwest", "west", "southwest", "south"}), 
		new Location(3,"D","Node D / 3", new int[]{1,2,4}, new string[]{"north", "northeast", "east"}), 
		new Location(4,"E","Node E / 4", new int[]{1,2,3}, new string[]{"northwest", "north", "west"}) 
		// 0: 1, 2
		// 1: 0, 2, 3, 4
		// 2: 0, 1, 3, 4
		// 3: 1, 2, 4
		// 4: 1, 2, 3
	};
}
