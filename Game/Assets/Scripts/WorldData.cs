using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;

public class WorldData : MonoBehaviour
{
    public static GameData.GameInformation gameData;

    public static void generateScenarios()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Scenarios/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Scenarios/");
        }
        string[] assets = {"Scenarios/BaseGame","Scenarios/Readme"};
        
        
        TextAsset basegame = Resources.Load(assets [0]) as TextAsset;
        TextAsset readme = Resources.Load(assets [1]) as TextAsset;
        
        
        byte[] baseText = basegame.bytes;
        byte[] readText = readme.bytes;
        FileStream file1 = File.Create(Application.persistentDataPath + "/Scenarios/BaseGame.xml");
        FileStream file2 = File.Create(Application.persistentDataPath + "/Scenarios/Readme.txt");
        file1.Write(baseText, 0, baseText.Length);
        file2.Write(readText, 0, readText.Length);
        file1.Close();
        file2.Close();
    }

    public static string StartNewGame(string playerName, string playerGender, string playerClass, string xmlFile)
    {
        gameData = new GameData.GameInformation(loadLocationData(xmlFile), loadItemData(xmlFile), playerName, playerGender, playerClass, 0);
        return "\n<<Game Started>>\n\n" + gameData.locations[gameData.currentLoc].getDescription(); //New Game Message here instead of currentloc <- add that to XML
    }

    public static string Go(string[] command)
    {
        if (command.Length <= 1)
        {
            return "Go Where?";
        }
        else if(command.Length > 2)
        {
            return "you can't go to more than one place at a time, unless you happen to split into two things, which may happen soon if you aren't careful";
        }
        else
        {
            foreach (LocationData.Location a in gameData.locations)
            {
                if (a.getNodeNumber() == gameData.currentLoc)
                {
                    for (int i = 0; i < a.getAdjacentNodes().Length; i++)
                    {
                        if (a.getAdjacentDirections() [i].Equals(command [1]))
                        {
                            gameData.currentLoc = a.getAdjacentNodes() [i];
                            return "Going: " + a.getAdjacentDirections() [i] + "\n" + gameData.locations[gameData.currentLoc].getDescription();
                        }
                    }
                }

            }
            return "Location does not exist";
        }
    }

    public static List<LocationData.Location> loadLocationData(string xmlLocation)
    {
        XDocument locationInfo = null;
        List<LocationData.Location> locData = new List<LocationData.Location>();

        try
        {
            //Debug.Log(xmlLocation);
            locationInfo = XDocument.Load(Application.persistentDataPath + "/Scenarios/" + xmlLocation, LoadOptions.PreserveWhitespace);
            
        }
        catch
        {
            GUISelector.message = "There was an error loading this scenario, please select another scenario.";
            GUISelector.Gui = 3;
            GUISelector.PreviousGui = 0;
            //Debug.Log("error reading XML");
        }
        if (locationInfo != null)
        {
            var location = locationInfo.Element("World").Element("Locations").Elements("Location");

            foreach (var a in location)
            {
                string n = a.Element("Location_Name").Value.ToString();
                string d = a.Element("Location_Description").Value.ToString();
                int nn = (int)a.Element("Location_NodeNumber");
                List<int> an = new List<int>();
                List<string> ad = new List<string>();

                foreach (var ani in a.Elements("Location_AdjacentNodes"))
                {
                    an.Add((int)ani);
                }
                foreach (var adi in a.Elements("Location_AdjacentDirections"))
                {
                    ad.Add(adi.Value.ToString());
                }

                locData.Add(new LocationData.Location(n, d, nn, an.ToArray(), ad.ToArray()));
            }
        }
        return locData;
    }

    public static List<ItemData.Item> loadItemData(string xmlLocation)
    {
        XDocument iteminfo = null;
        List<ItemData.Item> itemData = new List<ItemData.Item>();
        
        try
        {
            iteminfo = XDocument.Load(Application.persistentDataPath + "/Scenarios/" + xmlLocation, LoadOptions.PreserveWhitespace);  
        }
        catch
        {
            GUISelector.message = "There was an error loading this scenario, please select another scenario.";
            GUISelector.Gui = 3;
            GUISelector.PreviousGui = 0;
        }
        if (iteminfo != null)
        {
            var itemz = iteminfo.Element("World").Element("Items").Elements("Item");
            
            foreach (var a in itemz)
            {
                string n = a.Element("Item_Name").Value.ToString();
                string d = a.Element("Item_Description").Value.ToString();
                int nn = (int)a.Element("Item_Location");
                int w = (int)a.Element("Item_Weight");
                int os = (int)a.Element("Item_OpenState");
                int it = (int)a.Element("Item_Type");
                int ul = (int)a.Element("Item_Uses");
                itemData.Add(new ItemData.Item(n, d, nn, w, os, it, ul));
            }
        }
        return itemData;
    }

    public static string Look(string[] command)
    {
        if (command.Length == 1)
        {
            return "look where?";
        }
        else if (command.Length <= 3)
        {
            if (command [1].Equals("around") && command.Length == 2)
            {
                foreach (LocationData.Location a in gameData.locations)
                {
                    if (a.getNodeNumber() == gameData.currentLoc)
                    {
                        string directions = "From here you can go: ";
                        for (int i = 0; i < a.getAdjacentDirections().Length; i++)
                        {
                            directions = directions + a.getAdjacentDirections() [i] + " ";
                        }
                        return a.getDescription() + "\n" + directions + "\n" + Inventory.itemsAtLocation(gameData.currentLoc);
                    }
                }
            }
            else if (command [1].Equals("at"))
            {
                if (command.Length == 2)
                {
                    return "Look at what?";
                }

                foreach (ItemData.Item a in gameData.items)
                {
                    if (a.getName().Equals(command [2]) && (a.getLocation() == gameData.currentLoc || a.getLocation() == -1))
                    {
                        if (a.getWeight() < 999)
                        {
                            return a.getDescription() + "\nWeight: " + a.getWeight();
                        }
                        else
                        {
                            //this prevents "world objects / switches" from displaying their weight
                            return a.getDescription();
                        }
                       

                    }
                }

                return "Object does not exist at current location";
            }
        }
        else if (command.Length > 3)
        {
            return "Too many args";
        }
       
        return "Invalid modifier";
     
    }
}
