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
	bool _acceptInput;
	int _responseNo = 0;
	int _newGameStage = 0;
	int _attempts = 0;
	string _characterName;
	string _characterClass;
	string _characterGender;

	void Start()
	{
		SubmitButton.GetComponent<Button>().onClick.RemoveAllListeners();
		SubmitButton.GetComponent<Button>().onClick.AddListener(delegate {Submit();});
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
		if(_acceptInput)
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
		InputField.GetComponent<InputField>().text = "";

		//format the output properly
		//do a check to see if the text is blank

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
		yield return new WaitForSeconds(.02f);
		ScrollBar.GetComponent<Scrollbar>().value = 0.0000000000000f;
	}
	IEnumerator topScroll()
	{
		yield return new WaitForSeconds(.02f);
		ScrollBar.GetComponent<Scrollbar>().value = 1.0000000000000f;
	}
	
	string[] tokenize(string input)
	{
		string[] tokens = input.Split(default(string[]),System.StringSplitOptions.RemoveEmptyEntries);
		if(tokens.Length <= 0)
		{
			return null;
		}else if(!input.All(char.IsLetter))
		{
			return null;
		}
		else
		{
			return tokens;
		}
	}

	string parse(string input)
	{
		string name = "Nar'Hator: "; //Narrator name
		string[] tkn = tokenize(input);
		_attempts++;
		if(_newGameStage == 0)
		{

			if(tkn.Length > 1 || input.Length > 16)
			{
				return name + "That is too complicated for me to remember, do you go by something shorter?";
			}
			else if(tkn != null)
			{
				_attempts = 0;
				_newGameStage = 1;
				_characterName = tkn[0]; // set caps on name
				return name + "Ah! You do sound like one of those! Is the owner of this name a brave 'Fighter' or possibly a sneaky 'Rogue', or a skilled 'Ranger'. " +
					"Or are you of the magic variety? Possibly a regenerative 'Cleric', a master 'Sorcerer', or a grand 'Wizard'."; 
			}
			else if(input.ToLower() == "mor'thalas")
			{
				return name + "Well hello to you too! I didn't know that you spoke Scrubbish. Oh, right I got a bit carried away. What was your name?";
			}
			else if(input.ToLower() == "les'thalas")
			{
				return name + "Don't go so soon, you just got here, oh wait you can't leave, I'm holding you here until I get to know you better. So, what is your name?";
			}
			return name + "I can't quite understand you. What was your name?";
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
					return name + "Ok, I'm done. You have brought me to the point were I'm breaking character as the narrator of this story, actually I'm more like the Game Master for this whole journey. Yeah, I'm the GM and you are a PC. That " +
						"means that I can kill you at any moment.\nJPEG: Woah wait there buddy, you can't just kill the player, they are just trying to play the game\n" + name +" But it's MY game. I can have control over it if I want to" +
						"\nJPEG: Nope it's my game. I control everything. I'm going to let the player live.\n"+ name +"But...\nJPEG: No Buts. Be nice to the player. Do you want me to rename you to Coq of the Bah'llz clan?\n" +name + "No, No, No, I'll play nice. Player, just pick a class 'Fighter', 'Rogue', 'Ranger', 'Cleric', 'Sorcerer', or 'Wizard'.";
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
			string end = name + "Yeah, I do sort of see that now. Anyways, Lets get a better look at you. \n<Type in 'unequip mask'>";
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
			return "unequip mask";
		}else if (_newGameStage == 5)
		{
			return "Look at mirror";
		}
		else
		{
			return "Guru Meditation 0x0001";
		}
	}
}
