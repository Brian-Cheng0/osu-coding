 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace ZeldaGame
{
    public class DoNothingCommand : ICollisionCommand
    {
        public DoNothingCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            // Nothing to happen here...
        }

    }
}

