using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parser : MonoBehaviour
{
    private static Dictionary<string, int> commandList0;
    private static Dictionary<string, int> commandList1;
    private static Dictionary<string, int> devCommandList;
    private static string[] genHelp;
    private static string[] genHelpMod;
    private static bool devmode = false;

    public static void initializeCommands()
    {
        commandList0 = new Dictionary<string,int >();
        commandList1 = new Dictionary<string,int >();
        devCommandList = new Dictionary<string,int >();
        genHelp = new string[]
        {
            "Displays a list of commands / help with a particular command",
            "Clears the command line output",
            "Allows you to visualize the world via command line",
            "Allows you to move in a direction",
            "pickup",
            "drop",
            "inventory",
            "quit",
            "open",
            "close",
            "quip",
            "unequip",
            "use"
        };
        genHelpMod = new string[]
        {
            "Help <Command>",
            "None",
            "Around: Displays location information\nAt <Object>: Displays object information",
            "Go <Direction>: Goes in a particular direction. To get a list of locations use the look around command",
            "Pickup <Object>",
            "Drop <Object>",
            "ASC: List items in ascending order\nDEC: List items in decending order",
            "None",
            "??",
            "??",
            "Equip <Object>",
            "Unequip <Object>",
            "Use <Object>"
        };

        #region Main Menu Commands
        commandList0.Add("help", 0);
        commandList0.Add("clear", 1);
        commandList0.Add("quit", 2);
        commandList0.Add("load", 3);
        commandList0.Add("newgame", 4);
        commandList0.Add("import", 9);
        #endregion

        #region Basic Commands
        commandList1.Add("help", 0);
        commandList1.Add("clear", 1);
        commandList1.Add("look", 2);
        commandList1.Add("go", 3);
        commandList1.Add("pickup", 4);
        commandList1.Add("drop", 5);
        commandList1.Add("inventory", 6);
        //break for quit
        commandList1.Add("open", 8);
        commandList1.Add("close", 9);
        commandList1.Add("equip", 10);
        commandList1.Add("unequip", 11);
        commandList1.Add("use", 12);
        //endbreak for quit
        commandList1.Add("quit", 7);

      
        #endregion

        #region Developer Commands
        devCommandList.Add("devmode", 0);
        devCommandList.Add("setloc", 1);
        devCommandList.Add("additem", 2);
        devCommandList.Add("createloc", 3);
        devCommandList.Add("exportworld", 4);
        #endregion
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
        if (commandList1 == null)
        {
            initializeCommands();
        }
        string[] token = tokenize(input);
        if (token.Length <= 0 || !commandList1.ContainsKey(token [0].ToLower()))
        {
            if (token.Length <= 0 || devCommandList.ContainsKey(token [0].ToLower()))
            {
                #region devmode args
                int devcommand = devCommandList [token [0].ToLower()];
                if (devmode == true)
                {
                    if (devcommand == 0)
                    {

                        if (token.Length > 2)
                        {
                            return "too many args";
                        }
                        else if (token.Length <= 1)
                        {
                            return "devmode is enabled";
                        }
                        else if (token [1].ToLower().Equals("enable"))
                        {
                            return "devmode is already enabled";
                        }
                        else if (token [1].ToLower().Equals("disable"))
                        {
                            devmode = false;
                            return "devmode is now disabled";
                        }
                        else
                        {
                            return "unrecognized modifier";
                        }
                    }
                    else if (devcommand == 1)
                    {
                        
                    }
                    else if (devcommand == 2)
                    {
                        
                    }
                    else if (devcommand == 3)
                    {
                        
                    }
                    else if (devcommand == 4)
                    {
                        
                    }
                }
                else if (devmode == false)
                {
                    if (devcommand == 0)
                    {
                        if (token.Length > 2)
                        {
                            return "too many args";
                        }
                        else if (token.Length <= 1)
                        {
                            return "devmode is disabled";
                        }
                        else if (token [1].ToLower().Equals("enable"))
                        {
                            devmode = true;
                            return "devmode is now enabled";

                        }
                        else if (token [1].ToLower().Equals("disable"))
                        {
                            return "devmode is already disabled";
                        }
                        else
                        {
                            return "unrecognized modifier";
                        }

                    }
                }
                #endregion
            }
            return "Please enter a valid command";
        }

        int command = commandList1 [token [0].ToLower()];
        if (command == 0)
        {
            return help(token);
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
        else if (command == 8)
        {
            return "open command entered";
        }
        else if (command == 9)
        {
            return "close command entered";
        }
        else if (command == 10)
        {
            return "equip command entered";
        }
        else if (command == 11)
        {
            return "unequip command entered";
        }
        else if (command == 12)
        {
            //use
            return "use command entered";
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
            if (token [1].ToLower().Equals("-d"))
            {
                return "returning devmode command list";
            }
            if (token.Length <= 0 || !commandList1.ContainsKey(token [1].ToLower()))
            {
                return "Invalid modifier";
            }
            else
            {
                int command = commandList1 [token [1].ToLower()];

                return genHelp [command] + "\n------Modifiers------\n" +genHelpMod[command]+"\n ---------------------";
            }
            #endregion
        }
        else if (token.Length > 2)
        {
            return "too many args";  
        }
        return "something weird happened in the code";

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
