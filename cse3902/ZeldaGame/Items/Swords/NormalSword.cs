using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;
using System.Diagnostics;

namespace ZeldaGame
{
    public class NormalSword : Sword
    {
        

        private GameObjectManager objectManager;
        private MasterLink link;
        public bool InUse { get; set; }

        private Vector2 linkSize;

        public NormalSword(MasterLink link)
        {
            this.link = link;
            objectManager = link.objectManager;
            InUse = false;

            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkSwordDown); // Initialize default sprite
            currentLocation = link.currentLocation;
            direction = link.currentDirection;
            linkSize = link.Size; // Figure out how to use this instead of hardcoding values

         //   SwordType = SwordType.Normal;

        }

        // Update method from AbstractSword

        // Draw method from GameObject

        public override int GetAndSetDamage()
        {
            return damagePerSwing;
        }
        public override void AdjustUp()
        {
            currentLocation.X += 16;
            currentLocation.Y -= 33;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkSwordUp);
            directionChosen = true;
        }
        public override void AdjustDown()
        {
            currentLocation.X += 17;
            currentLocation.Y += 44;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkSwordDown);
            directionChosen = true;
        }
        public override void AdjustLeft()
        {
            currentLocation.X -= 36;
            currentLocation.Y += 16;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkSwordLeft);
            directionChosen = true;
        }
        public override void AdjustRight()
        {
            currentLocation.X += 38;
            currentLocation.Y += 14;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkSwordRight);
            directionChosen = true;
        }

        public override void AddDecoratorToEnemy(IEnemy enemy)
        {
            // Does not add decorator
        }
        public override void RemoveDecorator(SwordType swordType)
        {
            // No decorator to remove 
        }
    }
}

