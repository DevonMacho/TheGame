using UnityEngine;
using System.Collections;
    
public class FileBrowserGUI : MonoBehaviour
{
        
    protected string m_textPath;
    protected FileBrowser m_fileBrowser;
    [SerializeField]
    protected Texture2D
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
    public GUISkin skin;
    protected void OnGUIMain()
    {
        GUI.skin = skin;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Scenario File", GUILayout.Width(100));
        GUILayout.FlexibleSpace();
        GUILayout.Label(m_textPath ?? "none selected");
        if (GUILayout.Button("...", GUILayout.ExpandWidth(false)))
        {

            m_fileBrowser = new FileBrowser(
                new Rect(0, 100, Screen.width, Screen.height * .5f),
                    "Choose Scenario File",
                    FileSelectedCallback
            );
            m_fileBrowser.SelectionPattern = "*.xml";
            m_fileBrowser.DirectoryImage = m_directoryImage;
            m_fileBrowser.FileImage = m_fileImage;

        }
        GUILayout.EndHorizontal();
    }
        
    protected void FileSelectedCallback(string path)
    {
        m_fileBrowser = null;
        m_textPath = path;
    }
}
