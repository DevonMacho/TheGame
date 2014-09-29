using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static List<ItemData.Item> items;

    public static void initItemList()
    {
        if (items == null)
        {
            items = new List<ItemData.Item>();
            testAddItems();
        }
    }

    public static void addItem(string name, string description, int location, int weight)
    {
        items.Add(new ItemData.Item(name, description, location, weight));
    }

    public static void testAddItems()
    {
        addItem("rock", "A heavy blunt object that can be used to hurt Kramer", 4, 5);
        addItem("paper", "lighter and flatter than the rock", 2, 1);
        addItem("scissors", "It would be able to hurt anyone if they weren't safety scissors", -1, 2);
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
            if (a.getLocation() == -1)
            {
                total = total + "\n" + a.getName();
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
            if (a.getLocation() == location)
            {
                total = total + "\n" + a.getName();
                numItems++;
            }
        }
        if (numItems < 1)
        {
            total = total + "\n<None>";
        }
        return total + end;
    }

    public static string pickup(string[] command)
    {
        
        if (command.Length <= 1)
        {
            return "pickup what?";
        }
        else if (command.Length == 2)
        {
            foreach (ItemData.Item a in items)
            {
                if (a.getName().Equals(command [1]) && a.getLocation() == WorldData.currentLoc)
                {
                    items.Remove(a);
                    a.setLocation(-1);
                    items.Add(a);
                    return command [1] + " picked up";
                } 
            }
            return "Item not found at current location";
            
        }
        return "too many args";
    }

    public static string drop(string[] command)
    {
        if (command.Length <= 1)
        {
            return "drop what?";
        }
        else if (command.Length == 2)
        {
            foreach (ItemData.Item a in items)
            {
                if (a.getName().Equals(command [1]) && a.getLocation() == -1)
                {
                    items.Remove(a);
                    a.setLocation(WorldData.currentLoc);
                    items.Add(a);
                    return command [1] + " dropped";
                } 
            }
            return "Item not found in inventory";

        }
        return "too many args";
    }

}
