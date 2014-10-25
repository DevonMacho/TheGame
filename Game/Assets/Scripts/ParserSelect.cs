using UnityEngine;
using System.Collections;

public class ParserSelect : MonoBehaviour
{
    public static int parserSelect = 0;
    protected static int previousParser;
    //0 - main menu
    //1 - game
    //2 - save
    //3 - load
    //4 - new game
    //5 - quit
    public static int getPrevious()
    {
        return previousParser;
    }
    public static string Parser(string input)
    {
        if (parserSelect == 0)
        {
            string output = MainMenuParser.Parse(input);
            previousParser = parserSelect;
            parserSelect = stateChange(output);
            return output;
        }
        else if (parserSelect == 1)
        {
            string output = GameParser.Parse(input);
            previousParser = parserSelect;
            parserSelect = stateChange(output);
            return output;
        }
        else if (parserSelect == 4)
        {
            string output = NewGameParser.Parse(input);
            previousParser = parserSelect;
            parserSelect = stateChange(output);
            return output;
        }
        /*else if (parserSelect == 2)
        {

            string output = GameParser.Parse(input);
            previousParser = parserSelect;
            stateChange(output);
            return output;

        }
        */
        else if (parserSelect == 5)
        {
            string output = GenericCommands.quitParser(input);
            previousParser = parserSelect;
            parserSelect = stateChange(output);
            return output;
        }
        else
        {
            return input;
        }
    }

    protected static int stateChange(string input)
    {
        string[] trimput = input.Trim().Replace("<<Starting New Game>>","<<<NewGameStartNow>>>").Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries);
        if (input.Equals("Are you sure you want to quit?"))
        {
            return 5;
        }
        else if (trimput[0].Equals("<<<NewGameStartNow>>>"))
        {
            return 4;
        }
        else if (input.Equals("<<Loading>>"))
        {
            return 3;
        }
        else if (input.Equals("<<Saving>>"))
        {
            return 2;
        }
        else if (input.Equals("<<Resuming Game>>"))
        {
            return 1;
        }
        else if (input.Equals("<<Returning to Main Menu>>"))
        {
            return 0;
        }
        else
        {
            return parserSelect;
        }
    }
}
