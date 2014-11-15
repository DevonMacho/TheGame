using UnityEngine;

public class ConfigureHUD_GUI : MonoBehaviour
{
    public static Rect charStats = new Rect(1, Screen.height * 580 / 669, Screen.width / 2, Screen.height * 99 / 200);
    public static Rect miniMap = new Rect(1, 1, Screen.width / 6, Screen.width / 6);
    public static Texture clb = Resources.Load("GUI Assets/CommandLineBackground") as Texture;
    public static float miniX;
    public  static float miniY;
    public  static float statX;
    public  static float statY;

    public static void DragWindow(int windowID)
    {
        GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }

    public static void Start()
    {
        miniX = miniMap.x = GlobalSettings.MinimapX; //WTF these lined up perfectly...
        miniY = miniMap.y = GlobalSettings.MinimapY;
        statX = charStats.x = GlobalSettings.StatsX;
        statY = charStats.y = GlobalSettings.StatsY;
    }

    public static void Update()
    {
        charStats.height = Screen.height * 99 / 200;
        charStats.width = Screen.width / 4;
        miniMap.height = Screen.height / 6;
        miniMap.width = Screen.height / 6;
        miniX = miniMap.x;
        miniY = miniMap.y;
        statX = charStats.x;
        statY = charStats.y;
    }

    public static void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, Screen.height - (Screen.height * 256 / 768), Screen.width, Screen.height * 256 / 768), clb, ScaleMode.StretchToFill, true, 10.0F);
        charStats = GUI.Window(0, charStats, DragWindow, "Char Stats");
        miniMap = GUI.Window(1, miniMap, DragWindow, "MiniMap");
        if (GUI.Button(new Rect(Screen.width - Screen.width / 4, Screen.height - 3 * (Screen.height / 10), Screen.width / 5, Screen.height / 15), "save"))
        {
            GlobalSettings.saveHUD(miniX, miniY, statX, statY);
            GUISelector.message = "HUD Saved!";
            GUISelector.Gui = 3;
        }
        if (GUI.Button(new Rect(Screen.width - Screen.width / 4, Screen.height - (Screen.height / 10), Screen.width / 5, Screen.height / 15), "back"))
        {
            GUISelector.Gui = 1;
        }
        if (GUI.Button(new Rect(Screen.width - Screen.width / 4, Screen.height - 2 * (Screen.height / 10), Screen.width / 5, Screen.height / 15), "reset"))
        {
            miniMap.x = 1;
            miniMap.y = 1;
            charStats.x = 1;
            charStats.y = Screen.height * 580 / 669;
        }
        #region Border Collision
        if (miniMap.x > Screen.width - miniMap.width)
        { 
            miniMap.x = Screen.width - miniMap.width - 1;
        }
        if (miniMap.x < 0)
        {
            miniMap.x = 0 + 1;
        }
        if (miniMap.y > 2 * (Screen.height / 3) - miniMap.height)
        {
            miniMap.y = 2 * (Screen.height / 3) - miniMap.height - 1;
        }
        if (miniMap.y < 0)
        {
            miniMap.y = 0 + 1;
        }
        if (charStats.x > Screen.width - charStats.width)
        {
            charStats.x = Screen.width - charStats.width - 1;
        }
        if (charStats.x < 0)
        {
            charStats.x = 0 + 1;
        }
        if (charStats.y > 2 * (Screen.height / 3) - charStats.height)
        {
            charStats.y = 2 * (Screen.height / 3) - charStats.height - 1;
        }
        if (charStats.y < 0)
        {
            charStats.y = 0 + 1;
        }
        #endregion

    }


}
