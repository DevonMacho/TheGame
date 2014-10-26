using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class NewGameParser : MonoBehaviour
{

    // Use this for initialization
     
    public static List<string> files;
    private static int stage = 0;
    public static string xmlFile = "";
    private static string characterName = "";
    private static string type = "";
    private static string gender = "";
    private static int timesSubmitted = 0;

    public static string startNewGame(string[] token)
    {
        //change background to black
        if (token.Length > 1)
        {
            return "too many args";
        }


        files = new List<string>();
        listXML();


        if (files.Count >= 1)
        {
            string baseline = "<<Starting New Game>>\n";
            stage = 1;
            return baseline + listXML();
        }
        else
        {
            return "there aren't any scenarios in the folder, try using the 'import' command";
        }
    }

    public static string listXML()
    {
        string baseline = "Enter in the number of the scenario that you want to load\n--------Scenarios--------";
        
        string[] UncleanScenarios = Directory.GetFiles(Application.persistentDataPath + "/Scenarios/");
        string midline = "";
        int count = 0;
        foreach (string a in UncleanScenarios)
        {
            count ++;
            string[] cleaner = a.Trim().Replace("/", " ").Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
            files.Add(cleaner [cleaner.Length - 1]);
            midline = "\n" + count + ") " + cleaner [cleaner.Length - 1] + midline;
        }
        
        string final = baseline + midline + "\n------------------------";
        return final;
    }
    //get work done on stuff...

    public static string Parse(string input)
    {
        string[] token;
        if (stage != 2)
        {
            token = GenericCommands.tokenize(input);
        }
        else
        {
            token = GenericCommands.tokenizeKeepCase(input);
        }
        if (stage == 1)
        {
            if (token.Length <= 0)
            {
                return "Invalid Input";
            }
            if (token.Length > 1)
            {
                return "too many args";
            }
            int parsed;
            int.TryParse(token [0], out parsed);
            if (parsed > 0 && parsed <= files.Count)
            {
                xmlFile = files [parsed - 1];

                stage = 2;
                return "Scenario input accepted! transporting to the between zone...\n\nYou find yourself floating, alone in a dark space." +
                    "\nYou wonder who brought you here, or even why you are here.\nSuddenly a voice echoes out of the darkness:\n" +
                    "You look familiar, by what name do you call yourself traveler?";
            }
            else
            {
                return "Invalid Input\n" + listXML();
            }
        }
        else if (stage == 2)
        {
            if (token.Length <= 0)
            {
                return "\nI can't quite hear you, you sound muffled.";
            }
            if (token [0].Contains("!") || token [0].Equals(token [0].ToUpper()))
            {
                return "\nI'm a tad deaf in this ear, could you try speaking louder next time *Sarcasm*.";
            }
            if (token.Length > 1 || token[0].Length > 32)
            {
                return "\nThat is too complicated for me to remember, what do you call yourself for short?";
            }
            else if (token.Length == 1)
            {
                characterName = token [0];
                stage = 3;
                return "\nAh! " + characterName + ", you do sound like one of those. Are you here for fame, fortune, or power?";
            }
        }
        else if (stage == 3)
        {
            if (token.Length <= 0)
            {
                return "\nWhat was that? Are you here for fame, fortune, or power?";
            }
            if (input.ToLower().Equals("are all 3 an option?\n"))
            {
                return "\nsadly no, you have to choose either fame, fortune, or power.";
            }
            if (token.Length > 1)
            {
                return "\nwoah, repeat that one more time, but be quick about it and to the point; fame, fortune, or power.";
            }
            if (token [0].Equals("fame"))
            {
                type = "hunter";
            }
            else if (token [0].Equals("fortune"))
            {
                type = "thief";
            }
            else if (token [0].Equals("power"))
            {
                type = "knight";
            }
            else
            {
                return "\nyou're here for what now?";
            }
            if (type != "")
            {
                stage = 4;
                timesSubmitted = 0;
                return "\nI didn't take you for a " + type + ", are you sure that you are a " + type + "?";
            }


        }
        else if (stage == 4)
        {

            if (token.Length <= 0)
            {
                timesSubmitted++;
                if (timesSubmitted == 1)
                {
                    return "\nHmmm? did you say something. Just tell me yes or no";
                }
                if (timesSubmitted == 2)
                {
                    return "In case you forgot, just sitting there quietly, staring into nothingness, I asked a yes or no question.\n";
                }
                if (timesSubmitted == 3)
                {
                    return "You know what, you are completely ignoring the question, I'm not even going to repeat myself this time, " +
                        "you can just scroll back up on that little bar thingy and read what I said earlier, " +
                        "I am like god, talking to you right now and I am the all seeing and all powerful being that controls the game you are playing right now" +
                        " for all of you geeks out there, this is the narrator breaking the 4th wall out of frustration, due to insensitive players such as yourself\n";
                }
                if (timesSubmitted == 4)
                {
                    return "I hate you so much...\n";
                }
                if (timesSubmitted == 5)
                {
                    return "I hate you so much more now\n";
                }
                if (timesSubmitted == 6)
                {
                    return "I hate you even more now\n";
                }
                if (timesSubmitted == 7)
                {
                    return "Hate bar loading |-|\n";
                }
                if (timesSubmitted == 8)
                {
                    return "Hate bar loading |---|\n";
                }
                if (timesSubmitted == 9)
                {
                    return "Hate bar loading |-----|\n";
                }
                if (timesSubmitted == 10)
                {
                    return "Hate bar loading |---------|\n";
                }
                if (timesSubmitted == 11)
                {
                    return "Hate bar loading |--------------|\n";
                }
                if (timesSubmitted == 12)
                {
                    return "Hate bar loading |----------------------|\n";
                }
                if (timesSubmitted == 13)
                {
                    return "Hate bar loading |--------Hate-Fully-Loaded------|\n";
                }
                if (timesSubmitted >= 14 && timesSubmitted < 100)
                {
                    return "you've done this to me " + timesSubmitted + " times\n";
                }
                if (timesSubmitted == 100)
                {
                    return "one more time and I'm going to quit the game\n";
                }
                if (timesSubmitted == 101)
                {
                    return "I'm being serious\n";
                }
                if (timesSubmitted == 102)
                {
                    return "seriously, stop\n";
                }
                if (timesSubmitted == 103)
                {
                    return "you forced me to do this\n";
                }
                if (timesSubmitted == 104)
                {
                    return "one last chance, yes or no\n";
                }
                if (timesSubmitted >= 105)
                {
                    return GenericCommands.quit();
                }
            }
           
            if (token.Length > 1 && token.Length < 3)
            {
                return "\nIt's a simple yes or no";
            }
            else if (token.Length > 3)
            {
                return "\nIt's a simple yes or no not the story of your life and your indecisiveness";
            }
            else if (token [0].Equals("yes"))
            {
                stage = 5;
                return "\nAlright then. By the way, not to be rude or anything, but I can’t tell if you are a man or woman with that mask on. what exactly are you?";
            }
            else if (token [0].Equals("no"))
            {
                stage = 3;
                return "\nwell if you're not that, what are you here for; fame, fortune or power?";
            }
            else
            {
                return "\nI didn't quite hear you, was that a yes or a no?";
            }
          
        }
        else if (stage == 5)
        {
            timesSubmitted = 0;
            if (token.Length <= 0)
            {
                return "\nWhat was that? I asked if you were a man or a woman.";
            }
            else if (token.Length > 1)
            {
                return "\nJust tell me if you are a man or woman.";
            }
            else if (token [0].Equals("man"))
            {
                gender = "male";
                stage = 6;
                return endScene(0);
            }
            else if (token [0].Equals("woman"))
            {
                gender = "female";
                stage = 6;
                return endScene(0);
            }
            else if (timesSubmitted == 0)
            {
                return "\nthat was completely unintelligible, are you a man or a woman?";
            }
            else if (timesSubmitted == 1)
            {
                return "\nthere are two choices, man or woman";
            }
            else if (timesSubmitted == 2)
            {
                return "\nman / woman";
            }
            else if (timesSubmitted == 3)
            {
                return "\nMAN / WOMAN";
            }
            else if (timesSubmitted >= 4)
            {
                return "\nHey look, the programmer is lazy and didn't put in more than two of the infinite options that are out there for gender." +
                    " Trust me, he is more lazy than insensitive to the several other options for gender. Hell, he even told me to tell you that. " +
                    "So please, just for time's sake, pick either man or woman.";
            }
        }
        else if (stage == 6)
        {
            stage = 7;
            return endScene(1);
        }
        else if (stage == 7)
        {
            stage = 8;
            return endScene(2);
        }
        else if (stage == 8)
        {
            return WorldData.StartNewGame(characterName, gender, type, xmlFile);
        }

        return "Guru Mediation x0000005";
    }

    private static string endScene(int step)
    {
        if (step == 0)
        {
            return "\nI can kinda see that now, yeah....\n<input anything to advance>";
        }
        if (step == 1)
        {
            //fade to white
            return "\nA white light surrounds you as you start to feel heavier and heavier.\n<input anything to advance>";
        }
        if (step == 2)
        {

            return "\nWell, its time for me to go, good luck on your adventure!\n<input anything to start your adventure>";
        }
        return "Guru Meditation x0000006";
    }
}
