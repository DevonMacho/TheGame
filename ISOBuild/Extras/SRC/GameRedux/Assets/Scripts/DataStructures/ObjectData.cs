using UnityEngine;
using System.Collections;
[System.Serializable]
public class WorldObject
{
	string _name;
	string _description;
	bool _itemRequiredToOpen;
	string _requiredItem;
	bool _canOpen;
	bool _isOpen;
	bool _canPassIfClosed;
	int _lockedNode;
	int _location;

	public WorldObject(string name, string description, bool itemRequiredToOpen, string requiredItem, bool canOpen, bool isOpen, bool canPassIfClosed, int lockedNode, int location)
	{
		_name = name;
		_description = description;
		_itemRequiredToOpen = itemRequiredToOpen;
		_requiredItem = requiredItem;
		_canOpen = canOpen;
		_isOpen = isOpen;
		_canPassIfClosed = canPassIfClosed;
		_lockedNode = lockedNode;
		_location = location;
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

	public bool ItemRequiredToOpen
	{
		get
		{
			return _itemRequiredToOpen;
		}
	}

	public string RequiredItem
	{
		get
		{
			return _requiredItem;
		}
	}

	public bool CanOpen
	{
		get
		{
			return _canOpen;
		}
	}

	public bool IsOpen
	{
		get
		{
			return _isOpen;
		}
		set
		{
			_isOpen = value;
		}
	}

	public bool CanPassIfClosed
	{
		get
		{
			return _canPassIfClosed;
		}
	}

	public int LockedNode
	{
		get
		{
			return _lockedNode;
		}
	}

	public int Location
	{
		get
		{
			return _location;
		}
	}
}
