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
    public class VerticalBarrier : GameObject, IBlock, IDrawable, ICollidable
    {
        public VerticalBarrier()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.InvisVerticalBarrier);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?       
        }

        public override string GetCollidableType()
        {
            return "Block"; // Will always collide like a block
        }
    }
}