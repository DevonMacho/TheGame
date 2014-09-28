using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldData : MonoBehaviour {

	private static Dictionary<string, int> locationList;
	public static int currentLoc = 0;
	public static void initializeLocations ()
	{
		locationList = new Dictionary<string,int >();
		locationList.Add("Location A", 0);
		locationList.Add("Location B", 1);
	}
	//public static string getCurrent()
	//{
	//	return locationList[currentLoc];
	//}
	public static string Go(string newLoc)
	{
		if (newLoc.Equals ("location a")) {
			currentLoc = 0;
			return "You are now at "+ "Location A";

		} else if (newLoc.Equals ("location b")) {
			currentLoc = 1;
			return "You are now at "+ "Location B";
		} else {
			return "there is no such location";
		}

	}

	void Start () {
	
	}


}
