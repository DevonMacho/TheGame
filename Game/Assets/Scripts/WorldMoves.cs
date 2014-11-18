

using UnityEngine;
using System.Collections.Generic;

public class WorldMoves : MonoBehaviour
{
    public static int totalEnemies;

    public static void initEnemyList()
    {
        WorldData.gameData.Enemy = new List<Enemies.Enemy>();
        WorldData.gameData.Enemy .Add(new Enemies.Enemy("Electric Elemental", "Shocky Shock Shock!!!", 20, 5, 40, 2));
        WorldData.gameData.Enemy .Add(new Enemies.Enemy("Electric Elemental", "Shocky Shock Shock!!!", 20, 5, 40, 5));
        WorldData.gameData.Enemy .Add(new Enemies.Enemy("Electric Elemental", "Shocky Shock Shock!!!", 20, 10, 60, 8));
        WorldData.gameData.Enemy .Add(new Enemies.Enemy("Electric Elemental", "Shocky Shock Shock!!!", 20, 35, 50, 17));
        WorldData.gameData.Enemy .Add(new Enemies.Enemy("Electric Elemental", "Shocky Shock Shock!!!", 0, 5, 40, 19));
        WorldData.gameData.Enemy .Add(new Enemies.Enemy("Kraymoar", "Destroyer of Uranus", 40, 25, 150, 23));
    }

    public static void worldMoves()
    {
        totalEnemies = WorldData.gameData.Enemy .Count;
        for (int i = 0; i < totalEnemies; i++)
        {
            //Debug.Log("before");
            if (WorldData.gameData.Enemy [i].location == WorldData.gameData.currentLoc)
                            {                     

                //Debug.Log("after");
                WorldData.gameData.CombatLog += "You see a hostile "+WorldData.gameData.Enemy [i].name+" \n" ;
                enemyAction(WorldData.gameData.Enemy [i]);
                break;

            }         


            for (int j = 0; j< WorldData.gameData.Locations[WorldData.gameData.Enemy [i].location].getAdjacentNodes().Length; j++)
            {
                if (WorldData.gameData.Locations [WorldData.gameData.Locations [WorldData.gameData.Enemy [i].location].getAdjacentNodes() [j]].EnemyAtLocation == false)
                {
                    if (Mathf.Clamp((int)Mathf.Ceil(Random.Range(0, 1)), 0, 2) == 1)
                    {
                        LocationData.Location a = WorldData.gameData.Locations [WorldData.gameData.Enemy [i].location];
                        WorldData.gameData.Locations.Remove(WorldData.gameData.Locations [WorldData.gameData.Enemy [i].location]);
                        a.EnemyAtLocation = true;
                        WorldData.gameData.Locations.Add(a);

                        Enemies.Enemy temp = WorldData.gameData.Enemy [i];
                        WorldData.gameData.Enemy .Remove(WorldData.gameData.Enemy [i]);
                        temp.location = WorldData.gameData.Locations [WorldData.gameData.Locations [WorldData.gameData.Enemy [i].location].getAdjacentNodes() [j]].getNodeNumber();
                        WorldData.gameData.Enemy .Add(temp);

                        a = WorldData.gameData.Locations [WorldData.gameData.Enemy [i].location];
                        WorldData.gameData.Locations.Remove(WorldData.gameData.Locations [WorldData.gameData.Enemy [i].location]);
                        a.EnemyAtLocation = false;
                        WorldData.gameData.Locations.Add(a);
                        break;
                    }


                }
            }

            
        }
    }

    public static void enemyAction(Enemies.Enemy e)
    {
            
        int R = Mathf.Clamp((int)Mathf.Ceil(Random.Range(0, 100)), 0, 101);
       // Debug.Log(R);
        if (R < 10)
        { 
            //Debug.Log(1);
            if (e.hp <= e.maxhp * .10)
            {
                enemyFlee(e);
                WorldData.gameData.CombatLog = WorldData.gameData.CombatLog + "The enemy has fled with his tail betwixt his legs.";
            }
            else
                enemyAction(e);
        }
        else if (R > 9 && R < 20)
        {
           // Debug.Log(2);
            if (e.hp <= e.maxhp * .25)
            {
                e.hp = (int)(e.hp + e.maxhp * .2);
                WorldData.gameData.CombatLog = WorldData.gameData.CombatLog + "The enemy has devoured a small, grey kitten and gains 20% health.";
            }
            else
                enemyAction(e);
        }
        else if (R > 19 && R < 30)
        {
            //Debug.Log(3);
            int damage = (int)(1.5 * EnemyAttack(e));
            WorldData.gameData.CombatLog = WorldData.gameData.CombatLog + "The enemy attacks you and smites you betwixt the eyes for " + damage + ".";
            WorldData.gameData.Stats.setHealth(WorldData.gameData.Stats.getHealth() - (damage));
        }
        else
        {
           // Debug.Log(4);
            int damage = EnemyAttack(e);
            WorldData.gameData.CombatLog = WorldData.gameData.CombatLog + "The enemy attacks and deals " + damage + " damage.";
            WorldData.gameData.Stats.setHealth(WorldData.gameData.Stats.getHealth() - (damage));
        }
    }
        
    public static int EnemyAttack(Enemies.Enemy enemy)
    {
        const float combatConstant = .01f;
        const float e = 2.71828f;
        int damage = Mathf.Clamp((int)Mathf.Ceil(Random.Range(0, (int)(enemy.attack * ((Mathf.Pow(e, -1 * combatConstant / ((WorldData.gameData.TotalEndurance / 2) * (WorldData.gameData.TotalStrength/2))   )))))), 0, (int)(enemy.attack * ((Mathf.Pow(e, -1 * combatConstant / ((WorldData.gameData.TotalEndurance / 2) * (WorldData.gameData.TotalStrength/2) + WorldData.gameData.Armor))))));


        return damage;  
        
    }

    public static void enemyFlee(Enemies.Enemy en)
    {
        for (int j = 0; j< WorldData.gameData.Locations[en.location].getAdjacentNodes().Length; j++)
        {
            if (WorldData.gameData.Locations [WorldData.gameData.Locations [en.location].getAdjacentNodes() [j]].EnemyAtLocation == false)
            {
                LocationData.Location a = WorldData.gameData.Locations [en.location];
                WorldData.gameData.Locations.Remove(WorldData.gameData.Locations [en.location]);
                a.EnemyAtLocation = true;
                WorldData.gameData.Locations.Add(a);
                
                Enemies.Enemy temp = en;
                WorldData.gameData.Enemy .Remove(en);
                temp.location = WorldData.gameData.Locations [WorldData.gameData.Locations [en .location].getAdjacentNodes() [j]].getNodeNumber();
                WorldData.gameData.Enemy .Add(temp);
                
                a = WorldData.gameData.Locations [en .location];
                WorldData.gameData.Locations.Remove(WorldData.gameData.Locations [en.location]);
                a.EnemyAtLocation = false;
                WorldData.gameData.Locations.Add(a);
              
                break;
           
            }
        }
    }
}
                                                                  
    


