using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryController : MonoBehaviour
{

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
	public Text health;
	public Text magic;
	public Text charName;
	public Text charClass;
	public Text Inventory;
	public Scrollbar invScroll;
	public Text[] Equipped;
	public Button ret;
	public GameInput gi;
	
	public void openInv()
	{
		gameObject.SetActive(true);
		gi.deselectInput();
		ret.onClick.RemoveAllListeners();
		ret.onClick.AddListener(delegate
		{
			gameObject.SetActive(false);
		});

		strBase.text = GameMaster.GM.Data.Strength.ToString();
		dexBase.text = GameMaster.GM.Data.Dexterity.ToString();
		conBase.text = GameMaster.GM.Data.Constitution.ToString();
		intBase.text = GameMaster.GM.Data.Intelligence.ToString();
		wisBase.text = GameMaster.GM.Data.Wisdom.ToString();
		chaBase.text = GameMaster.GM.Data.Charisma.ToString();

		health.text = "Health:\t" + GameMaster.GM.Data.HP + " / " + GameMaster.GM.Data.maxHP; 
		magic.text = "Magic: \t" + GameMaster.GM.Data.MP + " / " + GameMaster.GM.Data.maxMP; 
		charName.text = GameMaster.GM.Data.BasicInfo.Name;
		charClass.text = "Level: " + GameMaster.GM.Data.BasicInfo.Level + "\t" + GameMaster.GM.Data.BasicInfo.CharClass;

		int[] itemStats = grabItemStats();
		strMod.text = itemStats [0].ToString();
		dexMod.text = itemStats [1].ToString();
		conMod.text = itemStats [2].ToString();
		intMod.text = itemStats [3].ToString();
		wisMod.text = itemStats [4].ToString();
		chaMod.text = itemStats [5].ToString();
		string inv = "";
		foreach(Item a in GameMaster.GM.Data.Items)
		{
			if(a.Location == -1)
			{
				inv += "Name: " +a.Name +"\nValue: " + a.Value + " GP\n";
				if(a.CanEquip)
				{
					inv +=
					"str: " + a.ModValues [0] + "\n"+
					"dex: " + a.ModValues [1] + "\n"+
					"con: " + a.ModValues [2] + "\n"+
					"int: " + a.ModValues [3] + "\n"+
					"wis: " + a.ModValues [4] + "\n"+
					"cha: " + a.ModValues [5] + "\n"+
					"atk: " + a.ModValues [6] + "\n"+
					"def: " + a.ModValues [7] + "\n";
				}
				inv += "\n";
			}
		}
		if (inv == "")
		{
			inv = "No items";
		}
		Inventory.text = inv;
		invScroll.value = 1;

		Equipped[0].text = "Head:\t" + findItem(-2);
		Equipped[1].text = "Chest:\t" +  findItem(-3);
		Equipped[2].text = "Hands:\t" +  findItem(-4);
		Equipped[3].text = "Waist:\t" +  findItem(-5);
		Equipped[4].text = "Legs:\t" +  findItem(-6);
		Equipped[5].text = "Feet:\t" +  findItem(-7);
		Equipped[6].text = "Weapon:\t" +  findItem(-8);
		Equipped[7].text = "Trinket:" +  findItem(-9);
	}
	string findItem(int slot)
	{
		if(slot < -1 && slot > -10)
		{
			foreach(Item a in GameMaster.GM.Data.Items)
			{
				if(a.Location == slot)
				{
					return a.Name;
				}
			}
		}
		return "None";
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
		foreach (Item a in GameMaster.GM.Data.Items)
		{
			if(a.Location > -10 && a.Location < -1)
			{
				if(a.ModValues != null)
				{
					_str += a.ModValues [0];
					_dex += a.ModValues [1];
					_con += a.ModValues [2];
					_int += a.ModValues [3];
					_wis += a.ModValues [4];
					_cha += a.ModValues [5];
					_atk += a.ModValues [6];
					_def += a.ModValues [7];
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
