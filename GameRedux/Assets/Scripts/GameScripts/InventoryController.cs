using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryController : MonoBehaviour {

	// Use this for initialization
	public Text strBase;
	public Text dexBase;
	public Text conBase;
	public Text intBase;
	public Text wisBase;
	public Text chaBase;

	public Text strMod;
	public Text dexMod;
	public Text conMod;
	public Text intMod;
	public Text wisMod;
	public Text chaMod;

	// Update is called once per frame
	void UpdateInv () 
	{
		strBase.text = GameMaster.GM.Data.Strength.ToString();
		dexBase.text = GameMaster.GM.Data.Dexterity.ToString();
		conBase.text = GameMaster.GM.Data.Constitution.ToString();
		intBase.text = GameMaster.GM.Data.Intelligence.ToString();
		wisBase.text = GameMaster.GM.Data.Wisdom.ToString();
		chaBase.text = GameMaster.GM.Data.Charisma.ToString();

		int[] itemStats = grabItemStats();
		strMod.text = itemStats[0].ToString();
		dexMod.text = itemStats[1].ToString();
		conMod.text = itemStats[2].ToString();
		intMod.text = itemStats[3].ToString();
		wisMod.text = itemStats[4].ToString();
		chaMod.text = itemStats[5].ToString();
	}
	int[] grabItemStats()
	{
		int _str = 0;
		int _dex = 0;
		int _con = 0;
		int _int = 0;
		int _wis = 0;
		int _cha = 0;
		int _atk = 0;
		int _def = 0;
		foreach(Item a in GameMaster.GM.Data.Items)
		{
			if(a.Location > -10 && a.Location < -1)
			{
				if(a.ModValues != null)
				{
					_str += a.ModValues[0];
					_dex += a.ModValues[1];
					_con += a.ModValues[2];
					_int += a.ModValues[3];
					_wis += a.ModValues[4];
					_cha += a.ModValues[5];
					_atk += a.ModValues[6];
					_def += a.ModValues[7];
				}
			}
		}
		return new int[]
		{
			_str,
			_dex,
			_con,
			_int,
			_wis,
			_cha,
			_atk,
			_def,
		};
	}
}
