using UnityEngine;
using System.Collections;
    
public class FileBrowserGUI : MonoBehaviour
{
        
    protected static string m_textPath;
    protected static FileBrowser m_fileBrowser;

    public static void Start()
    {
        m_directoryImage = Resources.Load("GUI Assets/Gnome-folder") as Texture2D;
        m_fileImage = Resources.Load("GUI Assets/Gnome-edit-select-all") as Texture2D;
    }

    public static FileBrowser M_fileBrowser
    {
        get
        {
            return m_fileBrowser;
        }
        set
        {
            m_fileBrowser = value;
        }
    }

    [SerializeField]
    static Texture2D
        m_directoryImage,
        m_fileImage;
        
    protected void OnGUI()
    {
        GUI.skin = skin;
        if (m_fileBrowser != null)
        {
            m_fileBrowser.OnGUI();
        }
        else
        {
            OnGUIMain();
        }
    }

    public static GUISkin skin;

    public static void OnGUIMain()
    {
        GUI.skin = Resources.Load("GUI Assets/fore") as GUISkin;

        m_fileBrowser = new FileBrowser(
                new Rect(0, 100, Screen.width, Screen.height * .5f),
                    GUISelector.message,
                    FileSelectedCallback
        );
        m_fileBrowser.SelectionPattern = "*" + GUISelector.FileType;
        m_fileBrowser.DirectoryImage = m_directoryImage;
        m_fileBrowser.FileImage = m_fileImage;
        if (GUISelector.FileType == ".xml")
        {

            m_fileBrowser.CurrentDirectory = Application.persistentDataPath + "/Scenarios";
        }
        if (GUISelector.FileType == ".save")
        {

            m_fileBrowser.CurrentDirectory = Application.persistentDataPath + "/SaveGames";
        }

    }
        
    static protected void FileSelectedCallback(string path)
    {
        m_fileBrowser = null;
        m_textPath = path;
    }
}
