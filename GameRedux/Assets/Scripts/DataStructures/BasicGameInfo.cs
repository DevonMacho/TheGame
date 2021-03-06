﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasicGameInfo
{

	[SerializeField]

	string name;
	string charClass;
	string location;
	int gold;
	int level;

	public BasicGameInfo(string name, string charClass, string location, int gold, int level)
	{
		this.name = name;
		this.charClass = charClass;
		this.location = location;
		this.gold = gold;
		this.level = level;
	}

	public string Name
	{
		get
		{
			return name;
		}
	}

	public string CharClass
	{
		get
		{
			return charClass;
		}
	}

	public string Location
	{
		get
		{
			return location;
		}
		set
		{
			location = value;
		}
	}

	public int Gold
	{
		get
		{
			return gold;
		}
		set
		{
			gold = value;
		}
	}

	public int Level
	{
		get
		{
			return level;
		}
		set
		{
			level = value;
		}
	}
}
