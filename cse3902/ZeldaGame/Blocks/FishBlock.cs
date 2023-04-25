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
    public class FishBlock : GameObject, IBlock, IDrawable, ICollidable
    {
        public FishBlock()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.FishBlock);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
        }

        public override String GetCollidableType()
        {
            return "Block";
        }
    }
}
