using UnityEngine;
using System;

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
	static Command[] _commandsInUse =
		{
	//basics
			new Command("help", "Lists availiable commands.", null),
			new Command("clear", "Clears the terminal.", null),
			new Command("look", "Allows you look at things / around you.", 
		    new Command[] 
		    { 
				new Command("at", "Allows you to look at an object. 'look at <object>'", new Command[]{new Command("<object>", "An Object", null)}),
				new Command("around", "Allows you to look around you.", null)
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
		else if(cmd.CommandName == "go")
		{
			return "go";
		}
		else if(cmd.CommandName == "look")
		{
			return "look";
		}
		else
		{
			return "Guru Meditation 0x0002";
		}


	}

	public static string DisplaySubCommands(Command cmd)
	{
		string ret = "\n==== Sub Commands ====\n";
		string fnl = "";
		if(cmd.SubCommands.Length == 1)
		{
			fnl = "\n";
		}
		foreach (Command a in cmd.SubCommands)
		{
			string tab;

			if(a.CommandName == "<Item>" || a.CommandName == "<Object>" || a.CommandName == "<Location>"  )
			{
				ret +="\n";
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
		return (ret + fnl +"======== END =========");
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

	public static Command[] Commands
	{
		get
		{
			return _commandsInUse;
		}
	}
}