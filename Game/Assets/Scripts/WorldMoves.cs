

using UnityEngine;
using System.Collections.Generic;

public class WorldMoves : MonoBehaviour
{
    public static int totalEnemies = 3;
    public static List<Enemies.Enemy> enemy = new List<Enemies.Enemy>();

    public static void worldMoves()
    {

        for (int i = 0; i<totalEnemies; i++)
        {
            if (enemy [i].location == WorldData.gameData.currentLoc)
            {                     
                if (Mathf.Clamp((int)Mathf.Ceil(Random.Range(0, 9)), 0, 10) <= 7)
                {
                    enemyAction(enemy[i]);
                    break;
                }
            }         


            for (int j = 0; j< (WorldData.gameData.Locations[enemy[i].location].getAdjacentNodes()).Length; j++)
            {
                if (WorldData.gameData.Locations [WorldData.gameData.Locations [enemy [i].location].getAdjacentNodes() [j]].EnemyAtLocation == true)
                    ;
                {
                    if (Mathf.Clamp((int)Mathf.Ceil(Random.Range(0, 1)), 0, 2) == 1)
                    {
                        LocationData.Location a = WorldData.gameData.Locations [enemy [i].location];
                        WorldData.gameData.Locations.Remove(WorldData.gameData.Locations [enemy [i].location]);
                        a.EnemyAtLocation = true;
                        WorldData.gameData.Locations.Add(a);

                        Enemies.Enemy temp = enemy[i];
                        enemy.Remove(enemy[i]);
                        temp.location = WorldData.gameData.Locations [WorldData.gameData.Locations [enemy [i].location].getAdjacentNodes() [j]].getNodeNumber();
                        enemy.Add(temp);

                        a= WorldData.gameData.Locations [enemy [i].location];
                        WorldData.gameData.Locations.Remove(WorldData.gameData.Locations [enemy [i].location]);
                        a.EnemyAtLocation=false;
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
        if (R < 10)
        { 
            if (e.hp <= e.maxhp * .10)
            {
                enemyFlee(e);
                WorldData.gameData.CombatLog = WorldData.gameData.CombatLog + "The enemy has fled with his tail betwixt his legs.";
            }
            else
                R = Mathf.Clamp((int)Mathf.Ceil(Random.Range(9, 100)), 9, 101);
        }
        else if (R > 9 && R < 20)
        {
            if (e.hp <= e.maxhp * .25)
            {
                e.hp = (int)(e.hp + e.maxhp * .2);
                WorldData.gameData.CombatLog = WorldData.gameData.CombatLog + "The enemy has devoured a small, grey kitten and gains 20% health.";
            }
            else
                R = Mathf.Clamp((int)Mathf.Ceil(Random.Range(21, 100)), 21, 101);
        }
        else if (R > 19 && R < 30)
        {
            int damage = (int)(1.5 * EnemyAttack(e));
            WorldData.gameData.CombatLog = WorldData.gameData.CombatLog + " The enemy attacks you and smites you betwixt the eyes for " + damage + ".";
            WorldData.gameData.Stats.setHealth(WorldData.gameData.Stats.getHealth() - damage);
        }
        else
        {
            int damage = EnemyAttack(e);
            WorldData.gameData.CombatLog = WorldData.gameData.CombatLog + "The enemy attacks and deals " + damage + " damage.";
            WorldData.gameData.Stats.setHealth(WorldData.gameData.Stats.getHealth() - damage);
        }
    }
        
    public static int EnemyAttack(Enemies.Enemy enemy)
    {
        const float combatConstant = .01f;
        const float e = 2.71828f;
        int damage = Mathf.Clamp((int)Mathf.Ceil(Random.Range(-3, 3)), -3, 3) + (int)(enemy.attack * ((Mathf.Pow (e,-1 * combatConstant * WorldData.gameData.Stats.Endurance))));

        return damage;  
        
    }

    public static void enemyFlee(Enemies.Enemy en)
    {
        for (int j = 0; j< (WorldData.gameData.Locations[en.location].getAdjacentNodes()).Length; j++)
        {
            if (WorldData.gameData.Locations [WorldData.gameData.Locations [en.location].getAdjacentNodes() [j]].EnemyAtLocation == true)
                ;
            {
                LocationData.Location a = WorldData.gameData.Locations [en.location];
                WorldData.gameData.Locations.Remove(WorldData.gameData.Locations [en.location]);
                a.EnemyAtLocation = true;
                WorldData.gameData.Locations.Add(a);
                
                Enemies.Enemy temp = en;
                enemy.Remove(en);
                temp.location = WorldData.gameData.Locations [WorldData.gameData.Locations [en .location].getAdjacentNodes() [j]].getNodeNumber();
                enemy.Add(temp);
                
                a= WorldData.gameData.Locations [en .location];
                WorldData.gameData.Locations.Remove(WorldData.gameData.Locations [en.location]);
                a.EnemyAtLocation=false;
                WorldData.gameData.Locations.Add(a);
              
                break;
           
            }
        }
    }
}
                                                                  
    


