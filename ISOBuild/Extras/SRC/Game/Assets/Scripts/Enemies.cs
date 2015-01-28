using UnityEngine;
using System;
using System.Collections.Generic;

public class Enemies : MonoBehaviour
{
    [Serializable]
    public struct Enemy
    {
      public  string name;
       public string description;
      public  int hp;
      public  int maxhp;
     public   int attack;
       public int armor;
       public int location;
        
        public int Maxhp
        {
            get
            {
                return maxhp;
            }
            set
            {
                maxhp = value;
            }
        }
        
        public  Enemy(string name, string description, int attack, int armor, int hp,int location)
        {
            this.name = name;
            this.description = description;
            this.attack = attack;
            this.armor = armor;
            this.hp = hp;
            this.maxhp = 100;
            this.location=location;      
        }
        
        
    }
    
}

