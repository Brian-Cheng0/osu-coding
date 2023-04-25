using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class FrostSword : Sword
    {
        private Sword sword;
        public FrostSword(Sword sword)
        {
            this.sword = sword;

        }
        public override int GetAndSetDamage()
        {
            return sword.damagePerSwing + 1; 
        }
        public override void AdjustUp()
        {
            currentLocation.X += 16;
            currentLocation.Y -= 33;
            sprite = SpriteFactory.Instance.getSprite(Sprite.UpFrostSword);
            directionChosen = true;
        }
        public override void AdjustDown()
        {
            currentLocation.X += 17;
            currentLocation.Y += 44;
            sprite = SpriteFactory.Instance.getSprite(Sprite.DownFrostSword);
            directionChosen = true;
        }
        public override void AdjustRight()
        {
            currentLocation.X += 38;
            currentLocation.Y -= 15;

            sprite = SpriteFactory.Instance.getSprite(Sprite.RightFrostSword);
            directionChosen = true;
        }
        public override void AdjustLeft()
        {
            currentLocation.X -= 26;
            currentLocation.Y -= 15;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LeftFrostSword);
            directionChosen = true;
        }

        public override void AddDecoratorToEnemy(IEnemy enemy)
        {
            if (enemy is not FrostEnemy)
            {
                GameObjectManager.Instance.Remove((GameObject)enemy);
                enemy = new FrostEnemy(enemy);
                GameObjectManager.Instance.Add((GameObject)enemy);
            }
        }

        public override void RemoveDecorator(SwordType swordType)
        {
            GameObjectManager.Instance.mLink.LinkSword = sword;
        }

    }
}
