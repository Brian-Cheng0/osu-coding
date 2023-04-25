﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class EnemyBlockRight : ICollisionCommand
    {
        private IEnemy enemy;
        public EnemyBlockRight()
        {

        }
        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            enemy = obj1 as IEnemy;
            enemy.MoveLeft();
        }

    }
}

