using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeldaGame
{

    public class MapDisplay : IUpdatable, IDrawable
    {

        private static float BLUE_X_SPACING = 22;
        private static float BLUE_Y_SPACING = 12;
        private static Vector2 BLUE_START_POINT = new Vector2(75, 75);
        private static float GRADUAL_SPACING = 30;
        private static Vector2 GRADUAL_START_POINT = new Vector2(400, -220);

        private int[,] mapArray;

        private ISprite mapIconSprite;
        private Vector2 mapIconLocation;

        private Vector2 bossRoomSprite;
        private Vector2 bossRoomLocation;

        private ISprite playerPinSprite;
        private Vector2 playerPinLocation;

        private Vector2 blueTileLocation;
        private Vector2 gradualTileLocation;

        private List<BlueRoomTile> allRoomsMap;
        private List<GradualRoomTile> discoveredRoomsMap;

        public MapDisplay()
        {
            mapArray = LevelManager.Instance.mapArray;

            allRoomsMap = new List<BlueRoomTile>();
            discoveredRoomsMap = new List<GradualRoomTile>();

            mapIconSprite = SpriteFactory.Instance.getSprite(Sprite.Map);
            mapIconLocation = new Vector2(145, -186); 

            

            playerPinSprite = SpriteFactory.Instance.getSprite(Sprite.PlayerPin);
            playerPinLocation = BLUE_START_POINT += new Vector2(6, 0);

            blueTileLocation = BLUE_START_POINT;
            gradualTileLocation = GRADUAL_START_POINT;
        }

        public void LoadInitialMaps()
        {
            UIManager.Instance.Add(this);
            PlaceAllMapRoomTiles(); // Initalizes mini map, doesn't draw yet
        }
   
      
        public void PlaceAllMapRoomTiles()
        { 
            int rowLength = mapArray.GetLength(0); // Number of rows in array
            int columnLength = mapArray.GetLength(1); // Number of columns in array
            
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < columnLength; j++)
                {
                    if (mapArray[i, j] > 0) // There is a room at this location, initialize it
                    {
                        allRoomsMap.Add(new BlueRoomTile(blueTileLocation, mapArray[i, j]));
                        discoveredRoomsMap.Add(new GradualRoomTile(gradualTileLocation, mapArray[i, j]));                        
                    }
                    blueTileLocation.X += BLUE_X_SPACING; // If on a new column, draw next tile x amount to the right
                    gradualTileLocation.X += GRADUAL_SPACING;
                }
                blueTileLocation.X = BLUE_START_POINT.X;
                gradualTileLocation.X = GRADUAL_START_POINT.X;

                blueTileLocation.Y += BLUE_Y_SPACING; // If on a new row, draw next tile y amount down 
                gradualTileLocation.Y += GRADUAL_SPACING;
            }
            
        }
     
        public void Update(GameTime gameTime)
        {
            mapIconLocation = UIManager.Instance.baseLocation + new Vector2(145, -186);
            mapIconSprite.Update(gameTime);

            // Minimap
            foreach (BlueRoomTile roomTile in allRoomsMap)
            {              
                roomTile.Update(gameTime);
            }
            // Large map
            foreach (GradualRoomTile roomTile in discoveredRoomsMap.ToList())
            {
                roomTile.Update(gameTime);
                
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (UIManager.Instance.mapIsActive)
            {
                mapIconSprite.Draw(spriteBatch, mapIconLocation);
            }
               
            foreach (BlueRoomTile roomTile in allRoomsMap)
            {
                roomTile.Draw(spriteBatch);
            }

            foreach (GradualRoomTile roomTile in discoveredRoomsMap)
            {
                if (LevelManager.Instance.nodesVisited[roomTile.roomNumber])
                {
                    roomTile.Draw(spriteBatch);
                }                         
            }
        }
    }
}
