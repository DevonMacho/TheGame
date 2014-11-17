using UnityEngine;
using System.Collections;

public class GUISelector : MonoBehaviour
{
    public static int Gui = 0;
    public static int PreviousGui;
    public static string message = "";
    public static string FileType = "";
    public static string FilePath = "";
    public static GUISkin skin; //= Resources.Load("GUI Assets/generic") as GUISkin;
    //public static GUISkin skin2;// = Resources.Load("GUI Assets/MainMenuSkin") as GUISkin;
    void Awake()
    {

        GlobalSettings.Start();
        WorldData.generateScenarios();
        SettingsGUI.Start();
        FileBrowserGUI.Start();
        GUI_Terminal.Start();
        ConfigureHUD_GUI.Start();
    }
    void Update()
    {

        GUI_Terminal.Update();       
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
                    ParserSelect.parserSelect = 4;
                    PreviousGui = 5;
                    //"<<Starting new game, input anything to continue>>\n";
                    Gui = 3;
                }
                else if (FileType == ".dat")
                {
                    message = "File Selected!!!\nloading game";
                    ParserSelect.parserSelect = 1;
                    PreviousGui = 5;
                    Gui = 3;
                    GameData.GameInformation testNull = GameData.GameInformation.deserialize(GUISelector.FilePath);
                    if (testNull != null)
                    {
                        WorldData.gameData = testNull;
                        GUI_Terminal.consoleLog = "<<Resuming Game>>\n";
                    }
                    else
                    {
                        GUISelector.message = "There was an error loading the save file";
                        GUISelector.Gui = 3;
                    }
                }
            }
        }
        if (Gui != 3 && Gui != 4)
        {
            PreviousGui = Gui;
        }
        if (Gui == 5)
        {
            GUI_Terminal.OnGUI();
        }
    }

    
}
