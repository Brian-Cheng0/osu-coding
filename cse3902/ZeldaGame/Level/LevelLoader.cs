using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace ZeldaGame
{
    public class LevelLoader
    {
        int locationIndex;

        Type gameObjectType;
        ConstructorInfo gameObjectInfo;
        GameObject gameObject;

        GameObjectManager objManager;
        LevelManager lvlManager;

        public LevelLoader(LevelManager levelManager)
        {
            objManager = GameObjectManager.Instance;
            lvlManager = levelManager;
        }

        public void LoadGameObjects()
        {
            locationIndex = 0;
            foreach (string gameObjectName in lvlManager.gameObjectNames) 
            {
                getAndSetGameObject(gameObjectName, locationIndex);
                locationIndex++;
            }
        }

        public void getAndSetGameObject(string name, int locationIndex)
        {
            gameObjectType = Type.GetType(name);            
            if (name.Contains("Room") || name.Contains("Basement"))
            {                               
                if (lvlManager.currentRoom == 8)
                {
                    // loads void room instead of normal room template
                    VoidRoom voidRoom = new VoidRoom();
                    voidRoom.Location = lvlManager.gameObjectLocations[locationIndex];
                    objManager.loadedRooms.Add(voidRoom);
                    List<GameObject> shopItems = ShopManager.Instance.LoadAndStockShop();
                    foreach (GameObject item in shopItems)
                    {
                        objManager.Add(item);
                    }
                }
                else if (lvlManager.currentRoom == 18)
                {
                    Basement basement = new Basement();
                    basement.Location = lvlManager.gameObjectLocations[locationIndex];
                    objManager.loadedRooms.Add(basement);
                }
                else
                {
                    StandardRoom dungeonRoom = new StandardRoom();
                    dungeonRoom.Location = lvlManager.gameObjectLocations[locationIndex];
                    objManager.loadedRooms.Add(dungeonRoom);
                }                
            }
            else
            {
                gameObjectInfo = gameObjectType.GetConstructor(Type.EmptyTypes);
                gameObject = (GameObject)gameObjectInfo.Invoke(null);

                objManager.Add(gameObject);
                gameObject.currentLocation = lvlManager.gameObjectLocations[locationIndex];
            }
        }
    }
}
