using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class TeleportPlayerDownCommand : ICollisionCommand
    {
        private MasterLink link;
        public TeleportPlayerDownCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            link = obj1 as MasterLink;
            if (!link.linkLock)
            {
                link.currentLocation.Y += 240;
                link.state = new BackwardStandingLinkState(link);
            }
              
        }

    }
}
