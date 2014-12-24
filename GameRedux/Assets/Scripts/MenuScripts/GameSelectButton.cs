using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSelectButton : MonoBehaviour {

	public int SlotNumber;
	public GameObject Controller;
	public GameObject Background;
	public GameObject MessageBox;
	public GameObject MessageBoxYes;
	GameMaster GM;
	public void UpdateBackground(BasicGameInfo info)
	{
		GetComponent<Button>().onClick.RemoveAllListeners();
		GM = GameObject.FindObjectOfType<GameMaster>();

		Transform gameData = transform.GetChild(0);
		Transform noData = transform.GetChild(1);
		Transform fileData = gameData.GetChild(3);
		if (info != null)
		{
			if(Controller.GetComponent<GameSelectController>().Load)
			{

				GetComponent<Button>().onClick.AddListener(delegate 
				{
					Background.SetActive(false);
					GM.LoadGame(SlotNumber);
				});
			}
			else
			{

				GetComponent<Button>().onClick.AddListener(delegate 
				{
					Background.SetActive(false);
					MessageBox.SetActive(true);
					MessageBoxYes.GetComponent<Button>().onClick.RemoveAllListeners();
					MessageBoxYes.GetComponent<Button>().onClick.AddListener(delegate
					{
						MessageBox.SetActive(false);
						GM.StartNewGame(SlotNumber);
					});

				});
			}
			gameData.gameObject.SetActive(true);
			noData.gameObject.SetActive(false);
			gameData.GetChild(0).GetComponent<Text>().text = "[   File "+ SlotNumber +"   ]"; //sets the slot number
			gameData.GetChild(1).GetComponent<Text>().text = info.Name; //set character name
			gameData.GetChild(2).GetComponent<Text>().text = info.CharClass; //set character class
			Transform basicStats = fileData.GetChild (0);
			basicStats.GetChild(0).GetComponent<Text>().text = "Loc:\t" + info.Name; //sets the location
			basicStats.GetChild(1).GetComponent<Text>().text ="GP:\t"+ info.Gold; //sets the gold value
			basicStats.GetChild(2).GetComponent<Text>().text ="LvL:\t"+ info.Level; //sets the gold value
		}

		else
		{
			gameData.gameObject.SetActive(false);
			noData.gameObject.SetActive(true);
			if(!Controller.GetComponent<GameSelectController>().Load)
			{
				GetComponent<Button>().onClick.AddListener(delegate 
				{
					Background.SetActive(false);
					GM.StartNewGame(SlotNumber);
				});
			}
		}
	}
}
