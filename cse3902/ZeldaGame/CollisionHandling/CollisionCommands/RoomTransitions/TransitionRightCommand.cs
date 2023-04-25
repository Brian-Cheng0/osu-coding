using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace ZeldaGame
{
    public class TransitionRightCommand : ICollisionCommand
    {
        public TransitionRightCommand()
        {

        }
        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            if (!LevelManager.Instance.isMoving)
                LevelManager.Instance.SwitchRoomSmooth("right");
        }

    }
}