using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Terminal : MonoBehaviour
{
    static bool mini = true;
    static bool stat = true;
    static string input = "";
    static Rect miniMap;
    static Rect stats;
    static Vector2 scrollPosition;
    static Vector2 ScrollPosition2;
    static Vector2 ScrollPosition3;
    public static string consoleLog = "";
    static List<string> commandHistory;
    static Texture clb;
    static GUISkin skin;
    static int commandIndex;

    public static void Start()
    {
        consoleLog = "";
        clb = Resources.Load("GUI Assets/CommandLineBackground") as Texture;
        skin = Resources.Load("GUI Assets/GameSkin") as GUISkin;
      
        
       
    }

    public static string toggleStats(string[] input)
    {
        if (input.Length > 1)
        {
            return "too many args";
        }
        stat = !stat;
        if (!stat)
        {
            return "The stat window was turned off";
        }
        else
        {
            return "The stat window was turned on";
        }

    }

    public static string toggleMiniMap(string[] input)
    {

        if (input.Length > 1)
        {
            return "too many args";
        }
        mini = !mini;
        if (!mini)
        {
            
            return "minimap was turned off";
        }
        else
        {
            return "minimap was turned on";
        }  
    }

    public static void Update()
    {

        miniMap = new Rect(GlobalSettings.MinimapX, GlobalSettings.MinimapY, Screen.height / 6, Screen.height / 6);
        stats = new Rect(GlobalSettings.StatsX, Screen.width * (GlobalSettings.StatsY / 1024) + 17, Screen.width / 4, Screen.height * 99 / 200);
    }

    public static void OnGUI()
    {
        if (WorldData.gameData != null)
        {
            if (WorldData.gameData.BackgroundList != null)
            {
                //Debug.Log(WorldData.gameData.BackgroundList.Count);
                if (WorldData.gameData.currentLoc < WorldData.gameData.BackgroundList.Count)
                {
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height * 2 / 3), WorldData.gameData.BackgroundList [WorldData.gameData.currentLoc], ScaleMode.ScaleToFit);
                }  
            }
            if (mini)
            {
                GUI.Box(miniMap, "");
                GUI.BeginGroup(miniMap);
                GUI.DrawTexture(new Rect(-WorldData.gameData.Locations [WorldData.gameData.currentLoc].Lox + miniMap.width / 2, -WorldData.gameData.Locations [WorldData.gameData.currentLoc].Loy + miniMap.height / 2, 1024, 1024), WorldData.gameData.MinimapTexture);
                GUI.EndGroup();

            }
            if (stat)
            {
                if (WorldData.gameData.InventoryData == null && WorldData.gameData != null)
                {
                    Inventory.updateInventory();
                }
                GUI.skin = Resources.Load("GUI Assets/Stats_Centered") as GUISkin;
                GUI.Box(stats, "");
                GUI.BeginGroup(stats);

                GUILayout.BeginArea(new Rect(stats.x / 100, 0, stats.width * 19 / 20, stats.height));
                GUILayout.BeginVertical();

                GUILayout.Label("Name: " + "\"" + WorldData.gameData.getName() + "\"");
                //GUILayout.Space(stats.height * 1 / 20);
                GUILayout.Label("Stats");
                GUI.skin = Resources.Load("GUI Assets/Stats") as GUISkin;
                ScrollPosition2 = GUILayout.BeginScrollView(ScrollPosition2, GUILayout.Width(stats.width * 19 / 20), GUILayout.Height(stats.height * 3 / 10));
                //int testInt = 0;
                GUILayout.Label("Health:\t" + WorldData.gameData.Stats.getHealth() + " / " + WorldData.gameData.Stats.getHealth() );
                GUILayout.Label("Attack:\t" + WorldData.gameData.Stats.BaseAttack + " + " + WorldData.gameData.AttackMod+ " = " + WorldData.gameData.TotalAttack);
                GUILayout.Label("Strength:\t" + WorldData.gameData.Stats.Strength + " + " + WorldData.gameData.StrengthModifier+ " = " + WorldData.gameData.TotalStrength);
                GUILayout.Label("Per. :\t" + WorldData.gameData.Stats.Perception + " + " + WorldData.gameData.PerceptionModifier+ " = " + WorldData.gameData.TotalPerception);
                GUILayout.Label("End. :\t" + WorldData.gameData.Stats.Endurance + " + " + WorldData.gameData.EnduranceModifier+ " = " + WorldData.gameData.TotalEndurance);
                GUILayout.Label("Agility:\t" + WorldData.gameData.Stats.Agility + " + " + WorldData.gameData.AgilityModifier+ " = " + WorldData.gameData.TotalAgility);
                GUILayout.Label("Luck:\t" + WorldData.gameData.Stats.Luck + " + " + WorldData.gameData.LuckModifier+ " = " + WorldData.gameData.TotalLuck);
                GUILayout.Label("Armor:\t" + WorldData.gameData.Armor);

                GUILayout.EndScrollView();
                GUI.skin = Resources.Load("GUI Assets/Stats_Centered") as GUISkin;
                GUILayout.Label("Equipped Items");
                GUI.skin = Resources.Load("GUI Assets/Stats") as GUISkin;
                ScrollPosition3 = GUILayout.BeginScrollView(ScrollPosition3, GUILayout.Width(stats.width * 19 / 20), GUILayout.Height(stats.height * 2 / 5));
                // 0 not usable
                // 1 usable
                // 2 head
                // 3 chest (body)
                // 4 gauntlets
                // 5 belt
                // 6 pants
                // 7 shoes
                // 8 weapon
               
                GUILayout.Label("Head:\t" + WorldData.gameData.InventoryData [0]);
                GUILayout.Label("Chest:\t" + WorldData.gameData.InventoryData [1]);
                GUILayout.Label("Gauntlets:\t" + WorldData.gameData.InventoryData [2]);
                GUILayout.Label("Belt:\t" + WorldData.gameData.InventoryData [3]);
                GUILayout.Label("Pants:\t" + WorldData.gameData.InventoryData [4]);
                GUILayout.Label("Shoes:\t" + WorldData.gameData.InventoryData [5]);
                GUILayout.Label("Weapon:\t" + WorldData.gameData.InventoryData [6]);
                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndArea();
                GUI.EndGroup();
                
            }
        }
        if (commandHistory == null)
        {
            commandHistory = new List<string>();
            commandHistory.Add("");
            commandIndex = 1;
        }
        GUI.skin = skin;
        GUI.DrawTexture(new Rect(0, Screen.height - (Screen.height * 256 / 768), Screen.width, Screen.height * 256 / 768), clb, ScaleMode.StretchToFill, true, 10.0F);
        GUILayout.BeginArea(new Rect(24, Screen.height - (Screen.height * 240 / 768), Screen.width - 48, (Screen.height * 240 / 768)));
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width - 48), GUILayout.Height(Screen.height * 179 / 768));
        GUILayout.Label(consoleLog);
        GUILayout.EndScrollView();
        if (GUI.GetNameOfFocusedControl() == "textField")
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
            {
                SubmitCommand();
            }
            else if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
            {
                if (commandIndex < commandHistory.Count)
                {
                    commandIndex++;
                    input = commandHistory [commandIndex - 1];
                }
            }
            else if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
            {
                if (commandIndex > 1)
                {
                    commandIndex--;
                    input = commandHistory [commandIndex - 1];
                }
            }
        }
        if (GUI.Button(new Rect(0, (Screen.height * 179 / 768), 60, (Screen.height * 40 / 768)), "Submit"))
        {
            SubmitCommand();
        }
        GUI.SetNextControlName("textField");
        input = GUI.TextField(new Rect(70, (Screen.height * 179 / 768), Screen.width - 120, (Screen.height * 40 / 768)), input);
        GUILayout.EndArea();
    }

    static void SubmitCommand()
    {
        //parser selecter
        string output = ParserSelect.Parser(input);
        //end parser swap
        commandIndex = 1;
        scrollPosition.y = Mathf.Infinity;
        if (!input.Equals(""))
        {
            consoleLog = consoleLog + input + "\n";
            commandHistory.Insert(1, input);
        }
        if (ParserSelect.parserSelect == 4) // used for dialog because it looks nicer with dialog
        {
            consoleLog = consoleLog + "\n" + output + "\n";
        }
        else
        {
            consoleLog = consoleLog + output + "\n\n";
        }
        if (output.Equals("<<Clearing>>"))
        {
            consoleLog = "<<Cleared>>\n";
            commandHistory = new List<string>();
            commandHistory.Add("");
            commandIndex = 1;
        }
        input = "";
    }
}
