using UnityEngine;
using System;
[System.Serializable]
public class Command
{
	string _command;
	string _commandDescription;
	bool _subcommands;

	public Command(string command, string description, bool subcommands)
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

	public bool SubCommands
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
			new Command("help","Lists availiable commands.",false),
			new Command("clear","Clears the terminal.",true),
			//does not take a turn
			new Command("look",".",true),
			new Command("inventory",".",true),
			//takes turn
			new Command("go",".",true),
			new Command("pickup",".",true),
			new Command("drop",".",true),
			new Command("equip",".",true),
			new Command("unequip",".",true),
			new Command("open",".",true),
			new Command("close",".",true),
			//final commands
			new Command("quit","Exits the current game.",false)
			
		};
	public static string ProcessCommands(Command cmd,string[] tkn, bool displaySubcommands)
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
	static string help()
	{
		string ret = "==== Command Descriptions ====\n";
		foreach(Command a in _commandsInUse)
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
			ret += a.CommandName + tab + a.CommandDescription +"\n";
		}
		return (ret + "============= END =============\n");
	}
	public static Command[] Commands
	{
		get
		{
			return _commandsInUse;
		}
	}
}