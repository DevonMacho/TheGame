using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSelectController : MonoBehaviour {

	public Button[] Files;
	public bool Load;
	BasicGameInfo[] FileInfo = new BasicGameInfo[3];
	// Use this for initialization
	public void SetLoad(bool val)
	{
		Load = val;
	}
	public void Clicked()
	{
		gameObject.SetActive(true);
		loadBasicSaveInfo();
		for(int i = 0; i < 3; i++)
		{
			Files[i].GetComponent<GameSelectButton>().UpdateBackground(FileInfo[i]);
		}
	}
	void loadBasicSaveInfo()
	{
		// pull in basic file info
		//if null do nothing
		FileInfo[0] = new BasicGameInfo("JPEG","Testing","Somewhere",20,69);
		FileInfo[2] = new BasicGameInfo("JPEG2","Testing2","Somewhere Else",40,27);
	}

}
