using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(mainMenu());
	}
	
	// Update is called once per frame
	IEnumerator mainMenu()
	{
		yield return new WaitForSeconds(70.0f);
		Application.LoadLevel("MainMenu");
	}
}
