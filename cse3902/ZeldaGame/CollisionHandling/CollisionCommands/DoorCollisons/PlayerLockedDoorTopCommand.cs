using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class PlayerLockedDoorTopCommand : ICollisionCommand
    {
        private IDoor lockedTopDoor;
        public PlayerLockedDoorTopCommand()
        {

        }
        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            lockedTopDoor = obj2 as IDoor;
            lockedTopDoor.UnlockDoor();
        }

    }
}