using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightScreen : MonoBehaviour
{


	public Button att;
	public Button def;
	public Button magicM;
	public Button Heal;
	public Image enemy;
	public GameInput panel;

	public GameObject window;
	public Text windTxt;
	public Button ret;

	public Text health;
	public Text magic;
	string move = "";
	void Awake()
	{

	}

	public void startFight()
	{
		setButtons();
		panel.deselectInput();
		gameObject.SetActive(true);
	}
	void endFight()
	{
		gameObject.SetActive(false);
	}
	void enemyTurn()
	{
		setButtons();
		window.SetActive(true);

		windTxt.text = move + "\n" + enemyMove();
		if(GameMaster.GM.Data.HP <= 0)
		{
			ret.onClick.RemoveAllListeners();
			ret.onClick.AddListener(delegate
			{
				Application.LoadLevel("GameOver");
			});

		}
		health.text = "Health:\t" +GameMaster.GM.Data.HP + " / " + GameMaster.GM.Data.maxHP;
		magic.text = "Magic:\t" +GameMaster.GM.Data.MP + " / " + GameMaster.GM.Data.maxMP;
	}
	string enemyMove()
	{
		int tick = 0;
		foreach(Enemy a in GameMaster.GM.Data.Enemies)
		{
			tick++;
			if(a.Location == GameMaster.GM.Data.Node)
			{
				if(a.HP <= 0)
				{
					ret.onClick.RemoveAllListeners();
					ret.onClick.AddListener(delegate
					{
						if(a.Name.ToLower() == "Kraymoar".ToLower())
						{
							Application.LoadLevel("Win");
						}
						endFight();
					});
					GameMaster.GM.Data.Enemies[tick-1].IsAlive = false;
					return a.Name + " has died.";
				}
				else
				{
					int rand = Random.Range(0,100);
					if(rand %10 <= 4)
					{
						return a.Name + " has missed.";
					}
					else if(rand %10 > 4 && rand %10 < 9)
					{
						int hit = Random.Range(1,(int)(a.MaxAttack/2));
						GameMaster.GM.Data.HP -= hit;
						return a.Name + " has hit you dealing " + hit +  " points of damage.";
					}
					else
					{
						int hit = Random.Range((int)(a.MaxAttack/2),((a.MaxAttack+1)));
						GameMaster.GM.Data.HP -= hit;
						return a.Name + " has critically hit you dealing " + hit +  " points of damage.";
					}
				}
			}
		}

		return "Guru Meditation 0x0004";
	}
	void setButtons()
	{
		health.text = "Health:\t" +GameMaster.GM.Data.HP + " / " + GameMaster.GM.Data.maxHP;
		magic.text = "Magic:\t" +GameMaster.GM.Data.MP + " / " + GameMaster.GM.Data.maxMP;
		att.onClick.RemoveAllListeners();
		def.onClick.RemoveAllListeners();
		magicM.onClick.RemoveAllListeners();
		Heal.onClick.RemoveAllListeners();
		ret.onClick.RemoveAllListeners();

		ret.onClick.AddListener(delegate
		{
			window.SetActive(false);
		});

		att.onClick.AddListener(delegate
		{
			attack();
			enemyTurn();
		});
		def.onClick.AddListener(delegate
		{
			defend();
			enemyTurn();
		});
		if(GameMaster.GM.Data.MP > 10)
		{
			magicM.onClick.AddListener(delegate
			{
				magicMissile();
				enemyTurn();
			});
		}
		foreach (Item a in GameMaster.GM.Data.Items)
		{
			if(a.Consumable && a.Location == -1 && a.Uses >= 1)
			{
				Heal.onClick.AddListener(delegate
				{
					usePotion();
					enemyTurn();
				});
			}
		}
	}

	void attack()
	{
		if(doIHit(true))
		{
			int tick = 0;
			foreach(Enemy a in GameMaster.GM.Data.Enemies)
			{
				tick++;
				if(a.Location == GameMaster.GM.Data.Node)
				{
					break;
				}
				
			}
			int hit = 10;
			GameMaster.GM.Data.Enemies[tick-1].HP -= hit;
			move = "You hit the enemy for " + hit + " damage.\n";
			return;
		}
		move = "You miss the enemy.";
	}

	void defend()
	{

	}

	void magicMissile()
	{
		GameMaster.GM.Data.MP -= 10;
		if(doIHit(false))
		{
			int tick = 0;
			foreach(Enemy a in GameMaster.GM.Data.Enemies)
			{
				tick++;
				if(a.Location == GameMaster.GM.Data.Node)
				{
					break;
				}
				
			}
			int hit = 10;
			GameMaster.GM.Data.Enemies[tick-1].HP -= hit;
			move = "You hit the enemy for " + hit + " damage.\n";
			return;
		}
		move = "You miss the enemy.";
	}

	void usePotion()
	{
		int tick = 0;
		foreach (Item a in GameMaster.GM.Data.Items)
		{
			tick++;
			if(a.Consumable && a.Location == -1 && a.Uses >= 1 && a.CanModifyStats)
			{
				GameMaster.GM.Data.Items[tick-1].Uses -= 1;

				GameCommands.useItem(a);
				move = "You used a potion and healed "+ a.ModValues[8] +" points.";
				return;
			}
		}
	}

	bool doIHit(bool attack)
	{

		if(attack)
		{
			int roll = Random.Range((int)(GameMaster.GM.Data.Strength / 2), 21);
			if(roll > 10)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			int roll = Random.Range((int)(GameMaster.GM.Data.Wisdom / 2), 21);
			if(roll > 10)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
