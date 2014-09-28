using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parser : MonoBehaviour {
	
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
		commandList.Add("quit",7);
		//Debug.Log("commands initialized");
	}
	private static string[] tokenize(string tkn)
	{
		string[] tokens;
		tokens = tkn.Split(default(string[]),System.StringSplitOptions.RemoveEmptyEntries);
		if (tokens.Length <= 0)
		{
			return new string[0];
		}
		return tokens;
	}

	public static string Parse (string input) 
	{
		if (commandList == null)
		{
			initializeCommands();
			WorldData.initializeLocations();
		}
		string[] token = tokenize(input);
		if (token.Length <= 0 || !commandList.ContainsKey(token[0].ToLower()))
		{
			return "Please enter a valid command";
		}
		int command = commandList[token[0].ToLower()];
      if (command == 0)
        {
          return help();
      } else if (command == 1)
        {
          return clear();
      } else if (command == 2)
        {
          return locationData();
        } 
      else if (command == 3)
        {
          return go(token[1].ToLower() + " " + token[2].ToLower());
        } 
      else if (command == 6)
        {
          return listItems();
        }
      else if(command == 7)
        {
          return quit();
        }
	private static string help()
	{
		string lst = "----- Commands ----\n";
		foreach (string i in commandList.Keys)
		{
			//testing re-merge
			lst += i + "\n" ;
		}
		lst += "------------------";
		return lst;
	}
  public static string locationData()
    {
      return "We need a location";
    }

  private static string listItems()
    {
      Inventory.initItemList();
      return Inventory.listItems();
    }
	private static string quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
		Application.Quit();
		return "Quitting...";
	}
	private static string clear()
	{
		return "<===Clearing===>";
	}
	private static string go(string going)
	{
		if (going == null) {
			return "Go Where?";
			}else{
		string location = WorldData.Go (going);
		return location;
		}
	}
}
