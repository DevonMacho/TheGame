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
	public Sprite[] backgrounds;
	public Sprite blank;
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
		_world = _flatWorld;
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
					_world =_flatWorld;
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
	/*
	Location[] _testWorld = new Location[]
	{
		new Location(0,"A","Node A / 0", new int[]{1,2}, new string[]{"southwest", "southeast"}, "Node A"), 
		new Location(1,"B","Node B / 1", new int[]{0,2,3,4}, new string[]{"northeast", "east", "south", "southeast"},"Node B"), 
		new Location(2,"C","Node C / 2", new int[]{0,1,3,4}, new string[]{"northwest", "west", "southwest", "south"},"Node C"), 
		new Location(3,"D","Node D / 3", new int[]{1,2,4}, new string[]{"north", "northeast", "east"},"Node D"), 
		new Location(4,"E","Node E / 4", new int[]{1,2,3}, new string[]{"northwest", "north", "west"},"Node E") 
		// 0: 1, 2
		// 1: 0, 2, 3, 4
		// 2: 0, 1, 3, 4
		// 3: 1, 2, 4
		// 4: 1, 2, 3
	};
	*/
	Location[] _flatWorld = new Location[]
	{
		new Location(0,"A","Node A / 0", new int[]{1}, new string[]{"east"}, "Node A"), 
		new Location(1,"B","Node B / 1", new int[]{0,2}, new string[]{"west", "east"}, "Node B"), 
		new Location(2,"C","Node C / 2", new int[]{1,3}, new string[]{"west", "east"}, "Node C"), 
		new Location(3,"D","Node D / 3", new int[]{2}, new string[]{"west"}, "Node D") 
	};
	Location[] _gameWorld = 
	{
		new Location(0,"A","This is certainly not earth, and doesn’t appear to be Uranus either (Although the smell is reminiscent of it).Scraps of what appears to be the research facility are scattered around, but the only useful thing seems to be a length of pipe.", new int[]{1}, new string[]{"east"}, "dead cats"), 
		new Location(1,"B","You follow a faint trail through a dense forest.", new int[]{0,2}, new string[]{"west", "east"},"a trailin the forest"), 
		new Location(2,"C","You come to a fork in the road. One trail leads northeast, the other leads southeast. The paths around here are dusty but seem well worn. ", new int[]{1,3,4}, new string[]{"west", "northeast", "southeast"},"fork"), 
		new Location(3,"D","You find some more pieces of the research facility scattered about including one storage container.", new int[]{2,5}, new string[]{"southwest","east"},"path with wreckage"), 
		new Location(4,"E","A small grassy area. There is not much here.", new int[]{2,7,6}, new string[]{"northwest", "northeast", "east"},"grassy area"),
		new Location(5,"F","More forest, but it seems to be thinning.", new int[]{3,7}, new string[]{"west", "east"},"thin forest"),
		new Location(6,"G","You run into what appears to be an alien that isn’t bent on your destruction.He greets you with “mor thalas”. While talking to him he tells you absolutely must go on pilgrimage like him to see the holy Claw in Scrubbington Village", new int[]{4,7}, new string[]{"west", "north"},"an alien"),
		new Location(7,"H","There is a village up ahead, the sign says 'Scrub Village'.You hear a tale from the head of the villagers the duke of Brillo that while they were worshipping the mighty Claw that fell from the heavens, the evil crayfish looking beast Kraymoar appeared and stole it then quickly retreated behind the massive stone gate to the east. The only way to open this gate is with the divine key which was lost in the wilds to the north long ago, but to this day no one has been brave enough to retrieve it. There is an inn to your right.", new int[]{5,4,6,18,8,24}, new string[]{"west", "southwest", "south", "east", "north", "right"},"The Village"),
		new Location(8,"I","You step into an area unlike anything you have seen before. Strange plants like ferns with tentacles surround you and the area reeks of monsters. ", new int[]{10,9,11,7}, new string[]{"north", "northeast", "northwest", "south"},"ferns"),
		new Location(9,"J","This area has the same strange ferns in it. Upon further inspection you see several items scattered about.", new int[]{8,12,10}, new string[]{"southeast", "north", "west"},"Thin ferns"),
		new Location(10,"K","You come across an almost perfectly circular area with many paths. You feel like this may be a representation of how life always moves in a circle, and that it might have some deeper insight into the meaning of life, or it could mean that you are lost in a strange land and your mind is starting to unravel slowly. Its probably the meaning of life though... sure. ", new int[]{8,9,14}, new string[]{"south", "east", "north"},"A circular area"),
		new Location(11,"L","There is a small ledge running along a steep cliff face with a long drop below it. You might be able to squeeze by. ", new int[]{8,14}, new string[]{"southeast", "north"},"a ledge"),
		new Location(12,"M","The landscape has changed slightly here, the ferns have given way to towering mushrooms in an array of spectacular colors.", new int[]{9,14,13}, new string[]{"south", "west", "north"},"mushrooms"),
		new Location(13,"N","There is a wide open area that seems to be surrounded by an almost impenetrable wall of towering mushrooms.", new int[]{12,14,16}, new string[]{"south", "southwest", "west"},"an open area"),
		new Location(14,"O","There is a statue of a man with a short beard and glasses pointing north. The inscription in the base reads “He points to the heavens and the heavens return NULL. - Great Adventurer Richard Hershey” ", new int[]{10,12,13,16,15}, new string[]{"south", "east", "northeast", "north", "west"},"a statue"),
		new Location(15,"P","There is a small open rocky area here, there isn’t much plant life.", new int[]{11,14,17}, new string[]{"south", "east", "north"},"rocky area"),
		new Location(16,"Q","There is a snaking pathway weaving between the local flora, which consists of both the towering mushrooms and the tentacle like ferns. ", new int[]{13,14,17}, new string[]{"east", "south", "west"},"both mushrooms and ferns"),
		new Location(17,"R","You can see the key the villagers spoke of, however it is hanging aroung the neck of a marble statue.", new int[]{16,15}, new string[]{"east", "south"},"a key"),
		new Location(18,"S","There stands before you a massive stone gate set between two un-scalable cliffs, you need a key to unlock it.", new int[]{19,7}, new string[]{"east", "west"},"gates"),
		new Location(19,"T","This area is covered by trees larger than any you’ve ever seen before. The atmosphere here seems a little off, anything that survives in a place like this must be tough. ", new int[]{18,20,21}, new string[]{"west", "south", "east"},"towering trees"),
		new Location(20,"U","There is only small patches of light filtering down from the impossibly high canopy.", new int[]{19,21,22}, new string[]{"north", "northeast", "east"},"dark forest"),
		new Location(21,"V","As you enter this area small bug like creatures scuttle away into the darkness. While nothing seems different about this area you can feel an evil aura emanating from the path eastward. ", new int[]{19,20,23}, new string[]{"west", "southwest", "east"},"dark ominous area"),
		new Location(22,"W","You are beginning to feel like you will never escape this gloomy collection of giants. Just as you start to dismiss this area you begin to feel something watching you from the path to the northeast.  ", new int[]{20,23}, new string[]{"west", "northeast"},"creepy area"),
		new Location(23,"X","You enter the area and it seems to be the first break from in the giant trees you’ve seen in ages. On the other side of this small clearing you see the grotesque Kraymoar.", new int[]{21,22}, new string[]{"west", "southwest"},"a dark pressence"),
		new Location(24,"Y","An inn stands here with a sign out front naming it the 'One Eyed Gopher Stroker'", new int[]{7}, new string[]{"left"},"")
	};
}
