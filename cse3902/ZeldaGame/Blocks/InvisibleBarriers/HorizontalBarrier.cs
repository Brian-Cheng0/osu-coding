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
    public class HorizontalBarrier : GameObject, IBlock, IDrawable, ICollidable
    {
        public HorizontalBarrier()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.InvisHorizontalBarrier);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?       
        }
        public override string GetCollidableType()
        {
            return "Block"; // Will always collide like a block
        }
    }
}