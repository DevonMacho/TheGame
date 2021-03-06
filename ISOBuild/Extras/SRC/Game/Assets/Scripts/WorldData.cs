﻿using UnityEngine;
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

        Texture2D minimap = Resources.Load("BaseGame/minimap") as Texture2D;
        object[] textures = Resources.LoadAll("BaseGame/Backgrounds");

        Directory.CreateDirectory(Application.persistentDataPath + "/Scenarios/BaseGame/Backgrounds");
       
        foreach (object a in textures)
        {

            if (a.GetType() == typeof(Texture2D))
            {
                Texture2D b = a as Texture2D;
                FileStream f1 = File.Create(Application.persistentDataPath + "/Scenarios/BaseGame/Backgrounds/" + b.name + ".png");
                f1.Write(b.EncodeToPNG(), 0, b.EncodeToPNG().Length);
                f1.Close();
            }
        }
        FileStream f2 = File.Create(Application.persistentDataPath + "/Scenarios/BaseGame/minimap.png");
        f2.Write(minimap.EncodeToPNG(), 0, minimap.EncodeToPNG().Length);
        f2.Close();
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
        playerStats baseStats = new playerStats(playerClass, playerGender); 
        gameData = new GameData.GameInformation(loadLocationData(xmlFile), loadItemData(xmlFile), playerName, playerGender, playerClass, 0, baseStats, 0);
        gameData.loadGameInfo(xmlFile);
        GUI_Terminal.consoleLog = "";
        GUI_Terminal.ScrollPosition = new Vector2(0,0);
        Inventory.updateInventory();
        WorldMoves.initEnemyList();
        return "\n<<Game Started>>\n\n" + gameData.IntroText + "\n\n" + gameData.Locations[0].getDescription();
    }

    public static bool inBattle()
    {
        foreach (Enemies.Enemy e in gameData.Enemy)
        {
            if (e.location == gameData.currentLoc)
            {
                //Debug.Log(e.location);
                return true;
            }
        }
        return false;
    }

    public static string attack()
    {
        foreach (Enemies.Enemy e in gameData.Enemy)
        {
            if (e.location == gameData.currentLoc)
            {

                Enemies.Enemy a = e;

                int damage =  Mathf.CeilToInt(Random.Range(0, gameData.TotalAttack)); 
               
                a.hp = (a.hp - damage);

                gameData.Enemy.Remove(e);

                if (a.hp <= 0)
                {


                    gameData.playerTurn();
                    gameData.CombatLog += "\nyou have attacked for " + damage + " damage!";
                    gameData.CombatLog += "\nenemy has been slain!";
                    return gameData.CombatLog; 
                    //drop
                }
            
                gameData.Enemy.Add(a);
                gameData.playerTurn(); 
                gameData.CombatLog += "\nyou have attacked for " + damage + " damage!";
                return gameData.CombatLog;
            }
        }
        return "there isn't an enemy here";
    }

    public static bool escapeRoll()
    {
        int roll = (int)Mathf.Ceil(Random.Range(0, 20));
        if (roll >= gameData.Stats.Agility + (int)(gameData.Stats.Luck / 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string Go(string[] command)
    {
        if (command.Length <= 1)
        {
            return "Go Where?";
        }
        else if (command.Length > 2)
        {
            return "you can't go to more than one place at a time, unless you happen to split into two things, which may happen soon if you aren't careful";
        }
        else
        {
            foreach (LocationData.Location a in gameData.Locations)
            {
                if (a.getNodeNumber() == gameData.currentLoc)
                {
                    for (int i = 0; i < a.getAdjacentNodes().Length; i++)
                    {
                        if (a.getAdjacentDirections() [i].Equals(command [1]))
                        {
                            if (!inBattle())
                            {
                                gameData.currentLoc = a.getAdjacentNodes() [i];
                                gameData.playerTurn();
                                return "Going: " + a.getAdjacentDirections() [i] + "\n" + gameData.Locations [gameData.currentLoc].getDescription() + "\n" + WorldData.gameData.CombatLog;

                            }
                            else
                            {
                                gameData.playerTurn();
                                if (escapeRoll())
                                {
                                    gameData.currentLoc = a.getAdjacentNodes() [i];
                                    return "Going: " + a.getAdjacentDirections() [i] + "\n" + gameData.Locations [gameData.currentLoc].getDescription() + "\n" + WorldData.gameData.CombatLog;
                                }
                                else
                                {
                                    return "you failed to escape...\n" + WorldData.gameData.CombatLog;
                                }
                            }
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
                int ri = (int)a.Element("Location_Required_Item");
                int ru = (int)a.Element("Location_Required_Use");
                int ro = (int)a.Element("Location_Required_Open");
                int rc = (int)a.Element("Location_Required_Close");
                int lox = (int)a.Element("GridLocationX");
                int loy = (int)a.Element("GridLocationY");
                locData.Add(new LocationData.Location(n, d, nn, an.ToArray(), ad.ToArray(), ri, ru, ro, rc, lox, loy));       
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

                int s = (int)a.Element("Item_Strength");
                int p = (int)a.Element("Item_Perception");
                int e = (int)a.Element("Item_Endurance");
                int i = (int)a.Element("Item_Agility");
                int l = (int)a.Element("Item_Luck");

                int rs = (int)a.Element("Item_REQ_Strength");
                int rp = (int)a.Element("Item_REQ_Perception");
                int re = (int)a.Element("Item_REQ_Endurance");
                int ra = (int)a.Element("Item_REQ_Agility");
                int rl = (int)a.Element("Item_REQ_Luck");

                int armor = (int)a.Element("Item_Armor");
                int attack = (int)a.Element("Item_Attack");

                int useEffect = (int)a.Element("Item_Use_Effect");
                int useModifier = (int)a.Element("Item_Use_Mod");

                int id = (int)a.Element("Item_ID");
               
                itemData.Add(new ItemData.Item(n, d, nn, w, os, it, ul, s, p, e, i, l, rs, rp, re, ra, rl, armor, attack, useEffect, useModifier, id));
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
                foreach (LocationData.Location a in gameData.Locations)
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

                foreach (ItemData.Item a in gameData.Items)
                {
                    if (a.getName().Equals(command [2]) && (a.getLocation() == gameData.currentLoc || a.getLocation() <= -1))
                    {
                        if (a.getWeight() < 999)
                        {
                            return a.getDescription() + 
                                "\nWeight: " + a.getWeight() +
                                "\nAttack Bonus: " + a.Attack + "\n" +
                                "\nArmor: " + a.Armor +
                                "\nStrength: " + a.S + "\t\tRequired Strength: " + a.Rs + 
                                "\nPerception: " + a.S + "\t\tRequired Perception: " + a.Rp +
                                "\nEndurance: " + a.S + "\t\tRequired Endurance: " + a.Re +
                                "\nAgility: " + a.S + "\t\tRequired Agility: " + a.Ra +
                                "\nLuck: " + a.S + "\t\tRequired Luck: " + a.Rl;
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