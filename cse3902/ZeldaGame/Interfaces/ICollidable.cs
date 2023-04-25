using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public interface ICollidable
    {
        // Create the bounding box
        public Vector2 Location { get; }
        public Vector2 Size { get; }

        public String GetCollidableType();

    }
}
