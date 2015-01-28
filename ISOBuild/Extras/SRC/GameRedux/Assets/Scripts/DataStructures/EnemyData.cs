using UnityEngine;
using System.Collections;
[System.Serializable]
public class Enemy
{
	string _name;
	string _description;
	int _location;
	bool _alive;
	bool _canRoam;
	Item[] _loot;
	int _gold;
	int _maxAttack;
	int _health;
	//Sprite _display;
	public Enemy(string name, string description, int location, bool alive, bool canRoam, Item[] loot, int gold,int maxAttack)
	{
		_name = name;
		_description = description;
		_location = location;
		_alive = alive;
		_canRoam = canRoam;
		_loot = loot;
		_gold = gold;
		_maxAttack = maxAttack;
		_health = maxAttack * 5;
		//_display = display;
	}

	public string Name
	{
		get
		{
			return _name;
		}
	}

	public string Description
	{
		get
		{
			return _description;
		}
	}

	public int Location
	{
		get
		{
			return _location;
		}
		set
		{
			_location = value;
		}
	}

	public bool IsAlive
	{
		get
		{
			return _alive;
		}
		set
		{
			_alive = value;
		}
	}

	public bool CanRoam
	{
		get
		{
			return _canRoam;
		}
	}

	public Item[] Loot
	{
		get
		{
			return _loot;
		}
	}

	public int Gold
	{
		get
		{
			return _gold;
		}
	}

	public int MaxAttack
	{
		get
		{
			return _maxAttack;
		}
	}

	public int HP
	{
		get
		{
			return _health;
		}
		set
		{
			_health = value;
		}
	}
	/*
	public Sprite Display
	{
		get
		{
			return _display;
		}
	}
	*/
}
