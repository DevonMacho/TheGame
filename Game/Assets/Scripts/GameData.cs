using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData : MonoBehaviour
{

    public class GameInformation
    {

        public List<LocationData.Location> locations = new List<LocationData.Location>();
        public List<ItemData.Item> items = new List<ItemData.Item>();
        //going public due to lazy ^

        string playerName;
        string playerGender;
        string playerClass;

        public GameInformation(List<LocationData.Location> locations, List<ItemData.Item> items, string playerName, string playerGender, string playerClass)
        {
            this.locations = locations;
            this.items = items;
            this.playerName = playerName;
            this.playerGender = playerGender;
            this.playerClass = playerClass;
        }

        public void serialize(GameInformation saveData, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/SaveGames/" + fileName);
            formatter.Serialize(file, saveData);
        }

        public GameInformation deserialize(string fileName)
        {
            if (File.Exists(Application.persistentDataPath + "/SaveGames/" + fileName))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/SaveGames/" + fileName, FileMode.Open);
                GameInformation a = (GameInformation)formatter.Deserialize(file);
                file.Close();
                return a;
            }
            else
            {
                return null;
            }
        }

        public string getName()
        {
            return this.playerName;
        }
        public string getGender()
        {
            return this.playerGender;
        }
        public string getClass()
        {
            return this.playerName;
        }

        public void getName(string playerName)
        {
            this.playerName = playerName;
        }
        public void getGender(string playerGender)
        {
            this.playerGender = playerGender;
        }
        public void getClass(string playerClass)
        {
            this.playerName = playerClass;
        }
        public void addItem(string name, string description, int location, int weight, int openState)
        {
            items.Add(new ItemData.Item(name, description, location, weight, openState));
        }
        public void addLocation(string name, string description, int nodeNumber, int[] adjacentNodes, string[] adjacentDirections)
        {
            locations.Add(new LocationData.Location(name, description, nodeNumber, adjacentNodes, adjacentDirections));
        }
    }
}
