using UnityEngine;
using System.Collections;

public class GUISelector : MonoBehaviour
{
		public static int Gui = 0;
		
		void Start ()
		{
				SettingsGUI.Start ();
		}

		void Update ()
		{
				
				SettingsGUI.Update ();
				MainMenuGUI.Update ();
				ConfigureHUD_GUI.Update ();

		}

		void OnGUI ()
		{
				if (Gui == 0) {
						MainMenuGUI.OnGUI ();
				}
				if (Gui == 1) {
						SettingsGUI.OnGUI ();
				}
				if (Gui == 2) {
						ConfigureHUD_GUI.OnGUI ();
				}
		}

}
