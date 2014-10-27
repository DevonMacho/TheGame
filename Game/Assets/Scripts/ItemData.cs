using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemData : MonoBehaviour
{
    [System.Serializable]
    public struct Container
    {
        private List<Item> contents;
        private string name;
        private string description;
        private int location;
        private int openState;
        private int itemType;


        public Container(string name, string description, int location, int openState, int itemType)
        {
            contents = new List<Item>();
            this.name = name;
            this.description = description;
            this.location = location;
            this.openState = openState;
            this.itemType = itemType;
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
        public void addItem(Item item)
        {
            this.contents.Add(item);
        }
        public void removeItem(Item item)
        {
            this.contents.Remove(item);
        }
        public List<Item> getItems()
        {
            return this.contents;
        }
        public int getItemType()
        {
            return this.itemType;
        }
        public void setItemType(int itemType)
        {
             this.itemType = itemType;
        }
    }
    [System.Serializable]
    public struct Item
    {
        private int weight;
        private string name;
        private string description;
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
        private int usesLeft;
        // -1 unlimited
        //  0 depleted
        //  1+ useable
        public Item(string name, string description, int location, int weight,int openState, int itemType,int usesLeft)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            this.weight = weight;
            this.openState = openState;
            this.itemType = itemType;
            this.usesLeft = usesLeft;
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
