using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class ItemImpactCommand : ICollisionCommand
    {
        private IItem item;
        public ItemImpactCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            // A bug arrises when we need to pass in both collidables instead of just one.
            // Sometimes ItemImpactCommand is called on the first object and sometimes on the second.
            // This identifies which object to call it on
            if (obj1 is IItem) item = obj1 as IItem;
            if (obj2 is IItem) item = obj2 as IItem;
            
            item.Impact();
        }

    }
}

