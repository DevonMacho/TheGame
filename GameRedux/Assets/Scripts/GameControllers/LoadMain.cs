using UnityEngine;
using System.Collections;

public class LoadMain : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("Load");
	}
	
	// Update is called once per frame
	IEnumerator Load()
	{
		yield return new WaitForSeconds(5.0f);
		Application.LoadLevel("MainMenu");
	}
}
