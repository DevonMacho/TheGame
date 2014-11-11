using UnityEngine;
using System.Collections;

public class RollTesting : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
			
		}
	
		// Update is called once per frame
		int runs = 0;

		void Update ()
		{
				int highestRoll = Mathf.Clamp ((int)Mathf.Ceil (Random.Range (100, 400) / 100), 1, 3);
				int highRoll = Mathf.Clamp ((int)Mathf.Ceil (Random.Range (100, 300) / 100), 1, 2);
				int shitRoll = - Mathf.Clamp ((int)Mathf.Ceil (Random.Range (100, 300) / 100), 1, 2);
				int mehRoll1 = Mathf.Clamp ((int)Mathf.Ceil (Random.Range (0, 110) / 100), 0, 1);
				int mehRoll2 = Mathf.Clamp ((int)Mathf.Ceil (Random.Range (0, 110) / 100), 0, 1);
				runs ++;
				if (highestRoll == 3 && highRoll == 2 && shitRoll == -2 && mehRoll1 == 1 && mehRoll2 == 1) {			
						Debug.LogError ("Extreme Stats Reached");
				} else {
						Debug.Log ("Run #" + runs);
				}
				/*
				Debug.Log ("Highest) " + highestRoll);
				Debug.Log ("High) " + highRoll);
				Debug.Log ("Shit) " + shitRoll);
				Debug.Log ("Meh 1) " + mehRoll1);
				Debug.Log ("Meh 2) " + mehRoll2);
				*/
		}
}
