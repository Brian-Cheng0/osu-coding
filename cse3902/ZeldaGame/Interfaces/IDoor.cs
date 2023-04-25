using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ZeldaGame
{
    public interface IDoor
    {
        public String CollidableType { get; set; }
        void ExplodeDoor();
        void UnlockDoor();
        void LockDoor();


    }
}
