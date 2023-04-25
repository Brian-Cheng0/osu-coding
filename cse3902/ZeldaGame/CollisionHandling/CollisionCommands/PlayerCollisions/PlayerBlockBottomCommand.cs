using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class PlayerBlockBottomCommand : ICollisionCommand
    {
        private ILink link;
        public PlayerBlockBottomCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            link = obj1 as ILink;
            Vector2 tempLocation = link.Location;
            tempLocation.Y -= overlap.Height;
            link.Location = tempLocation;


        }

    }
}

