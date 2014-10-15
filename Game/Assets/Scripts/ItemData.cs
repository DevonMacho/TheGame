using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour
{

    public struct Item
    {
        private int weight;
        private string name;
        private string description;
        private int location;
        //user's inventory is -1
        //user's body is -2
        public Item(string name, string description, int location, int weight)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            this.weight = weight;
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

    }
}
