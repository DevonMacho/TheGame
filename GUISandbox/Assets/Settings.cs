using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour
{
		
		private Rect background;
		private Rect videoOptions;
		private Rect soundOptions;
		private Rect resolutions;

		void Start ()
		{
				
		}

		void Update ()
		{
				background = new Rect (Screen.width / 7, Screen.height / 12, Screen.width * 5 / 7, Screen.height * 5 / 6);
				videoOptions = new Rect (background.width / 12, background.height / 20, background.width * 5 / 6, background.height * 1 / 3);
				soundOptions = new Rect (background.width / 12, background.height * 2 / 5, background.width * 5 / 6, background.height * 1 / 3);
				resolutions = new Rect (videoOptions.x / 2, videoOptions.y, videoOptions.width * 9 / 10, videoOptions.height * 2 / 5);
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
				GUILayout.Button ("640 x 480");
				GUILayout.Button ("800 x 600");
				GUILayout.Button ("1024 x 768");
				GUILayout.Button ("1440 x 1080");
				GUILayout.EndHorizontal ();
				GUILayout.EndArea ();
				GUI.EndGroup ();
				#endregion
				GUI.EndGroup ();
		}
}
