using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class FlameSword : Sword
    {
        private Sword sword;
        public FlameSword(Sword sword)
        {
            this.sword = sword;
            sprite = SpriteFactory.Instance.getSprite(Sprite.UpFlameSword);
           
        }
        public override int GetAndSetDamage()
        {
            return sword.damagePerSwing; // Flame does same damage as normal sword, but in Update() has flame infliction
        }
        public override void AdjustUp() 
        {
            currentLocation.X += 16;
            currentLocation.Y -= 33;
            sprite = SpriteFactory.Instance.getSprite(Sprite.UpFlameSword);
            directionChosen = true;
        }
        public override void AdjustDown()
        {
            currentLocation.X += 17;
            currentLocation.Y += 44;
            sprite = SpriteFactory.Instance.getSprite(Sprite.DownFlameSword);
            directionChosen = true;
        }
        public override void AdjustRight()
        {
            currentLocation.X += 38;
            currentLocation.Y -= 15;
            sprite = SpriteFactory.Instance.getSprite(Sprite.RightFlameSword);
            directionChosen = true;
        }
        public override void AdjustLeft()
        {
            currentLocation.X -= 26;
            currentLocation.Y -= 15;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LeftFlameSword);
            directionChosen = true;
        }
        public override void AddDecoratorToEnemy(IEnemy enemy)
        {
            if (enemy is not IgnitedEnemy) 
            {
                GameObjectManager.Instance.Remove((GameObject)enemy);              
                enemy = new IgnitedEnemy(enemy);
                GameObjectManager.Instance.Add((GameObject)enemy);
            }        
        }

        public override void RemoveDecorator(SwordType swordType)
        {
            GameObjectManager.Instance.mLink.LinkSword = sword;
        }
    }
}
