using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
public class GameInput : MonoBehaviour
{

	public GameObject SubmitButton;
	public GameObject InputField;
	public GameObject InputText;
	public GameObject OutputText;
	public GameObject ScrollBar;
	public GameObject cinematicButton;
	public GameObject inputHolder;
	public GameObject quitMenu;
	public GameObject GameView;
	public Button quitYes;
	public Button quitNo;
	public Image fore1;
	public Image fore2;
	public Sprite testImage;
	public InventoryController inv;
	List<string> _cmdHist;
	bool _acceptInput;
	bool _cinematic;
	int _cinematicStage = 0;
	int _cmdLoc = 1;
	string[] _currentCinematic;
	// Use this for initialization
	void Awake()
	{
		#if UNITY_EDITOR
		InputField.GetComponent<InputField>().contentType = UnityEngine.UI.InputField.ContentType.Name;
		#else
		InputField.GetComponent<InputField>().contentType = UnityEngine.UI.InputField.ContentType.Standard;
		#endif
		quitMenu.SetActive(false);
		quitYes.onClick.RemoveAllListeners();
		quitNo.onClick.RemoveAllListeners();
		quitYes.onClick.AddListener(delegate
		{
			Application.LoadLevel("MainMenu");
		});
		quitNo.onClick.AddListener(delegate
		{
			cancelQuit();
		});
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
		clearHistory();

		 string[] introCine = 
		{
			"The surrounding white light fades as you wake up in the middle of a clearing",
			"You panic for a moment, being surrounded by several charred cats, but then you remember;",
			"You were a researcher at a wormhole research facility on the planet Uranus.",
			"Right before you had a small lab accident resulting in what could only be described as quantum displacement through a wormhole,",
			"you were conducting several wormhole experiments involving throwing cats into generated wormholes to see if they would arrive live or dead depending on the spin state of subatomic particles.",
			"It looks like all of them have been charred by the wormhole... So much for quantum mechanics.",
			"As you are staring at the pile of dead cats, you remember something.",
			"Something big...",
			"Something red...",
			"Kraymoar!!!",
			"You remember an attack on the research facility.",
			"A giant prawn monster attacked, screaming his name over and over again.",
			"'I AM KRAYMOAR!!!' echoes through the back of your mind as you shudder, remembering what that creature had looked like.",
			"Once you finish remembering the horrors that you have experienced with Kraymoar, you start to remember what had happened earlier this afternoon (if the previous time reference frame is still valid, where ever you are now).",
			"Right before you were sucked into the wormhole, you were able to cut off one of Kraymoar's claws, which was sucked into the wormhole before he was.",
			"You remember seeing the claw bump into the generator before it was swallowed by the wormhole, causing it to start glowing bright purple.",
			"After you were sucked into the wormhole, you remember darkness, then light, then something about the 'Duke of Brillo' in a place called 'Scrubbington'",
			"You think that you should probably find him, before Kraymoar rears his ugly face again."
		};

		if(GameMaster.GM.NewGame)
		{
			//change testimage to level 1 background
			StartCoroutine(fadeTexture(testImage, new Sprite()));
			OutputText.GetComponent<Text>().text = startCinematic(introCine) + "\n";
		}
		else
		{
			//chage testimage to level GameMaster.Data.node background
			fore2.color = new Color(0,0,0,255);
			StartCoroutine(fadeTexture(testImage, new Sprite()));
			OutputText.GetComponent<Text>().text =  "Welcome Back!\n";
		}
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
			else if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				if(_cmdLoc < _cmdHist.Count)
				{
					_cmdLoc++;
					InputField.GetComponent<InputField>().text = _cmdHist[_cmdLoc -1];
				}
				InputField.GetComponent<InputField>().MoveTextEnd(true);
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				if(_cmdLoc > 1)
				{
					_cmdLoc--;
					InputField.GetComponent<InputField>().text = _cmdHist[_cmdLoc -1];
				}
				InputField.GetComponent<InputField>().MoveTextEnd(true);

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
		
		if(input != "")
		{
			//send input text to the parser
			string response = parse(input);
			_cmdHist.Insert(1,input);
			_cmdLoc = 1;
			//take input and output and 
			OutputText.GetComponent<Text>().text = OutputText.GetComponent<Text>().text + "\n" + input + "\n" + response + "\n";
		}
		if(gameObject.activeInHierarchy)
		{
			StartCoroutine("bottomScroll");

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
	
	public void deselectInput()
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
	IEnumerator clearDelay()
	{
		OutputText.GetComponent<Text>().text = "";
		clearHistory();
		yield return new WaitForSeconds(.001f);
		OutputText.GetComponent<Text>().text = "<<<======== cleared ========>>>";
		StartCoroutine("topScroll");

	}
	void clearHistory()
	{
		_cmdHist = new List<string>();
		_cmdHist.Add("");
		_cmdLoc = 1;
	}
	public void StartQuit()
	{
		deselectInput();
		GameView.SetActive(false);
		quitMenu.SetActive(true);

	}
	void cancelQuit()
	{
		OutputText.GetComponent<Text>().text = OutputText.GetComponent<Text>().text + "\nQuit Aborted\n";
		GameView.SetActive(true);
		quitMenu.SetActive(false);
		StartCoroutine("bottomScroll");
	}
	string parse(string input)
	{
		string[] tkn = tokenize(input);
		if(tkn == null)
		{
			return "Invalid Command";
		}
		else
		{
			foreach(Command a in GameCommands.Commands)
			{
				if(tkn[0].ToLower() == a.CommandName)
				{
					if(tkn.Length > 1)
					{
						if(a.SubCommands != null)
						{
							return GameCommands.ProcessCommands(a,tkn);
						}
						else
						{
							return "This command does not have any modifiers.";
						}
					}
					else if (tkn.Length <= 1 && tkn.Length > 0)
					{
						if(a.SubCommands != null)
						{
							return GameCommands.DisplaySubCommands(a);
						}
						else
						{
							return GameCommands.ProcessCommands(a,tkn);
						}
					}

				}
			}
		}
		return "Invalid Command";
	}
	IEnumerator fadeTexture(Sprite bottom, Sprite top)
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
