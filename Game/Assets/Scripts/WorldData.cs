using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldData : MonoBehaviour {

	private static List<LocationData.Location> locationList;
	public static int currentLoc = 0;
	public static void initializeLocations ()
	{
		if(locationList == null)
		{
			locationList = new List<LocationData.Location>();
			locationList.Add(new LocationData.Location("LocA","Location A" , 0, new int[2]{1,2}, new string[2]{"southwest","southeast"}));
			locationList.Add(new LocationData.Location("LocB","Location B" , 1, new int[4]{0,2,3,4}, new string[4]{"northeast","east","south","southeast"}));
			locationList.Add(new LocationData.Location("LocC","Location C" , 2, new int[4]{0,1,3,4}, new string[4]{"northwest","west","southwest","south"}));
			locationList.Add(new LocationData.Location("LocD","Location D" , 3, new int[3]{1,2,4}, new string[3]{"north","northeast","east"}));
			locationList.Add(new LocationData.Location("LocE","Location E" , 4, new int[3]{1,2,3}, new string[3]{"northwest","north","west"}));
		}
	}

	public static string Go(string goTo)
	{

		foreach(LocationData.Location a in locationList)
		{
			if(a.getNodeNumber() == currentLoc)
			{
				for(int i = 0; i < a.getAdjacentNodes().Length;i++)
				{
					if(a.getAdjacentDirections()[i].Equals(goTo))
					{
						currentLoc = a.getAdjacentNodes()[i];
						return "Going: " + a.getAdjacentDirections()[i];
					}
				}
			}

		}
		return "Location does not exist";
	}


	void Start () {
	
	}


}
