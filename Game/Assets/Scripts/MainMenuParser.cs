using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuParser : MonoBehaviour
{
    private static Dictionary<string, int> commandList0;

    public static void initializeCommands()
    {
        commandList0 = new Dictionary<string,int >();
        #region Main Menu Commands
        commandList0.Add("help", 0);
        commandList0.Add("clear", 1);
        commandList0.Add("quit", 2);
        commandList0.Add("load", 3);
        commandList0.Add("newgame", 4);
        commandList0.Add("import", 9);
        #endregion


    }
}
