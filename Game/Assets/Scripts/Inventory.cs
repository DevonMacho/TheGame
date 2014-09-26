using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {	
	public static Dictionary<ItemData.Item, string> playerInventory;
	void initPlayerInv () 
	{
		if (playerInventory == null)
		{
			playerInventory = new Dictionary<ItemData.Item,string >();
		}
		return;
	}
	void Update () 
	{
	
	}
}
