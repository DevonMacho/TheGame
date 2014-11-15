using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemData : MonoBehaviour
{
    [System.Serializable]
    public struct Container
    {
        //private List<Item> contents;
        private string name;
        private string description;
        private int location;
        private int openState;
        private int[] items;

        public Container(string name, string description, int location, int openState, int[] items)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            this.openState = openState;
            this.items = items;
        }

        public string getName()
        {
            return this.name; 
        }

        public string getDescription()
        {
            return this.description; 
        }

        public int getLocation()
        {
            return this.location;
        }

        public int getOpenState()
        {
            return this.openState;
        }

        public void setLocation(int location)
        {
            this.location = location;
        }

        public void setOpenState(int openState)
        {
            this.openState = openState;
        }

       
        public int[] Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }
    }

    [System.Serializable]
    public struct Item
    {
        private int weight;
        //If an item's weight is above 999 it can not be moved
        private string name;
        //Duh ^
        private string description;
        //Duh ^
        private int location;
        //user's inventory is -1
        //user's equipment is -2 - -7
        private int openState;
        // item can not be opened -1
        // item is closed 0
        // item is open 1
        private int itemType;
        // 0 not usable
        // 1 usable
        // 2 head
        // 3 chest (body)
        // 4 gauntlets
        // 5 belt
        // 6 pants
        // 7 shoes
        // 8 weapon in use

        private int usesLeft;
        // -1 unlimited
        //  0 depleted
        //  1+ useable

        //start
        int s;
        int p;
        int e;
        int a;
        int l;
        //end - equipped modifiers
        //start
        int rs;
        int rp;
        int re;
        int ra;
        int rl;
        //end - required stats for item
        int armor;
        //armor modifier
        int attack;
        //attack modifier
        int id;

       

        public int S
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
            }
        }

        public int P
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
            }
        }

        public int E
        {
            get
            {
                return e;
            }
            set
            {
                e = value;
            }
        }

        public int A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }

        public int L
        {
            get
            {
                return l;
            }
            set
            {
                l = value;
            }
        }
        public int Rs
        {
            get
            {
                return rs;
            }
            set
            {
                rs = value;
            }
        }
        
        public int Rp
        {
            get
            {
                return rp;
            }
            set
            {
                rp = value;
            }
        }
        
        public int Re
        {
            get
            {
                return re;
            }
            set
            {
                re = value;
            }
        }
        
        public int Ra
        {
            get
            {
                return ra;
            }
            set
            {
                ra = value;
            }
        }
        
        public int Rl
        {
            get
            {
                return rl;
            }
            set
            {
                rl = value;
            }
        }
        
        public int Armor
        {
            get
            {
                return armor;
            }
            set
            {
                armor = value;
            }
        }
        
        public int Attack
        {
            get
            {
                return attack;
            }
            set
            {
                attack = value;
            }
        }
        
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        //the item's ID
        // negative ID's mean that they are one of a kind
        //positive ID's spawn normally from enemies / found in boxes

        public Item(string name, string description, int location, int weight, int openState, int itemType, int usesLeft, int s, int p, int e, int a, int l, int rs, int rp, int re, int ra, int rl, int armor, int attack, int id)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            this.weight = weight;
            this.openState = openState;
            this.itemType = itemType;
            this.usesLeft = usesLeft;
            this.s = s;
            this.p = p;
            this.e = e;
            this.a = a;
            this.l = l;
            this.rs = rs;
            this.rp = rp;
            this.re = re;
            this.ra = ra;
            this.rl = rl;
            this.armor = armor;
            this.attack = attack;
            this.id = id;
        }

        public int getWeight()
        {
            return this.weight;
        }

        public string getName()
        {
            return this.name;
        }

        public string getDescription()
        {
            return this.description;
        }

        public int getLocation()
        {
            return this.location;
        }

        public void setLocation(int location)
        {
            this.location = location;
        }

        public int getOpenState()
        {
            return this.openState;
        }

        public void setOpenState(int openState)
        {
            this.openState = openState;
        }

        public int getItemType()
        {
            return this.itemType;
        }

        public void setItemType(int itemType)
        {
            this.itemType = itemType;
        }

        public void setUsesLeft(int usesLeft)
        {
            this.usesLeft = usesLeft;
        }

        public int getUsesLeft()
        {
            return this.usesLeft;
        }
    }
}
