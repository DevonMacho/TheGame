using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class NewGameInput : MonoBehaviour {
	
	public GameObject SubmitButton;
	public GameObject InputField;
	public GameObject InputText;
	public GameObject OutputText;
	public GameObject ScrollBar;
	public GameObject CharacterCreationBackground;
	public GameObject cinematicButton;
	public GameObject inputHolder;
	public Button CharacterCreationAcceptButton;
	bool _acceptInput;
	bool _cinematic;
	int _responseNo = 0;
	int _newGameStage = 0;
	int _attempts = 0;
	int _cinematicStage = 0;
	string _characterName;
	string _characterClass;
	string _characterGender;
	string[] _currentCinematic;
	void Start()
	{
		#if UNITY_EDITOR
			InputField.GetComponent<InputField>().contentType = UnityEngine.UI.InputField.ContentType.Name;
		#else
			InputField.GetComponent<InputField>().contentType = UnityEngine.UI.InputField.ContentType.Standard;
		#endif

		SubmitButton.GetComponent<Button>().onClick.RemoveAllListeners();
		SubmitButton.GetComponent<Button>().onClick.AddListener(delegate {Submit();});
		cinematicButton.GetComponent<Button>().onClick.RemoveAllListeners();
		cinematicButton.GetComponent<Button>().onClick.AddListener(delegate 
		{
			Submit();
		});	
		InputField.GetComponent<InputField>().onEndEdit.RemoveAllListeners();

		//ScrollBar.GetComponent<Scrollbar>().value = 1.0000000000000f;
		InputField.GetComponent<InputField>().characterLimit = 60;
		OutputText.GetComponent<Text>().text = "You find yourself floating in a dark space. You are completely and totally alone. " +
			"As you sit there in the darkness, you wonder who brought you here, why you are here, and who you are. " +
			"As you reach an existential peak, a voice echoes out of the darkness, breaking your concentration. You forget what had brought you to your state of enlightenment." +
			"\n\nVoice: Hello there traveler! You didn't seem like you were doing anything important, so I thought that I should come over and chat with you for a bit. " +
				"My name is Nar'Hator of the of the Omnis'Zant clan. By what name should I call you traveler?\n"; //welcome text
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

		//format the output properly
		//do a check to see if the text is blank
		if(_cinematic)
		{

			string checkValid = cinematic(_cinematicStage,_currentCinematic);
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
				return;
			}
		}

		if(input == "")
		{
			_responseNo = _responseNo % 12;
			string[] response =
			{
				"Hmm?","What?","Speak Up!","Please, speak more softly.","Fine, sit there in silence. See if I care.","I don't have all day!.",
				"Why are you sitting there smiling in silence? Did you loft an air biscuit? Wait, why should I worry about that, you are far away from me!","Why are you so quiet, Is there something wrong with my breath? Oh wait, you can't smell while you are here.","*Whistles loudly while looking at a timekeeping device*"
				,"Get on with it!","Well, do you have something to say? Hmmm!","Spit it out already!","Don't you think that I have better things to do than to sit here with you!","Speak I say!","Are you dumb? Speak!"
			};
			if (_responseNo < 12)
			{
				OutputText.GetComponent<Text>().text = OutputText.GetComponent<Text>().text + "\nNar'Hator: " + response[_responseNo] + "\n";
			}else
			{
				OutputText.GetComponent<Text>().text = OutputText.GetComponent<Text>().text + "\nNar'Hator: " + response[0] + "\n";
			}

			_responseNo += Random.Range(1,4);

		}
		else
		{
			//send input text to the parser
			string response = parse(input);
			//take input and output and 
			OutputText.GetComponent<Text>().text = OutputText.GetComponent<Text>().text + "You: " + input + "\n\n" + response + "\n";
			//Debug.Log(input);
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
		string[] tokens = input.Split(default(string[]),System.StringSplitOptions.RemoveEmptyEntries);
		if(tokens.Length <= 0)
		{
			return null;
		}
		foreach(string sub in tokens)
		{
			if(!sub.All(char.IsLetter))
			{
				return null;
			}
		}
		return tokens;
	}

	string parse(string input)
	{
		string name = "Nar'Hator: "; //Narrator name
		string[] tkn = tokenize(input);
		_attempts++;
		string[] nullResponse = {
			"What was that? I asked what your name was","Hmm? I asked what class you were. Are you a 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'?"
			,"Wazzat? Are you sure that you are a "+ _characterClass+ "? Its a simple 'Yes' or 'No'.", "Huh? I asked if you were a 'Man' or a 'Woman'.", "What? I told you to take off the mask\n<Type in 'unequip mask'>"
			,"Why are you just staring into the darkness? I asked you to look into the mirror.\n<Type in 'look at mirror'>"
			};//add more after you add in more steps
		if(tkn == null)
		{
			return name + nullResponse[_newGameStage];
		}
		if(_newGameStage == 0)
		{


			if(tkn != null && tkn.Length == 1 && input.Length <= 16)
			{
				if(input.ToLower() == "morthalas")
				{
					return name + "Well hello to you too! I didn't know that you spoke Scrubbish. Oh, right I got a bit carried away. What was your name?";
				}
				else if(input.ToLower() == "lesthalas")
				{
					return name + "Don't go so soon, you just got here, oh wait you can't leave, I'm holding you here until I get to know you better. So, what is your name?";
				}
				_attempts = 0;
				_newGameStage = 1;
				_characterName = tkn[0]; // set caps on name
				return name + "Ah! You do sound like one of those! Is the owner of this name a brave 'Fighter' or possibly a sneaky 'Rogue', or a skilled 'Ranger'. " +
					"Or are you of the magic variety? Possibly a regenerative 'Cleric', a master 'Sorcerer', or a grand 'Wizard'."; 
			} 
			else if(tkn.Length > 1 || input.Length > 16)
			{
				return name + "That is too complicated for me to remember, do you go by something shorter?";
			}
			else
			{
				return name + "I can't quite understand you. What was your name?";
			}
		}
		else if (_newGameStage == 1)
		{
			if(tkn.Length > 1)
			{
				return name + "You can only be one class, this isn't a mix and match grab bag sort of thing. Pick 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'.";
			}
			else
			{
				string[] classes = {"Fighter","Rogue","Ranger","Cleric","Sorcerer","Wizard"};
				foreach(string _class in classes)
				{
					if(_class.ToLower() == tkn[0].ToLower())
					{
						_attempts = 0;
						_newGameStage = 2;
						_characterClass = _class;
						return name + "I didn't really take you for a " + _characterClass + ". Are you sure that you are a " + _characterClass + "?";
					}else if(tkn[0].ToLower() == "paladin")
					{
						return name + "You aren't a goody two shoes, no one who throws kittens into a wormhole for a living is lawful good, unless they thought that was good, then they are most likely evil, and... you know what that is a paradox, no paladins due to the logical paradox that I just mentioned. Just pick 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'.";
					}else  if(tkn[0].ToLower() == "swashbuckler" || tkn[0].ToLower() == "pirate")
					{
						return name + "There are no buttpirates in this game, so please, move on and pick a real class like a 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'. No homebrew nonsense is allowed in this game.";
					}
				}
				if(_attempts == 1)
				{
					return name + "You want to be a what? Pick 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'.";
				}else if(_attempts == 2)
				{
					return name + "Look, Just pick 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'.";
				}
				else if(_attempts == 3)
				{
					return name + "I'm asking nicely, pick a class, your options are 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'.";
				}
				else if(_attempts == 4)
				{
					/*
					return name + "Ok, I'm done. You have brought me to the point were I'm breaking character as the narrator of this story, actually I'm more like the Game Master for this whole journey. Yeah, I'm the GM and you are a PC. That " +
						"means that I can kill you at any moment!\nJPEG: Woah wait there buddy, you can't just kill the player, they are just trying to play the game.\n" + name +" But it's MY game. I can have control over it if I want to!" +
						"\nJPEG: Nope it's my game. I control everything. I'm going to let the player live.\n"+ name +"But...\nJPEG: No Buts. Be nice to the player. Do you want me to rename you to Coq of the Bah'llz clan?\n" +name + "No, No, No, I'll play nice. Player, just pick a class 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'.";
			        */
					string[] cine1 = 
					{
						name + "Ok, I'm done. You have brought me to the point were I'm breaking character as the narrator of this story, actually I'm more like the Game Master for this whole journey. Yeah, I'm the GM and you are a PC. That means that I can kill you at any moment!",
						"JPEG: Woah wait there buddy, you can't just kill the player, they are just trying to play the game.",
						name +" But it's MY game. I can have control over it if I want to!",
						"JPEG: Nope it's my game. I control everything. I'm going to let the player live.",
						name +"But...",
						"JPEG: No Buts. Be nice to the player. Do you want me to rename you to Coq of the Bah'llz clan?",
						name + "No, No, No, I'll play nice. Player, just pick a class 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'."
					};
					//return cinematic part 1 (technically element 0)
					return startCinematic(cine1); //get string array made


}
				else
				{
					return name + "'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'.";
				}
			}
		}
		else if (_newGameStage == 2)
		{
			if(tkn.Length > 1)
			{
				return name + "It's a simple 'Yes' or 'No' question.";
			}
			else if(tkn[0].ToLower() == "yes")
			{
				_attempts = 0;
				_newGameStage = 3;
				return name + "Alright, whatever you say. By the way, not to be rude or anything but I can't tell if you are a Man or a Woman underneath that protective mask.";
			}
			else if(tkn[0].ToLower() == "no")
			{
				_newGameStage = 1;
				_attempts = 0;
				return name + "If you aren't a " + _characterClass + " then what are you? Are you a 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'?";
			}
			else
			{
				return name + "what was that? I asked if you were a " + _characterClass +".";
			}
		}
		else if(_newGameStage == 3)
		{
			string end = name + "Yeah, I do sort of see that now. Anyways, Lets get a better look at you.\n<Type in 'unequip mask'>";
			if(tkn.Length > 1)
			{
				return name + "It really is a one word answer, you are either a 'Man' or a 'Woman.'";
			}else if(tkn[0].ToLower() == "man")
			{
				_characterGender = "Man";
				_newGameStage = 4;
				return end;

			}else if(tkn[0].ToLower() == "woman")
			{
				_characterGender = "Woman";
				_newGameStage = 4;
				return end;
			}
			else if (_attempts == 1)
			{
				return name + "What was that? I asked if you are a 'Man' or a 'Woman'.";
			}else if (_attempts == 2)
			{
				return name + "Choose either 'Man' or a 'Woman'.";
			}
			else if (_attempts == 3)
			{
				return name + "'Man' or 'Woman'.";
			}else if (_attempts == 4)
			{
				return name + "'MAN'/'WOMAN'!!!";
			}else if (_attempts == 5)
			{
				return "JPEG: Hey, I'm the developer of this game. I just wanted to let you know that I am a lazy bastard and " +
					"I only put in two options for gender. Now, I am more lazy than insensitive to the nearly infinite options that are out there for " +
					"gender. To show how not insensitive I am, I included a gender neutral character option in the create a character step of the new game process (it's a checkbox up in the corner somewhere). " +
					"This option allows you to use any face parts that you want on your character portrait. Anyways, for time sake, please choose either 'Man' or 'Woman'.";
			}
			else
			{
				return "'Man' or 'Woman'.";
			}
		}else if (_newGameStage == 4)
		{
			//begin proto unequip function
			if(tkn[0].ToLower() == "unequip")
			{
				if(tkn.Length == 1)
				{
					return "Unequip what?";
				}
				else if(tkn.Length == 2)
				{
					if(tkn[1].ToLower() == "mask")
					{
						_newGameStage = 5;
						return name + "Ah, much better! I can finally see your face. Here take a look into this mirror\nA mirror materializes infront of you. <type in 'look at mirror'>";
					}
					else
					{
						return "You do not have that item equipped";
					}
				}else
				{
					return "You only have two arms and can not unequip more than one item at a time.";
				}
			}
			else
			{
				return name + "Just take off the mask already, I'm tired of waiting!\n<Type in 'unequip mask'>";
			}
		}else if (_newGameStage == 5)
		{
			if(tkn.Length == 1)
			{
				if(tkn[0].ToLower()  == "look")
				{
					return "you are completely surrounded by darkness, there is a single handheld mirror in front of you.";
				}else
				{
					return name + "What are you doing? I told you to look at the mirror\n<Type in 'look at mirror'>";
				}
			}else if(tkn.Length <= 3)
			{
				if(tkn[0].ToLower() == "look")
				{
					if(tkn[1].ToLower() == "at")
					{
						if(tkn.Length == 3)
						{
							if(tkn[2].ToLower() == "mirror")
							{
								_newGameStage = 6;
								deselectInput();
								CharacterCreationBackground.SetActive(true);
								return name + "Ah yes, you look like you are just about ready to go. Just open that door behind you and you can be on your way.\nAn average sized door suddenly materializes behind you <Type in 'open door' to leave>";//name +"Good luck on your adventure traveler! Oh, and do say hello to the Duke of Brillo over in Scrubbington for me, he might be able to help you out in your travels.";
							}
							else
							{
								return "That object is not in this plane of existence.";
							}
						}
						else
						{
							return "Look at what?";
						}

					}
					else
					{
						return name + "Why are you looking around franticly? I told you to look at the mirror.\n<Type in 'look at mirror'>";
					}
				}
				else
				{
					return name + "What are you doing? I told you to look at the mirror.\n<Type in 'look at mirror'>";
				}
			}
			else
			{
				return "*snaps fingers loudly* Are you awake in there? I asked you to look at the mirror.\n<Type in 'look at mirror'>";
			}

		}
		else if (_newGameStage == 6)
		{
			if(tkn[0].ToLower() == "open")
			{
				if(tkn.Length == 1)
				{
					return "Open what?";
				}
				else if (tkn.Length == 2)
				{
					if(tkn[1].ToLower() == "door")
					{
						return "A white light surrounds you as you open the door, you can hear a voice yelling behind you.\n" + name +"Good luck on your adventure traveler! Oh, and do say hello to the Duke of Brillo over in Scrubbington for me, he might be able to help you out in your travels!"+"\nThe game technically started but thats as far as I got so far. I also need to add in a large ass continue button for 'cutscenes' or large blocks of text";
					}
					else
					{
						return "That object does not exist on this plane of existence.";
					}
				}
				else
				{
					return "You can only open one thing at a time, as you only have two hands.";
				}
			}
			else
			{
				return name + "Why are you fiddling with your hands, open the door to leave.\n<Type in 'open door' to leave>";
			}
		}
		else
		{
			return "Guru Meditation 0x0001";
		}
	}
	void deselectInput()
	{
		_acceptInput = false;
		InputField.GetComponent<InputField>().DeactivateInputField();
	}
	string startCinematic(string[] cine)
	{
		_cinematic = true;
		_currentCinematic = cine;
		deselectInput();
		cinematicButton.SetActive(true);
		inputHolder.SetActive(false);
		_cinematicStage = 1;
		return cinematic(0,cine);
	}
	void stopCinematic()
	{
		_cinematic = false;
		_currentCinematic = null;
		deselectInput();
		cinematicButton.SetActive(false);
		inputHolder.SetActive(true);
	}
	string cinematic(int stage, string[] content)
	{

		if(content == null || stage < 0 || stage >= content.Length)
		{
			return null;
		}
		else
		{
			return content[stage];
		}
	}
}
