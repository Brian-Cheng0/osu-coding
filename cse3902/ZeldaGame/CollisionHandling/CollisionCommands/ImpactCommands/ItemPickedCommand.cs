using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class ItemPickupCommand : ICollisionCommand
    {
        private IItem item;
        public ItemPickupCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            item = obj2 as IItem;
            item.CollectItem();
        }

    }
}

