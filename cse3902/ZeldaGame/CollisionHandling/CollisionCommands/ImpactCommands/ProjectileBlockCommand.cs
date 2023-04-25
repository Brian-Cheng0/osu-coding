using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class ProjectileBlockCommand : ICollisionCommand
    {
        private IItem projectile;
        public ProjectileBlockCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            projectile = obj1 as IItem;
            projectile.Impact();


        }

    }
}

