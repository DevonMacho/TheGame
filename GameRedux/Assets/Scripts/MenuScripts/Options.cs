using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour {
	public GameObject ResPanel;
	public GameObject ResText;
	public GameObject MSlider;
	public GameObject ESlider;
	int width;
	int height;

	void Update () 
	{
	
	}

	public void SetPrefDefaults()
	{
		PlayerPrefs.SetFloat("MusicVolume",1.0f);
		PlayerPrefs.SetFloat("EffectVolume",1.0f);
		PlayerPrefs.SetInt("ResWidth",1024);
		PlayerPrefs.SetInt("ResHeight",768);
		PlayerPrefs.Save();
	}
	public bool CheckPrefs()
	{
		if (!PlayerPrefs.HasKey("MusicVolume") || !PlayerPrefs.HasKey("EffectVolume") || !PlayerPrefs.HasKey("ResWidth")|| !PlayerPrefs.HasKey("ResHeight"))
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	public void ToggleRes()
	{
		ResPanel.SetActive(!ResPanel.activeSelf);
	}
	public void LoadPrefs()
	{
		Screen.SetResolution(PlayerPrefs.GetInt("ResWidth"),PlayerPrefs.GetInt("ResHeight"),false);
		ResText.GetComponent<Text>().text = PlayerPrefs.GetInt("ResWidth")+ "x" + PlayerPrefs.GetInt("ResHeight");
		MSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
		ESlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("EffectVolume");
	}
	public void SavePrefs()
	{
		PlayerPrefs.SetFloat("MusicVolume", MSlider.GetComponent<Slider>().value);
		PlayerPrefs.SetFloat("EffectVolume",ESlider.GetComponent<Slider>().value);
		PlayerPrefs.SetInt("ResWidth",Screen.width);
		PlayerPrefs.SetInt("ResHeight",Screen.height);
		PlayerPrefs.Save();
	}
	public void SetHeight(int temp)
	{
		height = temp;
	}
	public void SetWidth(int temp)
	{
		width = temp;
	}
	public void SetResolution()
	{
		Screen.SetResolution(width,height,false);
		height = 0;
		width = 0;
	}
}
