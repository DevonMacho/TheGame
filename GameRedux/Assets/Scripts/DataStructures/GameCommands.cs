using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Command
{
	string _command;
	string _commandDescription;
	bool _hasSubCommands;
	Command[] _subcommands;

	public Command(string command, string description, Command[] subcommands)
	{
		_command = command;
		_commandDescription = description;
		_subcommands = subcommands;
	}

	public string CommandName
	{
		get
		{
			return _command;
		}
	}

	public string CommandDescription
	{
		get
		{
			return _commandDescription;
		}
	}

	public Command[] SubCommands
	{
		get
		{
			return _subcommands;
		}
	}
}

public class GameCommands :MonoBehaviour
{
	static string im = "Invalid Modifier.";
	static string tma = "Too many Arguments.";
	static Command[] _commandsInUse =
		{
	//basics
			new Command("help", "Lists availiable commands.", null),
			new Command("clear", "Clears the terminal.", null),
			new Command("look", "Allows you look at things / around you.", 
		    new Command[] 
		    { 
				new Command("at", "Allows you to look at an object. 'look at <object>'", new Command[]{new Command("<object>", "An Object", null)}),
				new Command("around", "Allows you to look around you.", null),
				new Command("<Location>", "Allows you to look towards a location 'look <Location Name>'.", null)
			}),
			new Command("inventory", "Opens your inventory.", null),
	//takes turn
			new Command("go", 		"Allows you to change your location.", 	new Command[]{new Command("<Location>", "go <Location Name>", 	null)}),
			new Command("pickup", 	"Allows you to pick up an item.", 		new Command[]{new Command("<Item>", 	"'pickup' <Item Name>", null)}),
			new Command("drop", 	"Allows you to drop an item.",			new Command[]{new Command("<Item>", 	"'drop <Item Name>'",	null)}),
			new Command("equip", 	"Equips an item.",						new Command[]{new Command("<Item>", 	"'equip <Item Name>'", 	null)}),
			new Command("unequip", 	"Unequips an item.",					new Command[]{new Command("<Item>", 	"'unequip <Item Name>'",null)}),
			new Command("open", 	"Opens an object.",						new Command[]{new Command("<Object>", 	"'open <Object Name>'", null)}),
			new Command("close", 	"Closes an object.",					new Command[]{new Command("<Object>", 	"'close <Object Name>'",null)}),
	//final commands
			new Command("quit", "Exits the current game.", null)
			
		};

	public static string ProcessCommands(Command cmd, string[] tkn)
	{


		if(cmd.CommandName == "help")
		{
			return help();
		}
		else if(cmd.CommandName == "clear")
		{
			return clear();
		}
		else if(cmd.CommandName == "look")
		{
			return look(cmd, tkn);
		}












		//last
		else if(cmd.CommandName == "quit")
		{

			return quit();
		}
		else
		{
			return "Guru Meditation 0x0002";
		}


	}

	public static string DisplaySubCommands(Command cmd)
	{
		string ret = "\n==== Sub Commands ====\n\n";
		foreach (Command a in cmd.SubCommands)
		{
			string tab;

			if(a.CommandName == "<Item>" || a.CommandName == "<Object>" )
			{

				tab = ":\t";
			}
			else if(a.CommandName.Length > 6)
			{
				tab = ":\t\t";
			}
			else if(a.CommandName.Length <= 6 && a.CommandName.Length >= 4)
			{
				tab = ":\t\t\t";
			}
			else
			{
				tab = ":\t\t\t\t";
			}
			ret += a.CommandName + tab + a.CommandDescription + "\n";
		}
		return (ret +"\n======== END =========");
	}

	static string help()
	{
		string ret = "\n==== Available Commands ====\n\n";
		foreach (Command a in _commandsInUse)
		{
			string tab;
			if(a.CommandName.Length > 6)
			{
				tab = ":\t\t";
			}
			else if(a.CommandName.Length <= 6 && a.CommandName.Length >= 4)
			{
				tab = ":\t\t\t";
			}
			else
			{
				tab = ":\t\t\t\t";
			}
			ret += a.CommandName + tab + a.CommandDescription + "\n";
		}
		return (ret + "\n============ END ============");
	}
	static string clear()
	{
		GameObject.FindObjectOfType<GameInput>().StartCoroutine("clearDelay");
		return "";

	}

	static string look(Command cmd, string[] tkn)
	{
		Location currentLoc = GameMaster.GM.World[GameMaster.GM.Data.Node];
		/* Known factors
		 * Tkn is at least 2 strings long
		 * the command IS look
		 */
		if(tkn[1].ToLower() == "at")
		{
			if(tkn.Length == 2)
			{
				foreach (Command a in getSubcommands(cmd))
				{
					if(a.CommandName == "at")
					{
						return a.CommandDescription;
					}
				}
				return "Guru Meditation 0x0003";
			}
			else if(tkn.Length == 3)
			{
				//check inventory / world
				return "Looking at " + tkn[2];
				//check to see if the object actually exists in the object list
				//return "That object does not exist";
			}
			else
			{
				return tma;
			}

		}
		else if(tkn[1].ToLower() == "around")
		{
			if(tkn.Length > 2)
			{
				return tma;
			}
			else
			{
				return currentLoc.Information;
			}
		}
		else
		{
				foreach(string a in currentLoc.AdjacentNodeDirection)
				{
					if(tkn[1].ToLower() == a.ToLower())
					{
						return "looking " + a;
					}
				}
				foreach(Location a in GameMaster.GM.World)
				{
					foreach(string b in a.AdjacentNodeDirection)
					{
						if(tkn[1].ToLower() == b.ToLower())
						{
							return "You can not go that way.";
						}
					}
				}
			return im;
		}

			
	}
	static string quit()
	{
		GameObject.FindObjectOfType<GameInput>().StartQuit();
		return "Quitting...";
	}


	static Command[] getSubcommands(Command cmd)
	{
		return cmd.SubCommands;
	}

	public static Command[] Commands
	{
		get
		{
			return _commandsInUse;
		}
	}
}