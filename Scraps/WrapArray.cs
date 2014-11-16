using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WrapArray : MonoBehaviour
{

		
		
		// Use this for initialization
		void Start ()
		{
				int[] intBefore = {0,1,2,3,4,5,6,7};
				string before = "[";
				foreach (int a in intBefore) {
						before = before + " " + a;
				}
				before = before + " ]";
				Debug.Log ("Before:\t" + before);
				
				
				
				for (int i = -10; i < 10; i++) {
						int[] intAfter = arrayShift (i, intBefore);
						string after = "[";
						foreach (int a in intAfter) {
								after = after + " " + a;
						}
						after = after + " ]";
						Debug.Log ("Input Value: " + i + "\t" + "Shift Value: " + i % intBefore.Length + "\t" + "After:\t" + after);
				}
				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		int[] arrayShift (int value, int[] shift)
		{
				int v2 = value % shift.Length;
				List<int> temp = new List<int> ();
				if (v2 > 0) {
				
				
						for (int i = v2; i > 0; i--) {
								temp.Add (shift [shift.Length - i]);
						}
				
						for (int i = 0; i < shift.Length; i++) {
								if (i + v2 < shift.Length) {
										temp.Add (shift [i]);
								}
								
								
								
						}
						return temp.ToArray ();
						
						
				
				} else if (v2 < 0) {
						
			
						for (int i = -v2; i < shift.Length; i++) {
								if (i + v2 < shift.Length) {
										temp.Add (shift [i]);
								}
							
						}
						for (int i = 0; i < -v2; i++) {
								temp.Add (shift [i]);
				
						}
						return temp.ToArray ();
				}
				return shift;
		}
}