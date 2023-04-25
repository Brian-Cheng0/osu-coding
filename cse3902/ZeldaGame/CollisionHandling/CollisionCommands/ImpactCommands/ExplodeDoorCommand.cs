using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class ExplodeDoorCommand : ICollisionCommand
    {
        private IDoor explodableDoor;
        public ExplodeDoorCommand()
        {

        }
        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            explodableDoor = obj2 as IDoor;
            explodableDoor.ExplodeDoor();
        }

    }
}