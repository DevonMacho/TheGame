using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class NewGameInput : MonoBehaviour {
	
	public GameObject SubmitButton;
	public GameObject InputField;
	public GameObject InputText;
	public GameObject OutputText;
	public GameObject ScrollBar;
	//bool _acceptInput;


	void Start()
	{
		InputField.GetComponent<InputField>().onEndEdit.RemoveAllListeners();
		InputField.GetComponent<InputField>().onEndEdit.AddListener(delegate 
		{
			//Submit();
		});

		SubmitButton.GetComponent<Button>().onClick.RemoveAllListeners();
		SubmitButton.GetComponent<Button>().onClick.AddListener(delegate 
		{
			Submit();

		});
	}
	public void Submit()
	{
		ScrollBar.GetComponent<Scrollbar>().value = 0.0000000000000f;
		string input = InputText.GetComponent<Text>().text;
		Debug.Log(input);
		//send input text to the parser
		//get response from parser and output it to OutputText
	}
	/*
	public void AcceptInput(bool value)
	{
		_acceptInput = value;
	}
	*/
}
