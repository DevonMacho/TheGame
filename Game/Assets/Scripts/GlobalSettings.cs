using UnityEngine;
using System.IO;
using System.Collections;

public class GlobalSettings : MonoBehaviour
{
    static float music;
    static float gameAudio;
    static float minimapX;
    static float minimapY;
    static float statsX;
    static float statsY;
    static string gameName;

    public static float Music
    {
        get
        {
            return music;
        }
        set
        {
            music = value;
        }
    }

    public static float GameAudio
    {
        get
        {
            return gameAudio;
        }
        set
        {
            gameAudio = value;
        }
    }

    public static float MinimapX
    {
        get
        {
            return minimapX;
        }
        set
        {
            minimapX = value;
        }
    }

    public static float MinimapY
    {
        get
        {
            return minimapY;
        }
    }

    public static float StatsX
    {
        get
        {
            return statsX;
        }
        set
        {
            statsX = value;
        }
    }

    public static float StatsY
    {
        get
        {
            return statsY;
        }
        set
        {
            statsY = value;
        }
    }

    public static string GameName
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

    public static void Start()
    {
        if (!PlayerPrefs.HasKey("music") || 
            !PlayerPrefs.HasKey("gameAudio") || 
            !PlayerPrefs.HasKey("gameName") || 
            !PlayerPrefs.HasKey("miniMapPositionX") || 
            !PlayerPrefs.HasKey("miniMapPositionY") ||
            !PlayerPrefs.HasKey("statsX") || 
            !PlayerPrefs.HasKey("statsY"))
        {
            settingDefaults();
        }
        else
        {
            reloadSettings();
        }
    }
    public static void reloadSettings()
    {
        minimapX = PlayerPrefs.GetFloat("miniMapPositionX"); 
        minimapY = PlayerPrefs.GetFloat("miniMapPositionY");
        statsX = PlayerPrefs.GetFloat("statsX");
        statsY = PlayerPrefs.GetFloat("statsY");
        music = PlayerPrefs.GetFloat("music");
        gameAudio = PlayerPrefs.GetFloat("gameAudio");
        gameName = PlayerPrefs.GetString("gameName");
    }
    public static void settingDefaults()
    {
        PlayerPrefs.SetFloat("music", 1.0f);
        PlayerPrefs.SetFloat("gameAudio", 1.0f);
        PlayerPrefs.SetFloat("miniMapPositionX", 1.0f);
        PlayerPrefs.SetFloat("miniMapPositionY", 1.0f);
        PlayerPrefs.SetFloat("statsX", 1.0f);
        PlayerPrefs.SetFloat("statsY", 131f);
        //saveHUD(ConfigureHUD_GUI.miniX, ConfigureHUD_GUI.miniY, ConfigureHUD_GUI.statX, ConfigureHUD_GUI.statY);
        PlayerPrefs.SetString("gameName", "Claw Of Kraymoar");
        PlayerPrefs.Save();
        reloadSettings();

    }

    public static void setSettings(float music, float gameAudio, string gameName)
    {
        PlayerPrefs.SetFloat("music", music);
        PlayerPrefs.SetFloat("gameAudio", gameAudio);
        PlayerPrefs.SetString("gameName", gameName);
        PlayerPrefs.Save();
        reloadSettings();
    }

    public static void saveHUD(float minimapX, float minimapY, float statsX, float statsY)
    {
        PlayerPrefs.SetFloat("miniMapPositionX", minimapX);
        PlayerPrefs.SetFloat("miniMapPositionY", minimapY);
        PlayerPrefs.SetFloat("statsX", statsX);
        PlayerPrefs.SetFloat("statsY", statsY);
        PlayerPrefs.Save();
        reloadSettings();
    }

    void Update()
    {
    
    }
}
