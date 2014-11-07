using UnityEngine;
using System.Collections;

public class GUISelector : MonoBehaviour
{
    public static int Gui = 0;
    public static int PreviousGui;
    public static string message = "";

    void Awake()
    {
        WorldData.generateScenarios();
        SettingsGUI.Start();
    }

    void Update()
    {
                
        SettingsGUI.Update();
        MainMenuGUI.Update();
        ConfigureHUD_GUI.Update();

    }

    void OnGUI()
    {
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
        if (Gui != 3)
        {
            PreviousGui = Gui;
        }
    }
    
}
