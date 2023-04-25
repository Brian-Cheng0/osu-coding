using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeldaGame
{
    public class CompassDisplay : IUpdatable, IDrawable
    {
        private ISprite compassIconSprite;
        private ISprite bossRoomPinSprite;

        private Vector2 compassIconLocation;
        private Vector2 bossRoomLocation;

        private Vector2 compassDisplacementFromCamera;
        private Vector2 pinDisplacementFromCamera;
        public CompassDisplay()
        {
            compassIconSprite = SpriteFactory.Instance.getSprite(Sprite.Compass);
            bossRoomPinSprite = SpriteFactory.Instance.getSprite(Sprite.BossRoomPin);

            compassIconLocation = Vector2.Zero;
            bossRoomLocation = Vector2.Zero;

            compassDisplacementFromCamera = new Vector2(140, -70);
            pinDisplacementFromCamera = new Vector2(175, 87);

        }       

        public void Update(GameTime gameTime)
        {
            compassIconLocation = UIManager.Instance.baseLocation + compassDisplacementFromCamera;
            bossRoomLocation = UIManager.Instance.baseLocation + pinDisplacementFromCamera;

            compassIconSprite.Update(gameTime);
            bossRoomPinSprite.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (UIManager.Instance.compassIsActive)
            {
                compassIconSprite.Draw(spriteBatch, compassIconLocation);
                bossRoomPinSprite.Draw(spriteBatch, bossRoomLocation);
            }        
        }
    }
}
