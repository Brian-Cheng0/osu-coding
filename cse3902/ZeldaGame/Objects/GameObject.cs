using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public abstract class GameObject
    {
        public ISprite sprite;
        public Vector2 currentLocation;

        public Vector2 Location
        {
            get
            {
                return currentLocation;
            }
            set
            {
                currentLocation = value; // what is value? it works?
            }
        }
        public Vector2 Size
        {
            get
            {
                // ask sprite how big am I right now

                return sprite.Size;
            }

        }
         
        public virtual void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Location); // make it current location?
        }

        public abstract String GetCollidableType();

       
    }
}
