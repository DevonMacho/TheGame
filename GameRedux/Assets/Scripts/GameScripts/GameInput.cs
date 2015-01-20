using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class GameInput : MonoBehaviour
{
	public GameObject SubmitButton;
	public GameObject InputField;
	public GameObject InputText;
	public GameObject OutputText;
	public GameObject ScrollBar;
	public GameObject cinematicButton;
	public GameObject inputHolder;
	bool _acceptInput;
	bool _cinematic;
	int _cinematicStage = 0;
	string[] _currentCinematic;
	// Use this for initialization
	void Awake()
	{
		#if UNITY_EDITOR
		InputField.GetComponent<InputField>().contentType = UnityEngine.UI.InputField.ContentType.Name;
		#else
		InputField.GetComponent<InputField>().contentType = UnityEngine.UI.InputField.ContentType.Standard;
		#endif
		SubmitButton.GetComponent<Button>().onClick.RemoveAllListeners(); //clears any actions
		SubmitButton.GetComponent<Button>().onClick.AddListener(delegate
		{
			Submit();
		}); // sets the button's action to submit

		cinematicButton.GetComponent<Button>().onClick.RemoveAllListeners();
		cinematicButton.GetComponent<Button>().onClick.AddListener(delegate
		{
			Submit();
		});

		InputField.GetComponent<InputField>().onEndEdit.RemoveAllListeners();

		InputField.GetComponent<InputField>().characterLimit = 60;

		 string[] introCine = 
		{
			"The surrounding white light fades as you wake up in the middle of a clearing",
			"You panic for a moment, being surrounded by several charred cats, but then you remember;",
			"You were a researcher at a wormhole research facility on the planet Uranus.",
			"Right before you a small lab accident resulting in what could only be described as quantum displacement through a wormhole,",
			"you were conducting several wormhole experiments involving throwing cats into generated wormholes to see if they would arrive live or dead depending on the spin state of subatomic particles.",
			"It looks like all of them have been charred by the wormhole... So much for quantum mechanics.",
			"As you are staring at the pile of dead cats, you remember something.",
			"Something big...",
			"Something red...",
			"Kraymoar!!!",
			"You remember an attack on the research facility.",
			"A giant prawn monster attacked, screaming his name over and over again.",
			"'I AM KRAYMOAR!!!' echoes through the back of your mind as you shudder, remembering what that creature looked like.",
			"Once you finish remembering the horrors that you have experienced with kraymoar, you start to remember what had happened earlier this afternoon (if the previous time reference frame is still valid where ever you are now).",
			"Right before you were sucked into the wormhole, you were able to cut off one of Kraymoar's claws, which was sucked into the wormhole before he was.",
			"You remember seeing the claw bump into the generator before it was swallowed by the wormhole, causing it to start glowing bright purple.",
			"After you were sucked into the wormhole, you remember darkness, then light, then something about the 'Duke of Brillo' in a place called 'Scrubbington'",
			"You think that you should probably find him, before Kraymoar rears his ugly face again."
		};


		OutputText.GetComponent<Text>().text = startCinematic(introCine) + "\n";
		StartCoroutine("topScroll");
	}
	
	// Update is called once per frame
	void Update()
	{
		if(_acceptInput && !_cinematic)
		{
			if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				
				Submit();
				
			}
			else if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				//Debug.Log("Up");
				//do something to blank the text
				//InputField.GetComponent<InputField>().MoveTextEnd(true); // moves cursor to the end
				//send in new text / stored text
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				//Debug.Log("Down");
				//do something to blank the text
				//InputField.GetComponent<InputField>().MoveTextEnd(true); // moves cursor to the end
				//send in new text / stored text
			}
			
			InputField.GetComponent<InputField>().ActivateInputField();
		}
	}

	public void Submit()
	{
		
		string input = InputText.GetComponent<Text>().text;
		InputField.GetComponent<InputField>().text = string.Empty;
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
				StartCoroutine("bottomScroll");
				StartCoroutine("selectInput");
				return;

			}
		}
		
		if(input == "")
		{
			//output no text error or just blank line
			
		}
		else
		{
			//send input text to the parser
			string response = parse(input);
			//take input and output and 
			OutputText.GetComponent<Text>().text = OutputText.GetComponent<Text>().text + "\n" + input + "\n" + response + "\n";
		}
		
		StartCoroutine("bottomScroll");
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
	
	string[] tokenize(string input)
	{
		string[] tokens = input.Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
		if(tokens.Length <= 0)
		{
			return null;
		}
		foreach (string sub in tokens)
		{
			if(!sub.All(char.IsLetter))
			{
				return null;
			}
		}
		return tokens;
	}
	
	void deselectInput()
	{
		
		_acceptInput = false;
		InputField.GetComponent<InputField>().DeactivateInputField();
	}
	
	IEnumerator selectInput()
	{
		yield return new WaitForSeconds(.1f);
		_acceptInput = true;
		InputField.GetComponent<InputField>().ActivateInputField();
		InputField.GetComponent<InputField>().Select();
	}
	
	string startCinematic(string[] cine)
	{
		_cinematic = true;
		_currentCinematic = cine;
		deselectInput();
		cinematicButton.SetActive(true);
		_cinematicStage = 1;
		cinematicButton.GetComponent<Button>().Select();
		return cinematic(0, cine);
	}
	
	void stopCinematic()
	{
		_cinematic = false;
		_currentCinematic = null;
		deselectInput();
		cinematicButton.SetActive(false);
		
		
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
	string parse(string input)
	{
		return "meh";
	}
}
