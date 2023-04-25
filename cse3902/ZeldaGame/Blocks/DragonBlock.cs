using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class DragonBlock : GameObject, IBlock, IDrawable, ICollidable
    {
        public DragonBlock()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.DragonBlock);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
        }

        public override String GetCollidableType()
        {
            return "Block";
        }
    }
}
