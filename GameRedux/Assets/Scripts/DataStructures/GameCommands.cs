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
			new Command("inventory", "Opens your inventory.", null),
			new Command("look", "Allows you look at things / around you.", 
		    new Command[] 
		    { 
				new Command("at", "Allows you to look at an object. 'look at <object>'", new Command[]{new Command("<object>", "An Object", null)}),
				new Command("around", "Allows you to look around you.", null),
				new Command("<Direction>", "Allows you to look in the desired direction 'look <Direction Name>'.", null)
			}),
			
	//takes turn
			new Command("go", "Allows you to change your location.", new Command[]{new Command("<Direction>", "go <Direction Name>", null)}),
			new Command("pickup", "Allows you to pick up an item.", new Command[]{new Command("<Item>", "'pickup' <Item Name>", null)}),
			new Command("drop", "Allows you to drop an item.", new Command[]{new Command("<Item>", "'drop <Item Name>'", null)}),
			new Command("equip", "Equips an item.", new Command[]{new Command("<Item>", "'equip <Item Name>'", null)}),
			new Command("unequip", "Unequips an item.", new Command[]{new Command("<Item>", "'unequip <Item Name>'", null)}),
			new Command("open", "Opens an object.", new Command[]{new Command("<Object>", "'open <Object Name>'", null)}),
			new Command("close", "Closes an object.", new Command[]{new Command("<Object>", "'close <Object Name>'", null)}),
			new Command("use", "Interact with an item.", new Command[]{new Command("<Item>", "'use <Item Name>'", null)}),
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
		else if(cmd.CommandName == "inventory")
		{
			return inventory();
		}
		else if(cmd.CommandName == "look")
		{
			return look(cmd, tkn);
		}
		else if(cmd.CommandName == "go")
		{
			return go(tkn);
		}
		else if(cmd.CommandName == "pickup")
		{
			return pickup(tkn);
		}
		else if(cmd.CommandName == "drop")
		{
			return drop(tkn);
		}
		else if(cmd.CommandName == "equip")
		{
			return equip(tkn);
		}
		else if(cmd.CommandName == "unequip")
		{
			return unequip(tkn);
		}
		else if(cmd.CommandName == "open")
		{
			return open(tkn);
		}
		else if(cmd.CommandName == "close")
		{
			return close(tkn);
		}
		else if(cmd.CommandName == "use")
		{
			return use(tkn);
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

			if(a.CommandName == "<Item>" || a.CommandName == "<Object>")
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
		return (ret + "\n======== END =========");
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
			else if(a.CommandName.Length <= 6 && a.CommandName.Length >= 3)
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
		Location currentLoc = GameMaster.GM.World [GameMaster.GM.Data.Node];
		/* Known factors
		 * Tkn is at least 2 strings long
		 * the command IS look
		 */
		if(tkn [1].ToLower() == "at")
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
				//check inventory
				foreach (Item a in GameMaster.GM.Data.Items)
				{
					if(a.Location < 0 || a.Location == GameMaster.GM.Data.Node)
					{
						if(a.Name.ToLower() == tkn [2].ToLower())
						{
							return a.Description;
						}
					}

				}

				foreach (WorldObject a in GameMaster.GM.Data.WorldObjects)
				{
					if(a.Location == GameMaster.GM.Data.Node)
					{
						if(a.Name.ToLower() == tkn [2].ToLower())
						{
							return a.Description;
						}
					}
					
				}

				//check world

				//check to see if the item actually exists in the item list
				foreach (Item a in GameMaster.GM.Data.Items)
				{
					if(a.Name.ToLower() == tkn [2].ToLower())
					{
						return "That object is not here.";
					}
					
				}
				//check to see if the object actually exists in the object list
				foreach (WorldObject a in GameMaster.GM.Data.WorldObjects)
				{
					if(a.Name.ToLower() == tkn [2].ToLower())
					{
						return "That object is not here.";
					}
				}

				return "That object does not exist.";
			}
			else
			{
				return tma;
			}
		}
		else if(tkn [1].ToLower() == "around")
		{
			if(tkn.Length > 2)
			{
				return tma;
			}
			else
			{
				string ret = "\n\n==== Items At Location ====\n\n";
				int tick = 0;
				foreach(Item a in GameMaster.GM.Data.Items)
				{
					if(a.Location == GameMaster.GM.Data.Node)
					{
						tick++;
						ret +=  a.Name+ "\n";
					}
				}
				if(tick == 0)
				{
					ret +=  "None"+ "\n";
				}
				ret += "\n======== Directions =======\n\n";
				tick = 0;
				foreach(string a in GameMaster.GM.World[GameMaster.GM.Data.Node].AdjacentNodeDirection)
				{
						tick++;
						string info = GameMaster.GM.World[ GameMaster.GM.World[GameMaster.GM.Data.Node].AdjacentNodes[tick-1]].ShortInfo;
						if(a.Length <= 5)
						{
							ret +=  a + "\t\t--\t" + info + "\n";
						}
						else
						{
							ret +=  a + "\t--\t" + info + "\n";
						}

				}
				return currentLoc.Information + ret + "\n=========== END ===========";
			}
		}
		else
		{
			int sub = 0;
			foreach (string a in currentLoc.AdjacentNodeDirection)
			{
				sub++;
				if(tkn [1].ToLower() == a.ToLower())
				{
					return "Looking " + a.ToLower() + "...\nYou see: " + GameMaster.GM.World [currentLoc.AdjacentNodes [sub - 1]].ShortInfo;
				}
				
			}
			foreach (Location a in GameMaster.GM.World)
			{
				foreach (string b in a.AdjacentNodeDirection)
				{
					if(tkn [1].ToLower() == b.ToLower())
					{
						return "You do not see anything in that direction.";
					}
				}
			}
			return im;
		}	
	}

	static string inventory()
	{
		GameObject.FindObjectOfType<GameInput>().inv.openInv();
		return "Opening Inventory...";
	}

	static string go(string[] tkn)
	{
		Location currentLoc = GameMaster.GM.World [GameMaster.GM.Data.Node];
		//go <Direction>
		if(tkn.Length == 2)
		{
			int sub = 0;
			foreach (string a in currentLoc.AdjacentNodeDirection)
			{
				sub++;
				if(tkn [1].ToLower() == a.ToLower())
				{

					foreach (WorldObject b in GameMaster.GM.Data.WorldObjects)
					{
						if(b.CanOpen && !b.CanPassIfClosed)
						{
							if(b.Location == GameMaster.GM.Data.Node)
							{
								if(b.LockedNode == currentLoc.AdjacentNodes [sub - 1])
								{
									if(!b.IsOpen)
									{
										return "You must open the '" + b.Name + "' to go in that direction.";
									}
								}
							}
						}
					}
					GameInput input = GameObject.FindObjectOfType<GameInput>();
					foreach(Enemy b in GameMaster.GM.Data.Enemies)
					{
						if(b.Location == currentLoc.AdjacentNodes [sub - 1] && b.IsAlive)
						{
							string[] cine1 = 
							{
								"Going " + a.ToLower() + "...\n" + GameMaster.GM.World [currentLoc.AdjacentNodes [sub - 1]].Information,"You spot an enemy.", "prepare for battle"
							};
							input.StartCoroutine(input.fadeTexture(GameMaster.GM.backgrounds[currentLoc.AdjacentNodes [sub - 1]], GameMaster.GM.backgrounds[GameMaster.GM.Data.Node]));
							GameMaster.GM.Data.Node = currentLoc.AdjacentNodes [sub - 1];
							return input.startCinematic(cine1);
							//start cinematic instead
						}
					}


					input.StartCoroutine(input.fadeTexture(GameMaster.GM.backgrounds[currentLoc.AdjacentNodes [sub - 1]], GameMaster.GM.backgrounds[GameMaster.GM.Data.Node]));
					if(currentLoc.AdjacentNodes [sub - 1] == 24)
					{
						GameMaster.GM.SaveGame("The 'One Eyed Gopher Stroker' Inn");
					}
					GameMaster.GM.Data.Node = currentLoc.AdjacentNodes [sub - 1];
					return "Going " + a.ToLower() + "...\n" + GameMaster.GM.World [currentLoc.AdjacentNodes [sub - 1]].Information;
				}
				
			}
			foreach (Location a in GameMaster.GM.World)
			{
				foreach (string b in a.AdjacentNodeDirection)
				{
					if(tkn [1].ToLower() == b.ToLower())
					{
						return "You can not go in that direction.";
					}
				}
			}
			return im;

		}
		else
		{
			return tma;
		}
	}

	static string pickup(string[] tkn)
	{
		if(tkn.Length == 2)
		{
			int looper = 0;
			foreach (Item a in GameMaster.GM.Data.Items)
			{
				looper++;
				if(a.Location > 0 && GameMaster.GM.Data.Node == a.Location)
				{
					if(a.Name.ToLower() == tkn [1].ToLower())
					{
						if(a.ItemType == "<Quest>")
						{
							GameMaster.GM.Data.Items [looper - 1].Location = -11;
						}
						else
						{
							GameMaster.GM.Data.Items [looper - 1].Location = -1;

						}
						return "Picking up: " + a.Name + ".";
					}
				}
			}
			foreach (Item a in GameMaster.GM.Data.Items)
			{
				if(a.Name.ToLower() == tkn [1].ToLower())
				{
					return "That item is not here.";
				}

			}
			return "That item does not exist.";
		}
		else
		{
			return tma;
		}
	}

	static string drop(string[] tkn)
	{
		if(tkn.Length == 2)
		{
			int looper = 0;
			foreach (Item a in GameMaster.GM.Data.Items)
			{
				looper++;
				if(a.Location == -1)
				{
					if(a.Name.ToLower() == tkn [1].ToLower())
					{
						GameMaster.GM.Data.Items [looper - 1].Location = GameMaster.GM.Data.Node;
						return "Dropping: " + a.Name + ".";
					}
				}
			}
			foreach (Item a in GameMaster.GM.Data.Items)
			{
				if(a.Name.ToLower() == tkn [1].ToLower())
				{
					if(a.ItemType == "<Quest>")
					{
						return "You can not drop a quest item.";
					}
					else
					{
						return "That item is not in your inventory.";
					}
				}
				
			}
			return "That item does not exist.";
		}
		else
		{
			return tma;
		}
	}

	static string equip(string[] tkn)
	{
		if(tkn.Length == 2)
		{
			int looper = 0;
			foreach (Item a in GameMaster.GM.Data.Items)
			{
				looper++;
				if(a.Location == -1)
				{
					if(a.Name.ToLower() == tkn [1].ToLower())
					{
						if(a.CanEquip)
						{
							foreach (Item b in GameMaster.GM.Data.Items)
							{
								if(b.Location == processItemEquipLocation(a))
								{
									return b.Name + " is currently equipped, unequip it to equip another item to this slot.";
								}
							}
							GameMaster.GM.Data.Items [looper - 1].Location = processItemEquipLocation(a);
							return "Equipping: " + a.Name + ".";
						}
						else
						{
							return "You can not equip that item.";
						}
					}
				}
			}
			foreach (Item a in GameMaster.GM.Data.Items)
			{
				if(a.Name.ToLower() == tkn [1].ToLower())
				{
					return "That item is not in your inventory.";
				}
				
			}
			return "That item does not exist.";
		}
		else
		{
			return tma;
		}
	}

	static string unequip(string[] tkn)
	{
		if(tkn.Length == 2)
		{
			int looper = 0;
			foreach (Item a in GameMaster.GM.Data.Items)
			{
				looper++;
				if(a.Location < -1 && a.Location > -10)
				{
					if(a.Name.ToLower() == tkn [1].ToLower())
					{
						GameMaster.GM.Data.Items [looper - 1].Location = -1;
						return "Unequipping: " + a.Name + ".";
					}
				}
			}
			foreach (Item a in GameMaster.GM.Data.Items)
			{
				if(a.Name.ToLower() == tkn [1].ToLower())
				{
					return "You do not have that item equipped.";
				}
				
			}
			return "That item does not exist.";
		}
		else
		{
			return tma;
		}
	}

	static string open(String[] tkn)
	{
		if(tkn.Length == 2)
		{
			foreach (WorldObject a in GameMaster.GM.Data.WorldObjects)
			{
				if(a.Name.ToLower() == tkn [1].ToLower())
				{
					if(a.Location == GameMaster.GM.Data.Node)
					{
						if(a.CanOpen)
						{
							if(a.IsOpen)
							{
								return a.Name + " is already open.";
							}
							else
							{
								if(a.ItemRequiredToOpen)
								{
									//check to see if they have the item
									foreach (Item b in GameMaster.GM.Data.Items)
									{
										if(a.RequiredItem.ToLower() == b.Name.ToLower())
										{
											if(b.Location == -11)
											{
												a.IsOpen = true;
												return "Opening " + a.Name; 
											}
										}
									}
									return "You do not have the item required to open this.";
								}
								else
								{
									a.IsOpen = true;
									return "Opening " + a.Name;
								}
							}
						}
						else
						{
							return "That object can not be opened";
						}
					}
				}
			}
			foreach (WorldObject a in GameMaster.GM.Data.WorldObjects)
			{
				if(a.Name.ToLower() == tkn [1].ToLower())
				{
					return "That object is not here.";
				}
			}
			return "That object does not exist.";
		}
		else
		{
			return tma;
		}
	}

	static string close(String[] tkn)
	{
		if(tkn.Length == 2)
		{
			foreach (WorldObject a in GameMaster.GM.Data.WorldObjects)
			{
				if(a.Name.ToLower() == tkn [1].ToLower())
				{
					if(a.Location == GameMaster.GM.Data.Node)
					{
						if(a.CanOpen)
						{
							if(a.IsOpen)
							{
								a.IsOpen = false;
								return "Closing " + a.Name;

							}
							else
							{
								return a.Name + " is already closed.";
							}
						}
						else
						{
							return "That object can not be closed.";
						}
					}
				}
			}
			foreach (WorldObject a in GameMaster.GM.Data.WorldObjects)
			{
				if(a.Name.ToLower() == tkn [1].ToLower())
				{
					return "That object is not here.";
				}
			}
			return "That object does not exist.";
		}
		else
		{
			return tma;
		}
	}

	static string use(string[] tkn)
	{
		if(tkn.Length == 2)
		{
			int tick = 0;
			foreach(Item a in GameMaster.GM.Data.Items)
			{
				tick++;
				if(a.Location == -1)
				{
					if(a.Name.ToLower() == tkn[1].ToLower())
					{
						if(a.Consumable)
						{
							if(a.Uses > 1)
							{
								GameMaster.GM.Data.Items[tick-1].Uses -= 1;
								if(GameMaster.GM.Data.Items[tick-1].Uses == 1)
								{
									useItem(a);
									return a.Name + " was used and has " + GameMaster.GM.Data.Items[tick-1].Uses +" use left.";
								}
								useItem(a);
								return a.Name + " was used and has " + GameMaster.GM.Data.Items[tick-1].Uses +" uses left.";
							}
							else if (a.Uses == 1)
							{
								useItem(a);
								GameMaster.GM.Data.Items[tick-1].Uses -= 1;
								return a.Name + " was used and no longer has any uses.";
							}
						}
						else
						{
							return "You can not use this item.";
						}
					}	
				}
			}

			foreach(Item a in GameMaster.GM.Data.Items)
			{
				if(a.Name.ToLower() == tkn[1].ToLower())
				{
					return "That item is not in your inventory.";
				}
			}

			return "That item does not exist.";
		}
		else
		{
			return tma;
		}
	}

	static string quit()
	{
		GameObject.FindObjectOfType<GameInput>().StartQuit();
		return "Quitting...";
	}
	public static void useItem(Item toUse)
	{
		if (toUse.ModValues != null && toUse.ModValues.Length == 9)
		{
			if(toUse.ModValues[8] + GameMaster.GM.Data.HP > GameMaster.GM.Data.maxHP)
			{
				GameMaster.GM.Data.HP = GameMaster.GM.Data.maxHP;
			}
			else
			{
				GameMaster.GM.Data.HP += toUse.ModValues[8];
			}

		}
	}
	static Command[] getSubcommands(Command cmd)
	{
		return cmd.SubCommands;
	}

	public static int processItemEquipLocation(Item item)
	{

		if(item.ItemType == "<Head>")
		{
			return -2;
		}
		else if(item.ItemType == "<Chest>")
		{
			return -3;
		}
		else if(item.ItemType == "<Hands>")
		{
			return -4;
		}
		else if(item.ItemType == "<Waist>")
		{
			return -5;
		}
		else if(item.ItemType == "<Legs>")
		{
			return -6;
		}
		else if(item.ItemType == "<Feet>")
		{
			return -7;
		}
		else if(item.ItemType == "<Weapon>")
		{
			return -8;
		}
		else if(item.ItemType == "<Trinket>")
		{
			return -9;
		}
		else
		{
			return -10;
		}
	}

	public static Command[] Commands
	{
		get
		{
			return _commandsInUse;
		}
	}
}