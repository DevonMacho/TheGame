using UnityEngine;
using System;
[System.Serializable]
public class Command
{
	string _command;
	string _commandDescription;
	bool _hasSubCommands;
	Command[] _subcommands;

	public Command(string command, string description ,Command[] subcommands)
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
			new Command("help","Lists availiable commands.",null),
			new Command("clear","Clears the terminal.",null),
			new Command("look",".", null),
			new Command("inventory",".",null),
			//takes turn
			new Command("go",".",null),
			new Command("pickup",".",null),
			new Command("drop",".",null),
			new Command("equip",".",null),
			new Command("unequip",".",null),
			new Command("open",".",null),
			new Command("close",".",null),
			//final commands
			new Command("quit","Exits the current game.",null)
			
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