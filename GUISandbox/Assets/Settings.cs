using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour
{
		
		private Rect background;
		private Rect videoOptions;
		private Rect soundOptions;
		private Rect resolutions;
		private Rect fullScreenOptions;
		private bool fullscreen = false;
		private float gameAudio; //fix so that it loads from global
		private float musicAudio; //fix so that it loads from global

		void Start ()
		{
				if (Screen.fullScreen == false) {
						fullscreen = false;
				} else {
						fullscreen = true;
				}
		}

		void Update ()
		{
				background = new Rect (Screen.width / 7, Screen.height / 12, Screen.width * 5 / 7, Screen.height * 5 / 6);
				
				videoOptions = new Rect (background.width / 12, background.height / 20, background.width * 5 / 6, background.height * 1 / 3);
				soundOptions = new Rect (background.width / 12, background.height * 2 / 5, background.width * 5 / 6, background.height * 1 / 6);
				resolutions = new Rect (videoOptions.x / 2, videoOptions.y, videoOptions.width * 9 / 10, videoOptions.height * 2 / 5);
				fullScreenOptions = new Rect (videoOptions.x / 2, videoOptions.y * 16 / 4, videoOptions.width * 9 / 10, videoOptions.height * 1 / 5);
				



				if (fullscreen == true) {
						Screen.fullScreen = true;
				} else if (fullscreen == false) {
						Screen.fullScreen = false;

				}
		}

		void OnGUI ()
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

				GUILayout.BeginArea (new Rect (soundOptions.width * 1 / 6, soundOptions.height *  9 / 30, soundOptions.width / 4, soundOptions.height));	
				GUILayout.BeginVertical ();		
				GUILayout.Box ("Game Volume");
				GUILayout.Space (soundOptions.height / 20);
				GUILayout.Box ("Music Volume");
				GUILayout.EndVertical ();
				GUILayout.EndArea ();

				GUI.EndGroup ();
				#endregion
				GUI.EndGroup ();
		}
}
