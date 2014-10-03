using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parser : MonoBehaviour
{
    
    private static Dictionary<string, int> commandList;

    public static void initializeCommands()
    {
        commandList = new Dictionary<string,int >();
        commandList.Add("help", 0);
        commandList.Add("clear", 1);
        commandList.Add("look", 2);
        commandList.Add("go", 3);
        commandList.Add("pickup", 4);
        commandList.Add("drop", 5);
        commandList.Add("inventory", 6);
        commandList.Add("quit", 7);
        //Debug.Log("commands initialized");
    }

    private static string[] tokenize(string tkn)
    {
        string[] tokens;
        tkn = tkn.ToLower();
        tokens = tkn.Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length <= 0)
        {
            return new string[0];
        }
        return tokens;
    }

    public static string Parse(string input)
    {
        if (commandList == null)
        {
            initializeCommands();
        }
        string[] token = tokenize(input);
        if (token.Length <= 0 || !commandList.ContainsKey(token [0].ToLower()))
        {
            return "Please enter a valid command";
        }

        int command = commandList [token [0].ToLower()];
        if (command == 0)
        {
            return help();
        }
        else if (command == 1)
        {
            return clear();
        }
        else if (command == 2)
        {
            return look(token);
        }
        else if (command == 3)
        {
            return go(token);
        }
        else if (command == 4)
        {
            return pickup(token);
        }
        else if (command == 5)
        {
            return drop(token);
            
        }
        else if (command == 6)
        {
            return listInventory();
        }
        else if (command == 7)
        {
            return quit();
        }
        else
        {
            return "you have entered in a valid command";
        }
    }

    private static string help()
    {
        string lst = "----- Commands ----\n";
        foreach (string i in commandList.Keys)
        {
            //testing re-merge
            lst += i + "\n";
        }
        lst += "------------------";
        return lst;
    }

    private static string listInventory()
    {
      
        return Inventory.listInventory();
    }
    private static string pickup(string[] token)
    {
        return Inventory.pickup(token);
    }
    private static string drop(string[] token)
    {
        return Inventory.drop(token);
    }
    private static string quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
        return "Quitting...";
    }
    private static string look(string[] token)
    {
        return WorldData.Look(token);
    }
    private static string clear()
    {
        return "<===Clearing===>";
    }

    private static string go(string[] token)
    {
            return WorldData.Go(token);
    }
}
