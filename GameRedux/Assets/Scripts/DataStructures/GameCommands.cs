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