using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class PrimaryItem : IUpdatable, IDrawable
    {

        private ISprite sprite;
        private Vector2 Location;
        private Vector2 displacementFromCamera;
        public PrimaryItem()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.SwordDisplay); // The primary weapon will always be the sword
            Location = Vector2.Zero;
            displacementFromCamera = new Vector2(453, 69);
        }

        public void Update(GameTime gameTime)
        {
            if (GameObjectManager.Instance.mLink.LinkSword is FlameSword)
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.FlamingSwordDisplay);
            }
            else if (GameObjectManager.Instance.mLink.LinkSword is FrostSword)
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.FrostSwordDisplay);
            }
            //else if (GameObjectManager.Instance.mLink.LinkSword is LightingSword)
            //{
            //    sprite = SpriteFactory.Instance.getSprite(Sprite.LightingSwordDisplay);
            //}
            
            Location = UIManager.Instance.baseLocation + displacementFromCamera;
            sprite.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Location);
        }
   
    }
}
