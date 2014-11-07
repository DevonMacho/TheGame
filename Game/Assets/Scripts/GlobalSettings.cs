using UnityEngine;
using System.IO;
using System.Collections;

public class GlobalSettings : MonoBehaviour
{

    void Start()
    {

    
    }
    public static void settingDefaults()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Scenarios/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Scenarios/");
        }
        string[] assets = {"Scenarios/BaseGame","Scenarios/Readme"};
        
        
        TextAsset basegame = Resources.Load(assets [0]) as TextAsset;
        TextAsset readme = Resources.Load(assets [1]) as TextAsset;
        
        
        byte[] baseText = basegame.bytes;
        byte[] readText = readme.bytes;
        FileStream file1 = File.Create(Application.persistentDataPath + "/Scenarios/BaseGame.xml");
        FileStream file2 = File.Create(Application.persistentDataPath + "/Scenarios/Readme.txt");
        file1.Write(baseText, 0, baseText.Length);
        file2.Write(readText, 0, readText.Length);
        file1.Close();
        file2.Close();
    }
    // Update is called once per frame
    void Update()
    {
    
    }
}
