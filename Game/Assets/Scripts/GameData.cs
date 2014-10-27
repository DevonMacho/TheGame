using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData : MonoBehaviour
{
    [System.Serializable]
    public class GameInformation
    {

        public List<LocationData.Location> locations = new List<LocationData.Location>();
        public List<ItemData.Item> items = new List<ItemData.Item>();
        //going public due to lazy ^
        public int currentLoc;
        string playerName;
        string playerGender;
        string playerClass;

        public GameInformation(List<LocationData.Location> locations, List<ItemData.Item> items, string playerName, string playerGender, string playerClass, int currentLoc)
        {
            this.locations = locations;
            this.items = items;
            this.playerName = playerName;
            this.playerGender = playerGender;
            this.playerClass = playerClass;
            this.currentLoc = currentLoc;
        }

        public void serialize(GameInformation saveData, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Debug.Log("1");
            if (!Directory.Exists(Application.persistentDataPath + "/SaveGames/"))
            {
                Debug.Log("2");
                Directory.CreateDirectory(Application.persistentDataPath + "/SaveGames/");
            }
            Debug.Log("3");
            FileStream file = File.Create(Application.persistentDataPath + "/SaveGames/" + fileName);
            Debug.Log("4");
            formatter.Serialize(file, saveData);
            Debug.Log("5");
            file.Close();
        }
       
        public static GameInformation deserialize(string fileName)
        {
            if (File.Exists(Application.persistentDataPath + "/SaveGames/" + fileName))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/SaveGames/" + fileName, FileMode.Open);
                GameInformation a = (GameInformation)formatter.Deserialize(file);
                file.Close();
                return a;
            }
            else
            {
                Debug.Log("null");
                return null;
            }
        }

        public string getName()
        {
            return this.playerName;
        }

        public string getGender()
        {
            return this.playerGender;
        }

        public string getClass()
        {
            return this.playerClass;
        }

        public void getName(string playerName)
        {
            this.playerName = playerName;
        }

        public void getGender(string playerGender)
        {
            this.playerGender = playerGender;
        }

        public void getClass(string playerClass)
        {
            this.playerClass = playerClass;
        }

        public void addItem(string name, string description, int location, int weight, int openState)
        {
            items.Add(new ItemData.Item(name, description, location, weight, openState));
        }

        public void addLocation(string name, string description, int nodeNumber, int[] adjacentNodes, string[] adjacentDirections)
        {
            locations.Add(new LocationData.Location(name, description, nodeNumber, adjacentNodes, adjacentDirections));
        }
    }
    static int saveState = 0;
    static int gameState;

    public static string startSave()
    {
        saveState = 1;
        gameState = ParserSelect.parserSelect;
        return "<<Saving>>\nEnter in a name for the save file";
    }

    static string saveName;

    public static string saveParser(string input)
    {
        string[] token = GenericCommands.tokenize(input);

        if (token.Length <= 0)
        {
            return "Invalid Input";
        }
        if (token.Length > 1)
        {
            return "too many args";
        }
        if (saveState == 1)
        {
            if (token [0].Length < 1 || token [0].Length > 64 || token.Length <= 0)
            {
                return "Save file name not valid";
            }


            if (!File.Exists(Application.persistentDataPath + "/SaveGames/" + token [0] + ".save"))
            {
                saveName = token [0] + ".save";
                WorldData.gameData.serialize(WorldData.gameData, saveName);

                if (gameState == 1)
                {
                    ParserSelect.parserSelect = gameState;
                    return "Saved!\n<<returning to game>>";
                }
                else if (gameState == 5)
                {
                    ParserSelect.parserSelect = gameState;
                    return "Saved!\n<input anything to quit>";
                }
            }
            else
            {
                saveState = 2;
                return "File name exists, do you want to overwrite the save file?";
            }
        }
        if (saveState == 2)
        {
            if (token [0].Equals("yes"))
            {
                WorldData.gameData.serialize(WorldData.gameData, saveName);
                if (gameState == 1)
                {
                    ParserSelect.parserSelect = gameState;
                    return "Saved!\n<<returning to game>>";
                }
                else if (gameState == 5)
                {
                    ParserSelect.parserSelect = gameState;
                    return "Saved!\n<input anything to quit>";
                }
            }
            if (token [0].Equals("no"))
            {
                saveState = 3;
                return "Do you want to rename your save file?";

            }
        }
        if (saveState == 3)
        {
            if (token [0].Equals("yes"))
            {
                saveState = 1;
                return "Enter in a name for the save file";
            }
            if (token [0].Equals("no"))
            {
                if (gameState == 1)
                {
                    ParserSelect.parserSelect = gameState;
                    return "Game Not Saved!\n<<returning to game>>";
                }
                else if (gameState == 5)
                {
                    ParserSelect.parserSelect = gameState;
                    return "Game Not Saved!\n";
                }
            }
        }
        return "Guru Mediation x0000007";
    }

    static int loadStatus = 0;
    
    public static string startLoad(string[] token)
    {
        if (token.Length > 1)
        {
            return "Too many args";
        }
        loadStatus = 1;
        gameState = ParserSelect.parserSelect;
        files = new List<string>();
        return "<<Loading>>\nAre you Sure that you want to load?";
    }

    static List<string> files;

    public static string loadParser(string input)
    {
        string[] token = GenericCommands.tokenize(input);
        if (token.Length <= 0)
        {
            return "invalid input";
        }
        if (loadStatus == 1)
        {
            if (token.Length > 1)
            {
                return "too many args";
            }
            if (token [0].Equals("yes"))
            {
                loadStatus = 2;
                return listSaveFiles();

            }
            else if (token [0].Equals("no"))
            {
                if (gameState == 0)
                {
                    ParserSelect.parserSelect = gameState;
                    return "<<returning to menu>>";
                }
                else if (gameState == 1)
                {
                    ParserSelect.parserSelect = gameState;
                    return "<<returning to game>>";
                }
            }
            else
            {
                return "invalid input";
            }
        }
        if (loadStatus == 2)
        {
            if (token.Length <= 0)
            {
                return "Invalid input\n" + listSaveFiles();
            }
            if (token.Length > 1)
            {
                return "too many args";
            }
            int parsed;
            int.TryParse(token [0], out parsed);
            if (parsed > 0 && parsed <= files.Count)
            {
                WorldData.gameData = GameData.GameInformation.deserialize(files [parsed - 1]);
                return "<<Resuming Game>>";
            }
            else
            {
                return "Invalid input\n" + listSaveFiles();
            }

        }
        return "Guru Mediation x0000008";

    }

    public static string listSaveFiles()
    {
        string baseline = "Enter in the number of the save that you want to load\n--------Saves--------";
        
        string[] UncleanScenarios = Directory.GetFiles(Application.persistentDataPath + "/SaveGames/");
        string midline = "";
        int count = 0;
        foreach (string a in UncleanScenarios)
        {
            count ++;
            string[] cleaner = a.Trim().Replace("/", " ").Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
            files.Add(cleaner [cleaner.Length - 1]);
            midline = "\n" + count + ") " + cleaner [cleaner.Length - 1] + midline;
        }
        string final = baseline + midline + "\n--------------------";
        return final;
    }
}
