using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class PlayerLockedDoorLeftCommand : ICollisionCommand
    {
        private IDoor lockedLeftDoor;
        public PlayerLockedDoorLeftCommand()
        {

        }
        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            lockedLeftDoor = obj2 as IDoor;
            lockedLeftDoor.UnlockDoor();
        }

    }
}