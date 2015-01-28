using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item
{

	string _name;
	string _description;
	int _value;
	bool _canEquip;
	string _itemType;
	int _location;
	bool _canModifyStats;
	bool _consumable;
	int  _uses;
	int[] _modVals;

	//generic constructor
	public Item(string name, string description, int value, bool canEquip, string itemType, int location, bool canModifyStats, bool consumable, int uses, int[] modifiers)
	{
		_name = name;
		_description = description;
		_value = value;
		_canEquip = canEquip;
		_itemType = itemType;
		_location = location;
		_canModifyStats = canModifyStats;
		_consumable = consumable;
		_uses = uses;
		_modVals = modifiers;
	}
	// quest Item / required item
	public Item(string name, string description, string itemType, int location)
	{
		_name = name;
		_description = description;
		_value = 0;
		_canEquip = false;
		_itemType = itemType;
		_location = location;
		_canModifyStats = false;
		_consumable = false;
		_uses = -1;
		_modVals = null;
	}
	//Wearable item
	public Item(string name, string description, int value, string itemType, int location, bool canModifyStats, int[] modifiers)
	{
		_name = name;
		_description = description;
		_value = value;
		_canEquip = true;
		_itemType = itemType;
		_location = location;
		_canModifyStats = canModifyStats;
		_consumable = false;
		_uses = -1;
		_modVals = modifiers;
	}
	//consumable item
	public Item(string name, string description, int value, string itemType, int location, bool canModifyStats, bool consumable, int uses, int[] modifiers)
	{
		_name = name;
		_description = description;
		_value = value;
		_canEquip = false;
		_itemType = itemType;
		_location = location;
		_canModifyStats = canModifyStats;
		_consumable = consumable;
		_uses = uses;
		_modVals = modifiers;
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

	public int Value
	{
		get
		{
			return _value;
		}
	}

	public bool CanEquip
	{
		get
		{
			return _canEquip;
		}
	}

	public string ItemType
	{
		get
		{
			return _itemType;
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

	public bool CanModifyStats
	{
		get
		{
			return _canModifyStats;
		}
	}

	public bool Consumable
	{
		get
		{
			return _consumable;
		}
	}

	public int Uses
	{
		get
		{
			return _uses;
		}
		set
		{
			_uses = value;
		}
	}

	public int[] ModValues
	{
		get
		{
			return _modVals;
		}
	}
}
