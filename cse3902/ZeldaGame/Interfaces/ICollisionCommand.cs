using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public interface ICollisionCommand
    {   
        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap);
    }
}
