using UnityEngine;
using System.Collections;

public class LocationData : MonoBehaviour 
{
    [System.Serializable]
	public struct Location
	{
		private int[] adjacentNodes;
		private string name;
		private string description;
		private int nodeNumber;
		private string[] adjacentDirections;


		public Location (string name, string description, int nodeNumber, int[] adjacentNodes, string[] adjacentDirections)

		{
			this.name = name;
			this.description = description;
			this.nodeNumber = nodeNumber;
			this.adjacentNodes = adjacentNodes;
			this.adjacentDirections = adjacentDirections;
		}

		public int getNodeNumber()
		{
			return this.nodeNumber;
		}
		public string getName()
		{
			return this.name;
		}
		public string getDescription()
		{
			return this.description;
		}
		public int[] getAdjacentNodes()
		{
			return this.adjacentNodes;
		}
		public string[] getAdjacentDirections()
		{
			return this.adjacentDirections;
		}

		public void setNodeNumber(int nodeNumber)
		{
			this.nodeNumber = nodeNumber;
		}
		public void setName(string name)
		{
			this.name = name;
		}
		public void setDescription(string description)
		{
			this.description = description;
		}
		public void setAdjacentNodes(int[] adjacentNodes)
		{
			this.adjacentNodes = adjacentNodes;
		}
		public void setAdjacentDirections(string[] adjacentDirections)
		{
			this.adjacentDirections = adjacentDirections;
		}
	}
}
