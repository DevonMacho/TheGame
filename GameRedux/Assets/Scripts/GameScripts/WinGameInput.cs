using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
public class WinGameInput : MonoBehaviour
{

	public GameObject OutputText;
	public GameObject ScrollBar;
	public GameObject cinematicButton;

	public Image fore1;
	public Image fore2;
	//public Sprite testImage;


	bool _acceptInput;
	bool _cinematic;
	int _cinematicStage = 0;
	string[] _currentCinematic;
	
	// Use this for initialization
	void Awake()
	{

		cinematicButton.GetComponent<Button>().onClick.RemoveAllListeners();
		cinematicButton.GetComponent<Button>().onClick.AddListener(delegate
		{
			Submit();
		});
		
			string[] endCine = 
		{
			"You have defeated kraymoar the destroyer of Uranus!", "People of the universes will praise your efforts for many years to come.", "Now, how do you get home from here..."
		};
			OutputText.GetComponent<Text>().text = startCinematic(endCine) + "\n";
			StartCoroutine(fadeTexture(new Sprite(),GameMaster.GM.backgrounds[GameMaster.GM.Data.Node]));
			StartCoroutine("topScroll");
		}
		
	
	void Update()
	{
		
		if(_acceptInput && !_cinematic)
		{
			if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				
				Submit();
				
			}

		}
	}
	
	public void Submit()
	{
		if(_cinematic)
		{
			
			string checkValid = cinematic(_cinematicStage, _currentCinematic);
			if(checkValid != null)
			{
				
				OutputText.GetComponent<Text>().text = OutputText.GetComponent<Text>().text + checkValid + "\n";
				StartCoroutine("bottomScroll");
				_cinematicStage++;
				return;
			}
			else
			{
				
				stopCinematic();
				return;
				
			}
		}
	}
	
	public void AcceptInput(bool value)
	{
		_acceptInput = value;
	}
	
	IEnumerator bottomScroll()
	{
		yield return new WaitForSeconds(.1f);
		ScrollBar.GetComponent<Scrollbar>().value = 0.0000000000000f;
	}
	
	IEnumerator topScroll()
	{
		yield return new WaitForSeconds(.1f);
		ScrollBar.GetComponent<Scrollbar>().value = 1.0000000000000f;
	}
	
	
	public string startCinematic(string[] cine)
	{
		_cinematic = true;
		_currentCinematic = cine;
		cinematicButton.SetActive(true);
		_cinematicStage = 1;
		cinematicButton.GetComponent<Button>().Select();
		return cinematic(0, cine);
	}
	
	void stopCinematic()
	{
		_cinematic = false;
		_currentCinematic = null;
		cinematicButton.SetActive(false);
		//roll credits
	}
	
	string cinematic(int stage, string[] content)
	{
		
		if(content == null || stage < 0 || stage >= content.Length)
		{
			return null;
		}
		else
		{
			return content [stage];
		}
	}
	
	public IEnumerator fadeTexture(Sprite bottom, Sprite top)
	{
		//top fades into bottom
		//topObject.mainTexture = top;
		//bottomObject.mainTexture = bottom;
		fore1.sprite = bottom;
		fore2.sprite = top;
		for(int i = 255; i >= 0; i--)
		{
			fore2.color = new Color(fore2.color.r, fore2.color.g,fore2.color.b,(float)(i/255.0f));
			yield return new WaitForSeconds(.0f);
			if(i%2 == 0 && i > 3)
			{
				i -=3;
				
			}
		}
		fore2.color = new Color(1.0f,1.0f,1.0f,0.0f);
		
	}
}
