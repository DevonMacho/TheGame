using UnityEngine;
using System.Collections;

public class GUISelector : MonoBehaviour
{
    public static int Gui = 0;
    public static int PreviousGui;
    public static string message = "";
    public static string FileType = "";
    public static string FilePath = "";
    public static GUISkin skin = Resources.Load("GUI Assets/fore") as GUISkin;

    void Awake()
    {

        WorldData.generateScenarios();
        SettingsGUI.Start();
        FileBrowserGUI.Start();
    }

    void Update()
    {
                
        SettingsGUI.Update();
        MainMenuGUI.Update();
        ConfigureHUD_GUI.Update();

    }

    void OnGUI()
    {
        GUI.skin = skin;
        if (Gui == 0)
        {
            MainMenuGUI.OnGUI();
        }
        if (Gui == 1)
        {
            SettingsGUI.OnGUI();
        }
        if (Gui == 2)
        {
            ConfigureHUD_GUI.OnGUI();
        }
        if (Gui == 3)
        {

            MessageGUI.OnGUI(message);
        }
        if (Gui == 4)
        {
            if (FileBrowserGUI.M_fileBrowser != null)
            {
                FileBrowserGUI.M_fileBrowser.OnGUI();
            }
            else
            {
                if (FileType == "")
                {
                    message = "Error loading file";
                    Gui = 3;
                }
                else if (FileType == ".xml")
                {
                    message = "File Selected!!!\nstarting new game";
                    //start game
                    Gui = 3;
                }
                else if (FileType == ".save")
                {
                    message = "File Selected!!!\nloading game";
                    //Load game
                    Gui = 3;
                }
            }
        }
        if (Gui != 3 && Gui != 4)
        {
            PreviousGui = Gui;
        }
    }
    
}
