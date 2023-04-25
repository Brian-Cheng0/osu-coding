using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class InventoryScreen : IUpdatable, IDrawable
    {
        public ISprite sprite;
        public Vector2 displacementFromCamera;

        public InventoryScreen()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.InventoryOverhead);
            displacementFromCamera = new Vector2(0, 0);
        }
        
        public void Update(GameTime gameTime)
        {
            displacementFromCamera = UIManager.Instance.baseLocation + new Vector2(0, -520);
            sprite.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, displacementFromCamera);
        }
    }
}
