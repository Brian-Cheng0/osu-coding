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
    
    public class ReinforcedSword : Sword
    {
        private Sword sword;
        public ReinforcedSword(Sword sword)
        { 
            this.sword = sword;
            sprite = sword.sprite;

            // Reinforced sword does double damage
            damagePerSwing = sword.damagePerSwing * 2;
        }
        public override int GetAndSetDamage()
        {
            return damagePerSwing;
        }
        public override void AdjustUp()
        {
            // Get previous swords sprite
            sword.AdjustUp();

            sprite = sword.sprite;
            currentLocation.X += 16;
            currentLocation.Y -= 33;

            directionChosen = true;
        }
        public override void AdjustDown()
        {
            sword.AdjustDown();

            sprite = sword.sprite;
            currentLocation.X += 17;
            currentLocation.Y += 44;
            directionChosen = true;
        }
        public override void AdjustRight()
        {
            sword.AdjustRight();

            sprite = sword.sprite;
            currentLocation.X += 38;
            currentLocation.Y -= 16;
            directionChosen = true;
        }
        public override void AdjustLeft()
        {
            sword.AdjustLeft();

            sprite = sword.sprite;
            currentLocation.X -= 26;
            currentLocation.Y -= 14;
            directionChosen = true;
        }
        public override void AddDecoratorToEnemy(IEnemy enemy)
        {
            // Add any previous decorators to this 
            sword.AddDecoratorToEnemy(enemy);
        }
        public override void RemoveDecorator(SwordType swordType)
        {
            GameObjectManager.Instance.mLink.LinkSword = sword;            
        }
    }
}

 