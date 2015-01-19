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
		FileInfo[0] = GameMaster.GM.LoadInfo(1);
		FileInfo[1] = GameMaster.GM.LoadInfo(2);
		FileInfo[2] = GameMaster.GM.LoadInfo(3);
	}

}
