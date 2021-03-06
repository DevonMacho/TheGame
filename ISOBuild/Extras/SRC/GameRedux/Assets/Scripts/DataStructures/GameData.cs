﻿using UnityEngine;
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
	int _hp;
	int _maxHp;
	int _mp;
	int _maxMp;
	string _gender;
	//4 - quest item
	//7 - wearable
	//9 - consumable
	//10 - generic

	//item list
	Item[] _items = new Item[]
	{
		new Item("Rock", "A large rock that can be used to hit kraymoar", 5, false, "<Generic>", 2, false, false, -1, null),
		new Item("Key", "Shiny golden key, it must be important", "<Quest>", 17),
		new Item("Cap", "The cap of Dunesay", 30, "<Head>", 2, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			-1, //int
			0, //wis
			0, //cha
			0, //attack
			2, //defend
			0 //health add
		}),
		new Item("KnitHelmet", "A helmet knit of strong wool, better for warmth than protection", 30, "<Head>", 6, true, new int[]
		    {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			1, //defend
			0 //health add
		}),
		new Item("LabCoat", "Lab coat of SCIENCE!!!", 30, "<Chest>", -3, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			2, //int
			0, //wis
			0, //cha
			0, //attack
			1, //defend
			0 //health add
		}),
		new Item("Khakis", "ragged khakis.Slightly burned from wormhole re-enrty", 30, "<Legs>", -6, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			1, //defend
			0 //health add
		}),
		new Item("Crocs", "why in gods name would you wear these?", 30, "<Feet>", -7, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			3, //defend
			0 //health add
		}),
		new Item("Gloves", "Mad scientist gloves", 30, "<Hands>", -4, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			1, //int
			0, //wis
			0, //cha
			1, //attack
			1, //defend
			0 //health add
		}),
		new Item("UtilityBelt", "Utility belt that can hold many tools. if only you had tools...", 30, "<Waist>", 3, true, new int[]
		         {
			1, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			1, //defend
			1 //health add
		}),
		new Item("Cat", "it appears to be one of the cats from your experimentation, it looks like it could be worn as a hat", 30, "<Head>", 0, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			1, //attack
			0, //defend
			0 //health add
		}),
		new Item("Pipe", "a length of pipe from the destroyed research facility", 30, "<Weapon>", 0, true, new int[]
		         {
			1, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			2, //attack
			0, //defend
			0 //health add
		}),
		new Item("LaserKnife", "used for toasting bread as you slice it.", 30, "<Weapon>", 9, true, new int[]
		         {
			0, //str
			1, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			5, //attack
			0, //defend
			0 //health add
		}),
		new Item("Chaps", "assless chaps retains armor value even while pooping", 30, "<Legs>", 13, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			1, //cha
			0, //attack
			3, //defend
			0 //health add
		}),
		new Item("FingerlessGloves", "They would lower your cool factor by 20. If that were a real stat", 30, "<Hands>", 16, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			0, //defend
			0 //health add
		}),
		new Item("Gauntlets", "Gauntlets of gauntleting", 30, "<Hands>", 11, true, new int[]
		         {
			1, //str
			1, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			2, //attack
			2, //defend
			1  //health add
		}),
		new Item("KnightsHelmet", "Helmet of a brave knight. it is unscracthed as the brave knight didnt feel the need to wear it in battle", 30, "<Head>", 9, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			1, //int
			0, //wis
			3, //cha
			1, //attack
			2, //defend
			0 //health add
		}),
		new Item("Boots", "squeaky boots", 30, "<Trinket>", 5, true, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			2, //defend
			0 //health add
		}),
		new Item("BusterSword", "every game needs a giant oversized sword", 30, "<Weapon>", 9, true, new int[]
		         {
			1, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			5, //attack
			1, //defend
			0 //health add
		}),
		new Item("Scissors", "A pair of good safety scissors", 30, "<Weapon>", -1, true, new int[]
		{
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			0, //defend
			1 //health add
		}),
		new Item("HealthPotion", "A health potion", 30, "<Consumable>", 12, true, true, 1, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			0, //defend
			10 //health add
		}),
		new Item("MegaPotion", "Better potion than health potion", 30, "<Consumable>", 20, true, true, 1, new int[]
		         {
			0, //str
			0, //dex
			0, //con
			0, //int
			0, //wis
			0, //cha
			0, //attack
			0, //defend
			20 //health add
		})



	};
	Enemy[] _enemies = new Enemy[] 
	{
		new Enemy("Kraymoar","A large crab monster",23,true,false,null,100,10)
	};

	int modRoll()
	{
		int retVal = (Random.Range(1, 2000) * Random.Range(1, 2000) )% 3;
		return retVal;
	}


	//world objects
	WorldObject[] _worldObjects = new WorldObject[] 
	{
		new WorldObject("Gate", "A large stone gate.", true, "Key", true, false, false,19, 18)
	};


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

		int highBegin = 12;
		int highEnd = 17;
		int lowBegin = 9;
		int lowEnd = 13;
		if(charClass.ToLower() == "Fighter".ToLower())
		{
			//Fighter: Str, Con, Dex

			int HighRoll = Random.Range(highBegin, highEnd);
			_str = HighRoll + modRoll();
			HighRoll = Random.Range(highBegin, highEnd);
			_dex = HighRoll;
			HighRoll = Random.Range(highBegin, highEnd);
			_con = HighRoll;



			int LowRoll = Random.Range(lowBegin, lowEnd);
			_int = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_wis = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_cha = LowRoll - modRoll();



		}
		else if(charClass.ToLower() == "Ranger".ToLower())
		{
			//Ranger: Dex, Str, Wis
			int HighRoll = Random.Range(highBegin, highEnd);
			_dex = HighRoll + modRoll();
			HighRoll = Random.Range(highBegin, highEnd);
			_str = HighRoll;
			HighRoll = Random.Range(highBegin, highEnd);
			_wis = HighRoll;



			int LowRoll = Random.Range(lowBegin, lowEnd);
			_con = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_int = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_cha = LowRoll - modRoll();
		}
		else if(charClass.ToLower() == "Rogue".ToLower())
		{
			// Rogue: Dex, Cha, Int
			int HighRoll = Random.Range(highBegin, highEnd);
			_dex = HighRoll + modRoll();
			HighRoll = Random.Range(highBegin, highEnd);
			_int = HighRoll;
			HighRoll = Random.Range(highBegin, highEnd);
			_cha = HighRoll;


			int LowRoll = Random.Range(lowBegin, lowEnd);
			_str = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_con = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_wis = LowRoll - modRoll();

		}
		else if(charClass.ToLower() == "Wizard".ToLower())
		{
			//Wizard: Wis, Cha, Str
			int HighRoll = Random.Range(highBegin, highEnd);
			_wis = HighRoll + modRoll();
			HighRoll = Random.Range(highBegin, highEnd);
			_str = HighRoll;
			HighRoll = Random.Range(highBegin, highEnd);
			_cha = HighRoll;

			int LowRoll = Random.Range(lowBegin, lowEnd);
			_dex = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_con = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_int = LowRoll - modRoll();


		}
		else if(charClass.ToLower() == "Sorcerer".ToLower())
		{
			//Sorcerer: Cha, Int, Con
			int HighRoll = Random.Range(highBegin, highEnd);
			_cha = HighRoll + modRoll();
			HighRoll = Random.Range(highBegin, highEnd);
			_con = HighRoll;
			HighRoll = Random.Range(highBegin, highEnd);
			_int = HighRoll;

			int LowRoll = Random.Range(lowBegin, lowEnd);
			_str = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_dex = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_wis = LowRoll - modRoll();

		}
		else if(charClass.ToLower() == "Cleric".ToLower())
		{
			//Cleric: Wis, Str, Con


			int HighRoll = Random.Range(highBegin, highEnd);
			_wis = HighRoll + modRoll();
			HighRoll = Random.Range(highBegin, highEnd);
			_str = HighRoll;
			HighRoll = Random.Range(highBegin, highEnd);
			_con = HighRoll;

			int LowRoll = Random.Range(lowBegin, lowEnd);
			_dex = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_int = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_cha = LowRoll - modRoll();
		}
		else
		{
			int HighRoll = Random.Range(highBegin, highEnd);
			_str = HighRoll + modRoll();
			HighRoll = Random.Range(highBegin, highEnd);
			_dex = HighRoll;
			HighRoll = Random.Range(highBegin, highEnd);
			_con = HighRoll;

			int LowRoll = Random.Range(lowBegin, lowEnd);
			_int = LowRoll;
			LowRoll = Random.Range(lowBegin, lowEnd);
			_wis = LowRoll - modRoll();
			LowRoll = Random.Range(lowBegin, lowEnd);
			_cha = LowRoll - modRoll();
		}
	
		_xp = 0;
		_nodeNo = 0;
		_maxHp = (_str + _dex/2 + _con);
		_maxMp = (_wis + _int/2 + _con/2);
		_hp = _maxHp;
		_mp = _maxMp;

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

	public WorldObject[] WorldObjects
	{
		get
		{
			return _worldObjects;
		}
		set
		{
			_worldObjects = value;
		}
	}

	public int HP
	{
		get
		{
			return _hp;
		}
		set
		{
			_hp = value;
		}
	}

	public int MP
	{
		get
		{
			return _mp;
		}
		set
		{
			_mp = value;
		}
	}

	public int maxHP
	{
		get
		{
			return _maxHp;
		}
	}

	public int maxMP
	{
		get
		{
			return _maxMp;
		}
	}

	public Enemy[] Enemies
	{
		get
		{
			return _enemies;
		}
		set
		{
			_enemies = value;
		}
	}
}
