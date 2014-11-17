using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameParser : MonoBehaviour
{

    private static Dictionary<string, int> commandList1;
    private static string[] genHelp;
    private static string[] genHelpMod;

    public static void initializeCommands()
    {

        commandList1 = new Dictionary<string,int >();
        genHelp = new string[]
        {
            "Displays a list of commands / help with a particular command",
            "Clears the command line output",
            "Allows you to visualize the world via command line",
            "Allows you to move in a direction",
            "Enables you to pick up an item",
            "Enables you to drop an item",
            "Lists your inventory",
            "Lets you quit the game",
            "Allows you to open objects",
            "Allows you to close objects",
            "Allows you to equip an item",
            "Allows you to unequip an item",
            "Allows you to use an object",
            "Allows you to save your progress",
            "Allows you to load your progress"
        };
        genHelpMod = new string[]
        {
            "Help <Command>",
            "None",
            "Around: Displays location information\nAt <Object>: Displays object information",
            "Go <Direction>: Goes in a particular direction. To get a list of locations use the look around command",
            "Pickup <Item>",
            "Drop <Item>",
            "ASC: List items in ascending order\nDEC: List items in decending order",
            "None",
            "Open <Object>",
            "Close <Object>",
            "Equip <Item>",
            "Unequip <Item>",
            "Use <Object>",
            "None",
            "None"
        };

        //order of appearance in the help command

        #region Basic Commands
        //"free" moves
        commandList1.Add("help", 0);
        commandList1.Add("clear", 1);
        commandList1.Add("stats", 15);
        commandList1.Add("minimap", 16);
        commandList1.Add("look", 2);
        commandList1.Add("inventory", 6);

        //uses a turn
        commandList1.Add("go", 3);
        commandList1.Add("pickup", 4);
        commandList1.Add("drop", 5);
        commandList1.Add("open", 8);
        commandList1.Add("close", 9);
        commandList1.Add("use", 12);

        //not WorldData.gameData.playerTurn(); yet
        commandList1.Add("attack", 18);
        commandList1.Add("defend", 19);
        //end not

        //cannot do in battle
        commandList1.Add("equip", 10);
        commandList1.Add("unequip", 11);
        commandList1.Add("save", 13);

        //instant leave game when in battle
        commandList1.Add("load", 14);
        commandList1.Add("mainmenu", 17);
        commandList1.Add("quit", 7);
      
        #endregion

    }

    public static string Parse(string input)
    {
        if (commandList1 == null)
        {
            initializeCommands();
        }
        string[] token = GenericCommands.tokenize(input);
        if (token.Length <= 0 || !commandList1.ContainsKey(token [0].ToLower()))
        {
            return "Please enter a valid command";
        }
        int command = commandList1 [token [0].ToLower()];
        if (command == 0)
        {
            return help(token);
        }
        else if (command == 1)
        {
            return GenericCommands.clear(token);
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
            return listInventory(token);
        }
        else if (command == 7)
        {
            return GenericCommands.startQuit(token);
        }
        else if (command == 8)
        {
            return Inventory.open(token);
        }
        else if (command == 9)
        {

            return Inventory.close(token);
            
        }
        else if (command == 10)
        {
            if (!WorldData.inBattle())
            {
                return Inventory.equip(token);
            }
            else
            {
                return "you can not equip an item in combat";
            }
        }
        else if (command == 11)
        {
            if (!WorldData.inBattle())
            {
                return Inventory.unequip(token);
            }
            else
            {
                return "you can not unequip an item in combat";
            }
        }
        else if (command == 12)
        {

            return Inventory.use(token);
        }
        else if (command == 13)
        {
            if (!WorldData.inBattle())
            {
                return GameData.startSave(token);
            }
            else
            {
                return "you can not save your game combat!";
            }
        }
        else if (command == 14)
        {
                return GameData.startLoad(token);
        }
        else if (command == 15)
        {
            return GUI_Terminal.toggleStats(token);
        }
        else if (command == 16)
        {
            return GUI_Terminal.toggleMiniMap(token);
        }
        else if (command == 17)
        {
            return GenericCommands.startMainMenu(token);
        }
        else if (command == 18)
        {
            return "attack was eneterd";
        }
        else if (command == 19)
        {
            return "defend was entered";
        }
        else
        {
            return "you have entered in a valid command";
        }
    }

    private static string help(string[] token)
    {
        if (token.Length == 1)
        {
            string lst = "----- Commands ----\n";
            foreach (string i in commandList1.Keys)
            {
                lst += i + "\n";
            }
            lst += "------------------";
            return lst;
        }
        else if (token.Length == 2)
        {
            #region Detailed Help modifiers
            if (!commandList1.ContainsKey(token [1].ToLower()))
            {
                return "Invalid modifier";
            }
            else if (commandList1.ContainsKey(token [1].ToLower()))
            {
                int command = commandList1 [token [1].ToLower()];

                return genHelp [command] + "\n------Modifiers------\n" + genHelpMod [command] + "\n ---------------------";
            }
            #endregion
        }
        else if (token.Length > 2)
        {
            return "too many args";  
        }
        return "Guru Meditation x0000002";

    }

    private static string listInventory(string[] token)
    {
      
        return Inventory.listInventory(token);
    }

    private static string pickup(string[] token)
    {
        return Inventory.pickup(token);
    }

    private static string drop(string[] token)
    {
        return Inventory.drop(token);
    }

    private static string look(string[] token)
    {
        return WorldData.Look(token);
    }

    private static string go(string[] token)
    {
        return WorldData.Go(token);
    }
}
