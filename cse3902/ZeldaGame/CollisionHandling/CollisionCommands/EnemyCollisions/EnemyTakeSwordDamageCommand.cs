using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class EnemyTakeSwordDamageCommand : ICollisionCommand
    {
        private Sword sword;
        private IEnemy enemy;
        public EnemyTakeSwordDamageCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        { 
            enemy = obj1 as IEnemy;
            sword = obj2 as Sword;

            sword.AddDecoratorToEnemy(enemy);
            enemy.TakeDamage(sword.GetAndSetDamage());
            if (enemy is FrostEnemy) enemy.FreezeEnemy(1);
        }

    }
}

