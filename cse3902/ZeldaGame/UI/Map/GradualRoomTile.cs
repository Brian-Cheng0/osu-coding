using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class GradualRoomTile : IUpdatable, IDrawable
    {
        private ISprite tileSprite;
        private Vector2 tileLocation;
        private Vector2 displacementOnMap;

        private ISprite playerPinSprite;
        private Vector2 playerPinLocation;

        public int roomNumber;
        public GradualRoomTile(Vector2 displacementOnMap, int roomNumber)
        {
            tileSprite = SpriteFactory.Instance.getSprite(Sprite.MapRoom);
            tileLocation = Vector2.Zero;
            playerPinLocation = Vector2.Zero;
            this.displacementOnMap = displacementOnMap;

            this.roomNumber = roomNumber;

            playerPinSprite = SpriteFactory.Instance.getSprite(Sprite.PlayerPin);
        }
        public void Update(GameTime gameTime)
        {
            tileLocation = UIManager.Instance.baseLocation + displacementOnMap;

            tileSprite.Update(gameTime);
            if (roomNumber == LevelManager.Instance.currentRoom)
            {
                playerPinLocation = UIManager.Instance.baseLocation + displacementOnMap + new Vector2(8, 8);
                playerPinSprite.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // If player has picked up the map, display the blue room tiles for the minimap
            tileSprite.Draw(spriteBatch, tileLocation);

            // Only draw the player pin in the room the player is in
            if (roomNumber == LevelManager.Instance.currentRoom)
            {
                playerPinSprite.Draw(spriteBatch, playerPinLocation);
            }
        }
        
    }
}
