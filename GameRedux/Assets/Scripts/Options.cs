using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour {
	public GameObject ResPanel;
	public GameObject ResText;
	public GameObject MSlider;
	public GameObject ESlider;
	public float MusicVolume;
	public float EffectVolume;
	float tempEV;
	float tempMV;
	// Use this for initialization
	public void Awake ()
	{
		//load every time options is clicked
		//load volume from file
		//load resolution from file
		Debug.Log( "Options is awake");
		ResText.GetComponent<Text>().text = Screen.width + "x" + Screen.height;

		//set Volume Sliders
		//Debug.Log(Screen.width + "x" + Screen.height);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
		public void ToggleRes()
	{
		ResPanel.SetActive(!ResPanel.activeSelf);
	}
	public void setTempEV(float tmp)
	{
		tempEV = tmp;
	}
	public void setTempMV(float tmp)
	{
		tempMV = tmp;
	}
}
