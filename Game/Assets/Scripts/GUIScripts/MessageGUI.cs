using UnityEngine;
using System.Collections;

public class MessageGUI : MonoBehaviour
{


    void Start()
    {
    
    }

    void Update()
    {
    
    }

    public static void OnGUI(string message)
    {
        GUI.skin = Resources.Load("GUI Assets/generic") as GUISkin;
        Rect box = new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 4);
        GUI.Box(box, "Message");
        GUI.Label(new Rect(box.x * 5 / 4, box.y * 9 / 8, box.width - box.width / 4, box.height - box.height / 3), message);


        if (GUI.Button(new Rect(box.width - box.width / 8, box.height * 55 / 20 - box.height, box.width / 4, box.height / 8), "Return"))
        {

            GUISelector.message = "";
            GUISelector.Gui = GUISelector.PreviousGui;

        }
    }
}
