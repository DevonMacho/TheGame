using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class MainMenuParser : MonoBehaviour
{


    private static Dictionary<string, int> commandList0;

    public static void initializeCommands()
    {
        commandList0 = new Dictionary<string,int >();
        #region Main Menu Commands
        commandList0.Add("help", 0);
        commandList0.Add("clear", 1);
        commandList0.Add("quit", 2);
        commandList0.Add("load", 3);
        commandList0.Add("newgame", 4);
        commandList0.Add("import", 5);
        #endregion

        if (!Directory.Exists(Application.persistentDataPath + "/Scenarios/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Scenarios/");
            //copy over main game xml from Asset folder
        }
    }

    public static string Parse(string input)
    {
        if (commandList0 == null)
        {
            initializeCommands();
        }
        string[] token = GenericCommands.tokenize(input);
        if (token.Length <= 0 || !commandList0.ContainsKey(token [0].ToLower()))
        {
            return "Please enter a valid command";
        }
        
        int command = commandList0 [token [0].ToLower()];
        if (command == 0)
        {
            return help(token);
        }
        else if (command == 1)
        {
            return GenericCommands.clear();
        }
        else if (command == 2)
        {
            return GenericCommands.startQuit(token);
        }
        else if (command == 3)
        {
            return GameData.startLoad();
        }
        else if (command == 4)
        {
            return NewGameParser.startNewGame();
        }
        else if (command == 5)
        {
            return import();
        }
        else
        {
            return "you have entered in a valid command";
        }
    }

    private static string import()
    {

        string fileLoc = EditorUtility.OpenFilePanel("Import Scenario", Application.persistentDataPath + "/Scenarios/", "xml");
        if (fileLoc.Equals(""))
        {
            return "Invalid Directory";
        }
        string fileLoc2 = fileLoc.Trim().Replace("/", " ");
        string[] directories;
        directories = fileLoc2.Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
        Debug.Log(directories [directories.Length - 1]);
        
        try
        {
            Debug.Log(Application.persistentDataPath + "/Scenarios/"+ directories [directories.Length - 1]);
            if (File.Exists(Application.persistentDataPath + "/Scenarios/" + directories [directories.Length - 1]))
            {
                return "file exists, please delete" + directories [directories.Length - 1];
            }
            else
            {
                //check valid XML file
                FileUtil.CopyFileOrDirectory(fileLoc, Application.persistentDataPath + "/Scenarios/"+ directories [directories.Length - 1]);
            }
        }
        catch
        {
            return "error copying data";
        }

        return directories [directories.Length - 1] + " Imported!";
    }

    private static string help(string[] token)
    {
        string [] genHelp = new string[]
        {
            "Displays a list of commands / help with a particular command",
            "Clears the command line output",
            "Lets you quit the game",
            "Allows you to load up a saved game",
            "Allows you to start a new game",
            "Allows you to import an XML file"
        };
        string [] genHelpMod = new string[]
        {
            "Help <Command>",
            "None",
            "None",
            "None",
            "None",
            "None"
        };
        if (token.Length == 1)
        {
            string lst = "----- Commands ----\n";
            foreach (string i in commandList0.Keys)
            {
                lst += i + "\n";
            }
            lst += "------------------";
            return lst;
        }
        else if (token.Length == 2)
        {
            #region Detailed Help modifiers

            if (!commandList0.ContainsKey(token [1].ToLower()))
            {
                return "Invalid modifier";
            }
            else if (commandList0.ContainsKey(token [1].ToLower()))
            {
                int command = commandList0 [token [1].ToLower()];
                
                return genHelp [command] + "\n------Modifiers------\n" + genHelpMod [command] + "\n ---------------------";
            }
            #endregion
        }
        else if (token.Length > 2)
        {
            return "too many args";  
        }
        return "Guru Meditation x0000001";
        
    }
}
