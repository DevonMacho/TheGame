using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class NewGameParser : MonoBehaviour
{

    public static int fileCount;
    private static int stage = 0;
    static string characterName = "";
    private static string type = "";
    private static string gender = "";
    private static int timesSubmitted = 0;

    public static void startNewGame()
    {
            stage = 1;
    }


    /*
     * need to put in a way to get stats in....
     * 
     * what I know:
     * 
     * Stats:
     * 
     * Strength: Melee Weapon Modifier / Weapon type you can use <- dont care about spelling
     * Perception: Ranged Weapon Modifier / Weapon type you can use
     * Endurance: Health Modifier
     * Charisma: not included
     * Intelligence: not included... <Fallout Joke Completed>
     * Agility: Speed Modifier - Higher Doge Chance (such agility much dodge so wow!)
     * Luck: Higher odds of finding better Weapons / Items
     * 
     * Total: 5 stats
     * 
     * Base Character:
     * Strength: 5
     * Perception: 5
     * Endurance: 5
     * Agility: 5
     * Luck: 5
     * 
     * 
     * Higher: 1    --  Ceil((Roll from 100 - 300) / 100)
     * High: 1      --  Ceil((Roll from 100 - 200) / 100)
     * Meh: 2       --  Floor((Roll from 0 - 101) / 100) 
     * Shit: 1      --  -Ceil((Roll from 100 - 200) / 100)
     * 
     * Hunter - High Strength, Higher Perception, Meh Endurance, Meh Agility, Shit Luck 
     * 
     * Thief - Meh Strength, Meh Perception, Shit Endurance, Higher Agility, High Luck 
     * 
     * Knight - Higher Strength, Meh Perception, High Endurance, Shit Agility, Meh Luck
     * 
     * Hunter (Extreme Stats):
     * Strength: 8
     * Perception: 7
     * Endurance: 5
     * Agility: 5
     * Luck: 3
     * 
     * Thief (Extreme Stats):
     * Strength: 5
     * Perception: 5
     * Endurance: 3
     * Agility: 8
     * Luck: 7
     * 
     * Knight (Extreme Stats):
     * Strength: 8
     * Perception: 5
     * Endurance: 7
     * Agility: 3
     * Luck: 5
     * 
     */

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
            stage = 2;
            return "You find yourself floating, alone in a dark space." +
                "\nYou wonder who brought you here, or even why you are here.\nSuddenly a voice echoes out of the darkness:\n\n" +
                "You look familiar, by what name do you call yourself traveler?";
        }
        else if (stage == 2)
        {
            if (token.Length <= 0)
            {
                return "I can't quite hear you, you sound muffled.";
            }
            if (token [0].Contains("!") || token [0].Equals(token [0].ToUpper()))
            {
                return "I'm a tad deaf in this ear, could you try speaking louder next time *Sarcasm*.";
            }
            if (token.Length > 1 || token [0].Length > 32)
            {
                return "That is too complicated for me to remember, what do you call yourself for short?";
            }
            else if (token.Length == 1)
            {
                characterName = token [0];
                stage = 3;
                return "Ah! " + characterName + ", you do sound like one of those. Are you here for fame, fortune, or power?";
            }
        }
        else if (stage == 3)
        {
            if (token.Length <= 0)
            {
                return "What was that? Are you here for fame, fortune, or power?";
            }
            if (input.ToLower().Equals("are all 3 an option?\n"))
            {
                return "sadly no, you have to choose either fame, fortune, or power.";
            }
            if (token.Length > 1)
            {
                return "woah, repeat that one more time, but be quick about it and to the point; fame, fortune, or power.";
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
                return "you're here for what now?";
            }
            if (type != "")
            {
                stage = 4;
                timesSubmitted = 0;
                return "I didn't take you for a " + type + ", are you sure that you are a " + type + "?";
            }


        }
        else if (stage == 4)
        {

            if (token.Length <= 0)
            {
                timesSubmitted++;
                if (timesSubmitted == 1)
                {
                    return "Hmmm? did you say something. Just tell me yes or no";
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
                return "It's a simple yes or no";
            }
            else if (token.Length > 3)
            {
                return "It's a simple yes or no not the story of your life and your indecisiveness";
            }
            else if (token [0].Equals("yes"))
            {
                stage = 5;
                timesSubmitted = 0;
                return "Alright then. By the way, not to be rude or anything, but I can’t tell if you are a man or woman with that mask on. what exactly are you?";
            }
            else if (token [0].Equals("no"))
            {
                stage = 3;
                return "well if you're not that, what are you here for; fame, fortune or power?";
            }
            else
            {
                return "I didn't quite hear you, was that a yes or a no?";
            }
          
        }
        else if (stage == 5)
        {

            if (token.Length <= 0)
            {
                return "What was that? I asked if you were a man or a woman.";
            }
            else if (token.Length > 1)
            {
                return "Just tell me if you are a man or woman.";
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
            timesSubmitted++;
            if (timesSubmitted == 1)
            {
                return "that was completely unintelligible, are you a man or a woman?";
            }
            else if (timesSubmitted == 2)
            {
                return "there are two choices, man or woman";
            }
            else if (timesSubmitted == 3)
            {
                return "man / woman";
            }
            else if (timesSubmitted == 4)
            {
                return "MAN / WOMAN";
            }
            else if (timesSubmitted >= 5)
            {
                return "Hey look, the programmer is lazy and didn't put in more than two of the infinite options that are out there for gender." +
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
            return WorldData.StartNewGame(characterName, gender, type, GUISelector.FilePath);
        }

        return "Guru Mediation x0000005";
    }

    private static string endScene(int step)
    {
        if (step == 0)
        {
            return "I can kinda see that now, yeah....\n<input anything to advance>";
        }
        if (step == 1)
        {
            //fade to white
            return "A white light surrounds you as you start to feel heavier and heavier.\n<input anything to advance>";
        }
        if (step == 2)
        {

            return "Well, its time for me to go, good luck on your adventure!\n<input anything to start your adventure>";
        }
        return "Guru Meditation x0000006";
    }
}
