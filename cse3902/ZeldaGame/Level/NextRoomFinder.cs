using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class NextRoomFinder
    {
        private int[,] mapArray;
        private int row;
        private int column;
        private Dictionary<int, bool> nodesVisited;
        public NextRoomFinder(int[,] mapArray, Dictionary<int, bool> nodesVisited)
        {
            this.mapArray = mapArray;
            this.nodesVisited = nodesVisited;

            // The location of the first room
            row = 5;
            column = 2;
        }

        public bool traverseRoom(string direction)
        {
            bool hasBeenVisited = false;

            switch(direction)
            {
                case "left":
                    column--;
                    break;
                case "right":
                    column++;
                    break;
                case "up":
                    row--;
                    break;
                case "down":
                    row++;
                    break;
            }


            int nextRoom = mapArray[row, column];
            if (!nodesVisited[nextRoom])
            {
                hasBeenVisited = nodesVisited[nextRoom];
                nodesVisited[nextRoom] = true;
            }
            else
            {
                hasBeenVisited = nodesVisited[nextRoom];
            }

            LevelManager.Instance.currentRoom = nextRoom;

            if (ShopManager.Instance.itemToRemove != null)
            {
                ShopManager.Instance.RestockShop();
            }
            return hasBeenVisited;
        }
    }
}
