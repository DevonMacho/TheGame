using UnityEngine;
using System.Collections;

public class LocationData : MonoBehaviour
{
    [System.Serializable]
    public struct Location
    {
        int[] adjacentNodes;
        string name;
        string description;
        int nodeNumber;
        string[] adjacentDirections;
        int reqItem;
        int reqUse;
        int reqOpen;
        int reqClosed;
        int lox;
        int loy;

        public Location(string name, string description, int nodeNumber, int[] adjacentNodes, string[] adjacentDirections, int reqItem, int reqUse, int reqOpen, int reqClosed, int lox, int loy)
        {
            this.name = name;
            this.description = description;
            this.nodeNumber = nodeNumber;
            this.adjacentNodes = adjacentNodes;
            this.adjacentDirections = adjacentDirections;
            this.reqItem = reqItem;
            this.reqUse = reqUse;
            this.reqOpen = reqOpen;
            this.reqClosed = reqClosed;
            this.lox = lox;
            this.loy = loy;
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

        public int ReqItem
        {
            get
            {
                return reqItem;
            }
            set
            {
                reqItem = value;
            }
        }
        
        public int ReqUse
        {
            get
            {
                return reqUse;
            }
            set
            {
                reqUse = value;
            }
        }
        
        public int ReqOpen
        {
            get
            {
                return reqOpen;
            }
            set
            {
                reqOpen = value;
            }
        }
        
        public int ReqClosed
        {
            get
            {
                return reqClosed;
            }
            set
            {
                reqClosed = value;
            }
        }

        public int Loy
        {
            get
            {
                return loy;
            }
        }
        
        public int Lox
        {
            get
            {
                return lox;
            }
        } 
    }
}
