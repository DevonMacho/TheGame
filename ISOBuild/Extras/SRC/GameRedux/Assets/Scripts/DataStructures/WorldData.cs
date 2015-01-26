using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
[System.Serializable]
public class WorldData
{
	Location[] _locations;

	public WorldData(int node, Location[] locs)
	{
		_locations = locs;
	}
	public Location[] Locations
	{
		get
		{
			return _locations;
		}
	}
}
*/
public class Location//: IEnumerable
{
	int _node;
	int[] _adjacentNodes;
	//bool _visited;
	string _locationName;
	string _locationInformation;
	string _shortInfo;
	string[] _adjacentDir;

	public Location(int node, string name, string info, int[] adjacentNodes, string[] adjacentDirection, string shortInfo)
	{
		_node = node;
		_locationName = name;
		_locationInformation = info;
		_adjacentNodes = adjacentNodes;
		_adjacentDir = adjacentDirection;
		_shortInfo = shortInfo;

		//add in a description of the locations in the future
	}
	public int Node
	{
		get
		{
			return _node;
		}
	}

	public string Name
	{
		get
		{
			return _locationName;
		}
	}

	public string Information
	{
		get
		{
			return _locationInformation;
		}
	}

	public int[] AdjacentNodes
	{
		get
		{
			return _adjacentNodes;
		}
	}

	public string[] AdjacentNodeDirection
	{
		get
		{
			return _adjacentDir;
		}
	}

	public string ShortInfo
	{
		get
		{
			return _shortInfo;
		}
	}
}