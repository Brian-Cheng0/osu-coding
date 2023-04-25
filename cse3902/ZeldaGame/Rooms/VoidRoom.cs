using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class VoidRoom : IRoom
    {
        ISprite sprite;
        public Vector2 Location;
        public VoidRoom()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.VoidRoom);
            Location = new Vector2(0, 172); // currentLocation or Location? does it matter?
        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Location);
        }
    }
}