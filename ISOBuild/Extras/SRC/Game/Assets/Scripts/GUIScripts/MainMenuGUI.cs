﻿using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour
{

    public static Rect background;
    public static Texture2D backgroundImage = Resources.Load("GUI Assets/spacebackground") as Texture2D;
    static float x;

    public static void Update()
    {
        background = new Rect(Screen.width / 7, Screen.height / 12, Screen.width * 5 / 7, Screen.height * 6 / 7);
        x = Mathf.PingPong(Time.time * 100, 16384);
    }
    
    public static void OnGUI()
    {
        GUI.skin = Resources.Load("GUI Assets/MainMenuSkin") as GUISkin;
        GUI.DrawTexture(new Rect(-x, 0, 16384 + Screen.width, 768), backgroundImage);
        GUI.BeginGroup(background);
        GUI.Box(new Rect(0, 0, background.width, background.height), "");


        
        if (GUI.Button(new Rect(50, background.height * 1 / 6, background.width - 100, background.width * 1 / 10), "New Game"))
        {
            if (GenericCommands.checkForFiles(".xml", "Scenarios"))
            {
                NewGameParser.startNewGame();
                GUISelector.FileType = ".xml";
                FileBrowserGUI.OnGUIMain();
                GUISelector.Gui = 4;
            }
            else
            {
                GUISelector.message = "There are not any scenario files to load, please use the import function to add in a scenario";
                GUISelector.Gui = 3;
            }
        }
        
        if (GUI.Button(new Rect(50, background.height * 2 / 6, background.width - 100, background.width * 1 / 10), "Load Game"))
        {
            if (GenericCommands.checkForFiles(".dat", "SaveGames"))
            {


                GUISelector.FileType = ".dat";
                FileBrowserGUI.OnGUIMain();
                GUISelector.Gui = 4;
               

            }
            else
            {
                GUISelector.message = "There are not any save files to load";
                GUISelector.Gui = 3;
            }
        }
        
        if (GUI.Button(new Rect(50, background.height * 3 / 6, background.width - 100, background.width * 1 / 10), "Options"))
        {
            GUISelector.Gui = 1;
        }
        
        if (GUI.Button(new Rect(50, background.height * 4 / 6, background.width - 100, background.width * 1 / 10), "Import"))
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                System.Diagnostics.Process.Start(@Application.persistentDataPath + "/Scenarios/");
                GUISelector.message = "Follow the readme in the Scenarios Folder";
                GUISelector.Gui = 3; 
            }
            else
            {
                GUISelector.message = "Your Platform does not support the import command at this time";
                GUISelector.Gui = 3; 
            }
        }
        
        if (GUI.Button(new Rect(50, background.height * 5 / 6, background.width - 100, background.width * 1 / 10), "Quit"))
        {
            GenericCommands.quit();
        }
        
        GUI.EndGroup();


        GUILayout.BeginArea(new Rect(Screen.width / 7, Screen.height / 9, background.width, background.height));
        GUILayout.Label(GlobalSettings.GameName);
        GUILayout.EndArea();
    }
}
