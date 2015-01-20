using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameCommands
{
	Command[] _commands;

	GameCommands(Command[] commands)
	{
		_commands = commands;
		/* 
		 *Here is the concept behind mapping the commands
		 *Element 0 - I am a root command and I have 3 subcommands.
		 *Element 1 - I am a subcommand of element 0 and I have no subcommands
		 *Element 2 - I am a subcommand of element 0 and I have two subcommands
		 *Element 3 - I am a subcommand of element 2 and I have no subcommands
		 *Element 4 - I am a subcommand of element 2 and I have no subcommands
		 *Element 5 - I am a subcommand of element 0 and I have no subcommands
		 *Element 6 - I am a root command and I have one subcommand
		 *Element 7 - I am a subcommand of element 6 and I have no subcommands
		 */
	}

	public Command[] Commands
	{
		get
		{
			return _commands;
		}
	}
}
[System.Serializable]
public class Command
{
	string _command;
	string _commandDescription;
	int _subcommands;

	public Command(string command, string description, int subcommands)
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

	public int SubCommands
	{
		get
		{
			return _subcommands;
		}
	}
}