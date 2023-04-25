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
    public class WaterBlock : GameObject, IBlock, IDrawable, ICollidable
    {
        public WaterBlock()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.WaterBlock);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
        }

        public override String GetCollidableType()
        {
            return "WaterBlock"; // Water collides differenty. Player/Enemies cant pass through it but items can
        }
    }
}
 