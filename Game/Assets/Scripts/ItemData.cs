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

        public Container(string name, string description, int location, int openState)
        {
            contents = new List<Item>();
            this.name = name;
            this.description = description;
            this.location = location;
            this.openState = openState;
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
    }
    [System.Serializable]
    public struct Item
    {
        private int weight;
        private string name;
        private string description;
        private int location;
        //user's inventory is -1
        //user's body is -2
        private int openState;
        // item can not be opened -1
        // item is closed 0
        // item is open 1
        public Item(string name, string description, int location, int weight,int openState)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            this.weight = weight;
            this.openState = openState;
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
    }
}
