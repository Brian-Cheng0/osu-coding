using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class EnemyTakeOtherDamageCommand : ICollisionCommand
    {
        private IEnemy enemy;
        private ArrowDecorator arrow;
        public EnemyTakeOtherDamageCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            enemy = obj1 as IEnemy;

            if (obj2 is ArrowDecorator)
            {
                arrow = obj2 as ArrowDecorator;
                arrow.AddDecoratorToEnemy(enemy);
            }       
            enemy.TakeDamage(5); // ATTENTION! The goal is to create an IWeapon interface so we can merge this and the
            // EnemyTakeSwordDamageCommand. As of now anything besides a sword does 10 damage.
        }

    }
}

