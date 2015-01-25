using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameData
{
	[SerializeField]

	BasicGameInfo
		_basic;
	int _str;
	int _dex;
	int _con;
	int _int;
	int _wis;
	int _cha;
	int _xp;
	int _nodeNo;
	string _gender;
	//4 - quest item
	//7 - wearable
	//9 - consumable
	//10 - generic
	Item[] _items = new Item[]
	{
		new Item("Rock", "A large rock that can be used to hit kraymoar",5,false,"<Generic>",2,false,false,-1,null),
		new Item("Paper","A piece of paper that looks important","<Quest>",3),
		new Item("Scissors","A pair of good safety scissors",30,"<Weapon>",-1,true,new int[]
		{
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			2, //attack
			1 //defend
		})
	};
	//item list

	//newGame
	public GameData(string name, string charClass, string gender)
	{
		_basic = new BasicGameInfo(name, charClass, "NewGame", 0, 1);
		_gender = gender;

		//if character class do things... also roll motherfucka
		/*
                    Fighter: Str, Con, Dex
                    Ranger: Dex, Str, Wis
                    Rogue: Dex, Cha, Int
                    Wizard: Wis, Cha, Str
                    =========> Paladin: Con Str, Wis <========= Excluding due to forced alignment (Lawful Good. eww...)
                    Sorcerer: Cha, Int, Con
                    Cleric: Wis, Str, Con
		
                    I would like to thank the entire Silverfist clan for this info
        */

		_str = 10;
		_dex = 10;
		_con = 10;
		_int = 10;
		_wis = 10;
		_cha = 10;
		_xp = 0;
		_nodeNo = 0;

	}

	public int Strength
	{
		get
		{
			return _str;
		}
	}

	public int Dexterity
	{
		get
		{
			return _dex;
		}
	}

	public int Constitution
	{
		get
		{
			return _con;
		}
	}

	public int Intelligence
	{
		get
		{
			return _int;
		}
	}

	public int Wisdom
	{
		get
		{
			return _wis;
		}
	}

	public int Charisma
	{
		get
		{
			return _cha;
		}
	}

	public int Experience
	{
		get
		{
			return _xp;
		}
	}

	public string Gender
	{
		get
		{
			return _gender;
		}
	}

	public BasicGameInfo BasicInfo
	{
		get
		{
			return _basic;
		}
	}

	public int Node
	{
		get
		{
			return _nodeNo;
		}
		set
		{
			_nodeNo = value;
		}
	}

	public Item[] Items
	{
		get
		{
			return _items;
		}
		set
		{
			_items = value;
		}
	}
}
