using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using System.Security.AccessControl;

public class GameData : MonoBehaviour
{
    public static List<Texture2D> backgroundList = new List<Texture2D>();
    public static Texture2D minimapTexture;
    [System.Serializable]
    public class GameInformation
    {
        List<LocationData.Location> locations = new List<LocationData.Location>();
        List<ItemData.Item> items = new List<ItemData.Item>();

        string _combatLog;
        //access all variables from WorldData.gameData.<modifier>
        public string CombatLog
        {
            get
            {
                return _combatLog;
            }
            set
            {
                _combatLog = value;
            }
        }

        public List<LocationData.Location> Locations
        {
            get
            {
                return locations;
            }
            set
            {
                locations = value;
            }
        }

        public List<ItemData.Item> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        //going public due to lazy ^
        public int currentLoc;
        int turn;
        string playerName;
        string playerGender;
        string playerClass;
        playerStats stats;


        string gameName = "Blank";
        string xmlName = "Blank";


        string[] inventoryData = new string[10];

        public List<Texture2D> BackgroundList
        {
            get
            {
                return backgroundList;
            }
        }

        public Texture2D MinimapTexture
        {
            get
            {
                return minimapTexture;
            }
            set
            {
                minimapTexture = value;
            }
        }

    public string XmlName
      {
        get
          {
            return xmlName;
          }
        set
          {
            xmlName = value;
          }
      }
        public string[] InventoryData
        {
            get
            {
                return inventoryData;
            }
            set
            {
                inventoryData = value;
            }
        }

        public string GameName
        {
            get
            {
                return gameName;
            }
            set
            {
                gameName = value;
            }
        }

        string introText;

        public string IntroText
        {
            get
            {
                return introText;
            }
            set
            {
                introText = value;
            }
        }

        public GameInformation(List<LocationData.Location> locations, List<ItemData.Item> items, string playerName, string playerGender, string playerClass, int currentLoc, playerStats stats, int turn)
        {
            this.locations = locations;
            this.items = items;
            this.playerName = playerName;
            this.playerGender = playerGender;
            this.playerClass = playerClass;
            this.currentLoc = currentLoc;
            this.stats = stats;
            this.turn = turn;
        }

        public void playerTurn()
        {
            //Put world moves here

            this.turn++;
        }

        public string serialize(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (!Directory.Exists(Application.persistentDataPath + "/SaveGames/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/SaveGames/");
            }
            try
            {
                FileStream file = File.Open(Application.persistentDataPath + "/SaveGames/" + fileName, FileMode.Create);     
                formatter.Serialize(file, this);
                file.Close();
                return "saved";
            }
            catch
            {
                return "error";
            }
        }

        public void loadGameInfo(string xmlLocation)
        {
            XDocument gameInfo = null;
            
            try
            {
                //Debug.Log(xmlLocation);
                gameInfo = XDocument.Load(Application.persistentDataPath + "/Scenarios/" + xmlLocation, LoadOptions.PreserveWhitespace);
                
            }
            catch
            {
                GUISelector.message = "There was an error loading this scenario, please select another scenario.";
                GUISelector.Gui = 3;
                GUISelector.PreviousGui = 0;
                //Debug.Log("error reading XML");
            }
            if (gameInfo != null)
            {
                this.gameName = gameInfo.Element("World").Element("Game_Info").Element("GameName").Value.ToString();
                this.introText = gameInfo.Element("World").Element("Game_Info").Element("IntroString").Value.ToString();
                this.xmlName = gameInfo.Element("World").Element("Game_Info").Element("XMLName").Value.ToString();



               minimapTexture = new Texture2D(1024,1024);
                minimapTexture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/Scenarios/"+ this.xmlName + "/minimap.png"));
                backgroundList = new List<Texture2D>();
                foreach(string a in Directory.GetFiles(Application.persistentDataPath + "/Scenarios/"+ this.xmlName +"/Backgrounds"))
                {
                    if(a != Application.persistentDataPath + "/Scenarios/"+ this.xmlName +"/Backgrounds" + "/.DS_Store")
                    
                    {Texture2D b = new Texture2D(1024,1024);
                    b.LoadImage(File.ReadAllBytes(a));
                    //Debug.Log(a);
                    backgroundList.Add(b);
                    }
                }
                //FileStream f1 = File.Create(Application.persistentDataPath + "/Scenarios/BaseGame/Backgrounds/" + b.name +".png");

            }
        }

        public static GameInformation deserialize(string fileName)
        {
            if (File.Exists(Application.persistentDataPath + "/SaveGames/" + fileName))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (var stream = File.Open(Application.persistentDataPath + "/SaveGames/" + fileName,FileMode.Open,FileAccess.Read))
                {
                    GameInformation a = (GameInformation)formatter.Deserialize(stream);
                    stream.Flush();
                    stream.Close();
                    return a;
                }

            }
            else
            {
                return null;
            }
        }

        public playerStats Stats
        {
            get
            {
                return stats;
            }
        }

        public string getName()
        {
            char[] temp = this.playerName.ToCharArray();
            string temp2 = temp [0].ToString().ToUpper() + new string(temp).TrimStart(temp [0]).ToLower();
            return temp2;
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

        public void addItem(string name, string description, int location, int weight, int openState, int itemType, int usesLeft, int s, int p, int e, int a, int l, int rs, int rp, int re, int ra, int rl, int armor, int attack, int useEffect, int useModifier, int id)
        {
            items.Add(new ItemData.Item(name, description, location, weight, openState, itemType, usesLeft, s, p, e, a, l, rs, rp, re, ra, rl, armor, attack, useEffect, useModifier, id));

        }

        public void addLocation(string name, string description, int nodeNumber, int[] adjacentNodes, string[] adjacentDirections, int reqItem, int reqUse, int reqOpen, int reqClosed, int lox, int loy)
        {
            locations.Add(new LocationData.Location(name, description, nodeNumber, adjacentNodes, adjacentDirections, reqItem, reqUse, reqOpen, reqClosed, lox, loy));
        }
    }











    static int saveState = 0;
    static int gameState;

    public static string startSave(string[] token)
    {
        if (token.Length > 1)
        {
            return "too many args";
        }
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

            saveName = token [0] + ".dat";
            if (!File.Exists(Application.persistentDataPath + "/SaveGames/" + saveName))
            {
               

                if (WorldData.gameData.serialize(saveName) == "saved")
                {

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
                    else if (gameState == 6)
                    {
                        GenericCommands.returnToMenu("Your game has been saved, returning to the main menu");
                        return "Saved!\n<returning to main menu>";
                    }
                }
                else
                {
                    saveState = 3;
                    return "there was an error saving the game, do you want to rename the file?";
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

                if (WorldData.gameData.serialize(saveName) == "saved")
                {
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
                    else if (gameState == 6)
                    {
                        GenericCommands.returnToMenu("Your game has been saved, returning to the main menu");
                        ParserSelect.parserSelect = gameState;
                        return "Saved!\n<returning to main menu>";
                    }
                }
                else
                {
                    saveState = 3;
                    return "there was an error saving the game, do you want to rename the file?";
                }
            }
            if (token [0].Equals("no"))
            {
                saveState = 3;
                return "Do you want to rename your save file?";

            }
            else
            {
                return "Invalid Input";
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
                else if (gameState == 6)
                {
                    GenericCommands.returnToMenu("Your game has not been saved, returning to the main menu");
                    ParserSelect.parserSelect = gameState;
                    return "Saved!\n<returning to main menu>";
                }
            }
            else
            {
                return "Invalid Input";
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
        if (!Directory.Exists(Application.persistentDataPath + "/SaveGames/"))
        {
            return "There aren't any files to load";
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
                listSaveFiles();
                if (files.Count >= 1)
                {
                    loadStatus = 2;
                    return listSaveFiles();
                }
                else
                {
                    string s = "there aren't any save files\n";
                    if (gameState == 0)
                    {
                        ParserSelect.parserSelect = gameState;
                        return s + "<<returning to menu>>";
                    }
                    else if (gameState == 1)
                    {
                        ParserSelect.parserSelect = gameState;
                        return s + "<<returning to game>>";
                    }
                }

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
            if (parsed > 0 && parsed <= fileCount)
            {
                GameData.GameInformation testNull = GameData.GameInformation.deserialize(files [parsed - 1]);
                if (testNull != null)
                {
                    WorldData.gameData = testNull;
                    return "<<Resuming Game>>";
                }
                else
                {
                    //return to menu / game
                    return "error loading file";
                } 


            }
            else
            {
                return "Invalid input\n" + listSaveFiles();
            }

        }
        return "Guru Mediation x0000008";

    }

    public static int fileCount;

    public static string listSaveFiles()
    {
        fileCount = 0;
        string baseline = "Enter in the number of the save that you want to load\n--------Saves--------";
        
        string[] UncleanScenarios = Directory.GetFiles(Application.persistentDataPath + "/SaveGames/");
        string midline = "";
        foreach (string a in UncleanScenarios)
        {
            string[] cleaner = a.Trim().Replace("/", " ").Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
            if (cleaner [cleaner.Length - 1].ToLower().Contains(".dat"))
            {
                fileCount ++;
                files.Add(cleaner [cleaner.Length - 1]);
                midline = "\n" + fileCount + ") " + cleaner [cleaner.Length - 1] + midline;
            }
        }
        string final = baseline + midline + "\n--------------------";
        return final;
    }
}

[System.Serializable]
public class playerStats
{  
    /*
     * Stats:
     * 
     * Strength: Melee Weapon Modifier / Weapon type you can use <- dont care about spelling
     * Perception: Ranged Weapon Modifier / Weapon type you can use
     * Endurance: Health Modifier
     * Charisma: not included
     * Intelligence: not included... <Fallout Joke Completed>
     * Agility: Speed Modifier - Higher Doge Chance (such agility much dodge so wow!)
     * Luck: Higher odds of finding better Weapons / Items
     * 
     * Total: 5 stats
     * 
     * Base Character:
     * Strength: 5
     * Perception: 5
     * Endurance: 5
     * Agility: 5
     * Luck: 5
     * 
     * 
     * Higher: 1    --  Mathf.Clamp((int)Mathf.Ceil(Random.Range(100, 400) / 100), 1, 3);
     * High: 1      --   Mathf.Clamp((int)Mathf.Ceil(Random.Range(100, 300) / 100), 1, 2);
     * Meh: 2       --  Mathf.Clamp((int)Mathf.Ceil(Random.Range(0, 110) / 100), 0, 1);
     * Bad: 1      --  -Mathf.Clamp((int)Mathf.Ceil(Random.Range(100, 300) / 100), 1, 2);
     * 
     * Hunter - High Strength, Higher Perception, Meh Endurance, Meh Agility, Bad Luck 
     * 
     * Thief - Meh Strength, Meh Perception, Bad Endurance, Higher Agility, High Luck 
     * 
     * Knight - Higher Strength, Meh Perception, High Endurance, Bad Agility, Meh Luck
     * 
     * Hunter (Extreme Stats):
     * Strength: 8
     * Perception: 7
     * Endurance: 5
     * Agility: 5
     * Luck: 3
     * 
     * Thief (Extreme Stats):
     * Strength: 5
     * Perception: 5
     * Endurance: 3
     * Agility: 8
     * Luck: 7
     * 
     * Knight (Extreme Stats):
     * Strength: 8
     * Perception: 5
     * Endurance: 7
     * Agility: 3
     * Luck: 5
     * 
     */
    int _str;
    int _per;
    int _end;
    int _agil;
    int _luck;
    int _playerHealth;
    int _playerHealthMax;
    
    public int getHealth()
    {
        return _playerHealth;
    }

    public string displayStats()
    {
       
        string total = "----- Stats ----\n";
        string end = "\n";
        for (int i = total.Length; i > 0; i--)
        {
            end += "-";
        }
        total += ("Health: " + _playerHealth + " / " + _playerHealthMax + "\n");
        total += ("Strength: " + _str + "\n");
        total += ("Perception: " + _per + "\n");
        total += ("Endurance: " + _end + "\n");
        total += ("Agility: " + _agil + "\n");
        total += ("Luck: " + _luck);
        return total + end;
    }

    public void setHealth(int playerHealth)
    {
        _playerHealth = Mathf.Clamp(playerHealth, -10, _playerHealthMax);
    }

    public void RecalculateMaxHealth()
    {
        _playerHealthMax = (Mathf.CeilToInt((_str / 2 + _end * 2) * 10));
    }

    public int MaxHealth
    {
        get
        {
            return _playerHealthMax;
        }
    }
    
    public int Strength
    {
        get
        {
            return _str;
        }
    }
    
    public int Perception
    {
        get
        {
            return _per;
        }
    }
    
    public int Endurance
    {
        get
        {
            return _end;
        }
    }
    
    public int Agility
    {
        get
        {
            return _agil;
        }
    }
    
    public int Luck
    {
        get
        {
            return _luck;
        }
    }
    
    public playerStats(string characterClass, string characterGender)
    {
        int highestRoll = Mathf.Clamp((int)Mathf.Ceil(Random.Range(100, 400) / 100), 1, 3);
        int highRoll = Mathf.Clamp((int)Mathf.Ceil(Random.Range(100, 300) / 100), 1, 2);
        int badRoll = - Mathf.Clamp((int)Mathf.Ceil(Random.Range(100, 300) / 100), 1, 2);
        int mehRoll1 = Mathf.Clamp((int)Mathf.Ceil(Random.Range(0, 110) / 100), 0, 1);
        int mehRoll2 = Mathf.Clamp((int)Mathf.Ceil(Random.Range(0, 110) / 100), 0, 1);
        if (characterClass == "hunter")
        {
            if (characterGender == "male")
            {
                _str = 5 + highRoll;
                _per = 4 + highestRoll;
                _end = 5 + mehRoll1;
                _agil = 4 + mehRoll2;
                _luck = 5 + badRoll;
            }
            else if (characterGender == "female")
            {
                _str = 4 + highRoll;
                _per = 5 + highestRoll;
                _end = 4 + mehRoll1;
                _agil = 5 + mehRoll2;
                _luck = 5 + badRoll;
            }
            else
            {
                _str = 5 + highRoll;
                _per = 5 + highestRoll;
                _end = 5 + mehRoll1;
                _agil = 5 + mehRoll2;
                _luck = 5 + badRoll;
            }

        }
        else if (characterClass == "thief")
        {
            if (characterGender == "male")
            {
                _str = 5 + mehRoll1;
                _per = 4 + mehRoll2;
                _end = 5 + badRoll;
                _agil = 4 + highestRoll;
                _luck = 5 + highRoll;
            }
            else if (characterGender == "female")
            {
                _str = 4 + mehRoll1;
                _per = 5 + mehRoll2;
                _end = 4 + badRoll;
                _agil = 5 + highestRoll;
                _luck = 5 + highRoll;
            }
            else
            {
                _str = 5 + mehRoll1;
                _per = 5 + mehRoll2;
                _end = 5 + badRoll;
                _agil = 5 + highestRoll;
                _luck = 5 + highRoll;
            }

            
        }
        else if (characterClass == "knight")
        {
            if (characterGender == "male")
            {
                _str = 5 + highestRoll;
                _per = 4 + mehRoll1;
                _end = 5 + highRoll;
                _agil = 4 + badRoll;
                _luck = 5 + mehRoll2; 
            }
            else if (characterGender == "female")
            {
                _str = 4 + highestRoll;
                _per = 5 + mehRoll1;
                _end = 4 + highRoll;
                _agil = 5 + badRoll;
                _luck = 5 + mehRoll2; 
            }
            else
            {
                _str = 5 + highestRoll;
                _per = 5 + mehRoll1;
                _end = 5 + highRoll;
                _agil = 5 + badRoll;
                _luck = 5 + mehRoll2; 
            }

        }
        _playerHealth = _playerHealthMax = (Mathf.CeilToInt((_str / 2 + _end * 2) * 10));
    }
}