using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZeldaGame
{
    public class LevelReader
    {
        public XmlReader reader;
        public XmlReaderSettings settings;
        public LevelManager lvlManager;

        public LevelReader(LevelManager levelManager)
        {
            settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            String dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            reader = XmlReader.Create(dir + "//Content//AllRoomsLevelLoader.xml", settings);
            lvlManager = levelManager;
        }

        public void parseOneRoom(int roomNum)
        {
            bool isInRoom = false;

            while (reader.Read())
            {
                if (reader.Name == "Room" + roomNum)
                {
                    isInRoom = true;
                }
                if (isInRoom)
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        string[] locationAndType = reader.Value.Split(' ');
                        Vector2 gameObjectLocation = new Vector2(Convert.ToInt32(locationAndType[0]), Convert.ToInt32(locationAndType[1]));
                        lvlManager.gameObjectLocations.Add(gameObjectLocation);
                        lvlManager.gameObjectNames.Add(locationAndType[2]);
                    }
                    if (reader.Name == "Room" + roomNum && isInRoom && reader.NodeType == XmlNodeType.EndElement)
                    {
                        isInRoom = false;
                    }
                }
            }
        }
    } 
}