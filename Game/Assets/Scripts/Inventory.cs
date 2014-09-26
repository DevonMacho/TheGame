using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 

{
	static List<ItemData.Item> items;
	public static void initItemList()
	{
		if(items == null)
		{
			items = new List<ItemData.Item>();
			testAddItems();
		}
	}
	public static void addItem(string name, string description,int location, int weight)
	{
		items.Add(new ItemData.Item(name,description,location,weight));
	}

	public static void testAddItems()
	{
		addItem("Mock","yeah",-1,2);
		addItem("Ing","yeah",-1,2);
		addItem("Bird","yeah",-1,2);
	}
	public static string listItems()
	{
		string total = "----- Inventory ----";
		string end = "\n";
		for (int i = total.Length; i > 0; i--)
		{
			end += "-";
		}
		foreach (ItemData.Item a in items)
		{
			total = total + "\n"+ a.getName();
		}
		return total + end;
	}

}
