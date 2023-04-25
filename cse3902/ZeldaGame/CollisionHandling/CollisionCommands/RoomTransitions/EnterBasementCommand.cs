using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace ZeldaGame
{
    public class EnterBasementCommand : ICollisionCommand
    {
        public EnterBasementCommand()
        {

        }
        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            LevelManager.Instance.moveToAndFromBasement("left");
        }

    }
}