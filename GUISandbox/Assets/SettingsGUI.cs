using UnityEngine;
using System.Collections;

public class SettingsGUI : MonoBehaviour
{
		
		static Rect background;
		static Rect videoOptions;
		static Rect soundOptions;
		static Rect resolutions;
		static Rect fullScreenOptions;
		static bool fullscreen;
		static float gameAudio; //fix so that it loads from global
		static float musicAudio; //fix so that it loads from global

		public static void Start ()
		{
				if (!Screen.fullScreen) {
						fullscreen = false;
				} else {
						fullscreen = true;
				}
		}

		public static void Update ()
		{
				background = new Rect (Screen.width / 7, Screen.height / 12, Screen.width * 5 / 7, Screen.height * 5 / 6);
				
				videoOptions = new Rect (background.width / 12, background.height / 20, background.width * 5 / 6, background.height * 1 / 3);
				soundOptions = new Rect (background.width / 12, background.height * 2 / 5, background.width * 5 / 6, background.height * 1 / 6);
				resolutions = new Rect (videoOptions.x / 2, videoOptions.y, videoOptions.width * 9 / 10, videoOptions.height * 2 / 5);
				fullScreenOptions = new Rect (videoOptions.x / 2, videoOptions.y * 16 / 4, videoOptions.width * 9 / 10, videoOptions.height * 1 / 5);
				



				if (fullscreen) {
						Screen.fullScreen = true;
				} else if (!fullscreen) {
						Screen.fullScreen = false;

				}
		}

		public static void OnGUI ()
		{
				GUI.BeginGroup (background);
				GUI.Box (new Rect (0, 0, background.width, background.height), "Settings");
				GUI.Box (new Rect (videoOptions), "Video Options");
				GUI.Box (soundOptions, "Sound Options");
				#region Video Options		
				GUI.BeginGroup (videoOptions);
				GUI.Box (resolutions, "Resolutions");
				GUILayout.BeginArea (new Rect (resolutions.x, resolutions.y * 2, resolutions.width, resolutions.height * 2));
				GUILayout.BeginHorizontal ("box");
				
				if (GUILayout.Button ("800 x 600")) {
						Screen.SetResolution (800, 600, fullscreen);
				}
				if (GUILayout.Button ("1024 x 768")) {
						Screen.SetResolution (1024, 768, fullscreen);
				}

				if (GUILayout.Button ("1152 x 864")) {
						Screen.SetResolution (1152, 864, fullscreen);
				}
				
				GUILayout.EndHorizontal ();
				GUILayout.EndArea ();
				GUI.EndGroup ();
				GUI.BeginGroup (videoOptions);
				GUI.Box (fullScreenOptions, "");
				fullscreen = GUI.Toggle (new Rect (fullScreenOptions.x * 11 / 10, fullScreenOptions.y * 11 / 10, fullScreenOptions.width, fullScreenOptions.height), fullscreen, "  Full Screen");
				GUI.EndGroup ();				
				#endregion

				#region Sound Settings
				GUI.BeginGroup (soundOptions);
				GUILayout.BeginArea (new Rect (soundOptions.width * 1 / 2, soundOptions.height / 3, soundOptions.width / 3, soundOptions.height));	
				GUILayout.BeginVertical ();		
				gameAudio = GUILayout.HorizontalSlider (gameAudio, 0, 1);
				GUILayout.Space (soundOptions.height / 5);
				musicAudio = GUILayout.HorizontalSlider (musicAudio, 0, 1);
				GUILayout.EndVertical ();
				GUILayout.EndArea ();

				GUILayout.BeginArea (new Rect (soundOptions.width * 1 / 6, soundOptions.height * 9 / 30, soundOptions.width / 4, soundOptions.height));	
				GUILayout.BeginVertical ();		
				GUILayout.Box ("Game Volume");
				GUILayout.Space (soundOptions.height / 20);
				GUILayout.Box ("Music Volume");
				GUILayout.EndVertical ();
				GUILayout.EndArea ();

				GUI.EndGroup ();
				#endregion
				#region buttons
				if (GUI.Button (new Rect (background.width / 2 - (background.width / 3) / 2, background.height * 5 / 8, background.width / 3, background.height / 12), "Save")) {
						Debug.Log ("Saving Configuration");
				}
				if (GUI.Button (new Rect (background.width / 2 - (background.width / 3) / 2, background.height * 6 / 8, background.width / 3, background.height / 12), "Configure HUD")) {
						GUISelector.Gui = 2;
				}
				if (GUI.Button (new Rect (background.width / 2 - (background.width / 3) / 2, background.height * 7 / 8, background.width / 3, background.height / 12), "Back")) {
						GUISelector.Gui = 0;
				}
				#endregion

				GUI.EndGroup ();
		}
}
