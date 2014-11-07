using UnityEngine;
using System.Collections;
    
public class FileBrowserGUI : MonoBehaviour
{
        
    protected static string m_textPath;
    protected static FileBrowser m_fileBrowser;

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
        m_directoryImage = Resources.Load("/GUI Assets/Gnome-folder") as Texture2D,
        m_fileImage = Resources.Load("/GUI Assets/Gnome-edit-select-all") as Texture2D;
        
    protected void OnGUI()
    {
        GUI.skin = skin;
        if (m_fileBrowser != null)
        {
            m_fileBrowser.OnGUI();
        }
        else
        {
           // OnGUIMain(GUISelector.FileType);
        }
    }
    public static GUISkin skin;

    public static void OnGUIMain(string FileType)
    {
        GUI.skin = skin;

            m_fileBrowser = new FileBrowser(
                new Rect(0, 100, Screen.width, Screen.height * .5f),
                    "Choose Scenario File",
                    FileSelectedCallback
            );
            m_fileBrowser.SelectionPattern = "*"+ FileType;
            m_fileBrowser.DirectoryImage = m_directoryImage;
            m_fileBrowser.FileImage = m_fileImage;
    }
        
    static protected void FileSelectedCallback(string path)
    {
        m_fileBrowser = null;
        m_textPath = path;
    }
}
