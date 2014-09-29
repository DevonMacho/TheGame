using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 

{
	public static List<ItemData.Item> items;
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
		addItem("rock","A heavy blunt object that can be used to hurt Kramer",4,5);
		addItem("paper","lighter and flatter than the rock",2,1);
		addItem("scissors","It would be able to hurt anyone if they weren't safety scissors",-1,2);
	}
	public static string listItems()
	{
		string total = "----- Inventory ----";
		string end = "\n";
		for (int i = total.Length; i > 0; i--)
		{
			end += "-";
		}
        int numItems = 0;
		foreach (ItemData.Item a in items)
		{
            if(a.getLocation() == -1)
            {
                total = total + "\n"+ a.getName();
                numItems++;
            }
		}
        if (numItems < 1)
        {
            total = total + "\n<Empty>";
        }
		return total + end;
	}
    public static string itemsAtLocation(int location)
    {
        string total = "----- Items at location ----";
        string end = "\n";
        for (int i = total.Length; i > 0; i--)
        {
            end += "-";
        }
        int numItems = 0;
        foreach (ItemData.Item a in items)
        {
            if(a.getLocation() == location)
            {
                total = total + "\n"+ a.getName();
                numItems++;
            }
        }
        if (numItems < 1)
        {
            total = total + "\n<None>";
        }
        return total + end;
    }

}
