using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

// get asc and desc sort on inventory

public class Inventory : MonoBehaviour
{

    /*
    public static void addItem(string name, string description, int location, int weight, int openState)
    {
        items.Add(new ItemData.Item(name, description, location, weight, openState));
    }

    public static void testAddItems()
    {
        addItem("rock", "A heavy blunt object that can be used to hurt Kraymo'r", 4, 5, -1);
        addItem("paper", "lighter and flatter than the rock", 2, 1, -1);
        addItem("scissors", "It would be able to hurt anyone if they weren't safety scissors", -1, 2, -1);
    }
*/

    public static string listInventory(string[] token)
    {
        if (token.Length == 1)
        {
            string total = "----- Inventory ----";
            string end = "\n";
            for (int i = total.Length; i > 0; i--)
            {
                end += "-";
            }
            int numItems = 0;
            foreach (ItemData.Item a in WorldData.gameData.items)
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

        else if (token.Length == 2)
        {
            if (token [1].ToLower().Equals("asc"))
            {

                var orderedInv = WorldData.gameData.items.OrderBy(x => x.getName());
                string total = "----- Inventory ----";
                string end = "\n";
                for (int i = total.Length; i > 0; i--)
                {
                    end += "-";
                }
                int numItems = 0;
                foreach ( var a in orderedInv)
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
            else if (token [1].ToLower().Equals("dsc"))
            {
                var orderedInv = WorldData.gameData.items.OrderByDescending(x => x.getName());
                string total = "----- Inventory ----";
                string end = "\n";
                for (int i = total.Length; i > 0; i--)
                {
                    end += "-";
                }
                int numItems = 0;
                foreach ( var a in orderedInv)
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
            else
            {
                return "Invalid modifier";
            }
        }
        else if (token.Length > 2)
        {
            return "too many args";
        }
        else
        {
            return "unknown error";
        }
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
        foreach (ItemData.Item a in WorldData.gameData.items)
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
            foreach (ItemData.Item a in WorldData.gameData.items)
            {
                if (a.getName().Equals(command [1]) && a.getLocation() == WorldData.currentLoc)
                {
                    WorldData.gameData.items.Remove(a);
                    a.setLocation(-1);
                    WorldData.gameData.items.Add(a);
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
            foreach (ItemData.Item a in WorldData.gameData.items)
            {
                if (a.getName().Equals(command [1]) && a.getLocation() == -1)
                {
                    WorldData.gameData.items.Remove(a);
                    a.setLocation(WorldData.currentLoc);
                    WorldData.gameData.items.Add(a);
                    return command [1] + " dropped";
                } 
            }
            return "Item not found in inventory";

        }
        return "too many args";
    }

}
