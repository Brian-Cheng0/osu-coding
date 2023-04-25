using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeldaGame
{
    public class CollisionDetector
    {

        private AllCollisionHandler handler;
        private GameObjectManager objectManager;
        public CollisionDetector(GameObjectManager objectManager)
        {
            handler = new AllCollisionHandler();
            this.objectManager = objectManager;
        }

        // Returns what direction the collision is occuring between obj1 and obj2. Returns None if no collision
        public void DetectCollision(ICollidable obj1, ICollidable obj2)
        {
            Side sideOfCollision = Side.None;

            Rectangle hitbox1 = new Rectangle((int)obj1.Location.X, (int)obj1.Location.Y, (int)obj1.Size.X, (int)obj1.Size.Y);
            Rectangle hitbox2 = new Rectangle((int)obj2.Location.X, (int)obj2.Location.Y, (int)obj2.Size.X, (int)obj2.Size.Y);

            Rectangle overlap = Rectangle.Intersect(hitbox1, hitbox2);

            int dx = overlap.Width;
            int dy = overlap.Height;

            if (!overlap.IsEmpty) // If there is a collision, find out which side
            {
                if (dx >= dy) // This is a top-down collision
                {
                    if (hitbox1.Location.Y <= hitbox2.Location.Y) { sideOfCollision = Side.Bottom; } // Obj1 is above obj2
                    else { sideOfCollision = Side.Top; }                                             // Obj1 is below obj2
                }
                else if (dx < dy)// This is a left-right collision
                {
                    if (hitbox1.Location.X <= hitbox2.Location.X) { sideOfCollision = Side.Right; } // Right of obj1 is touching obj2          
                    else { sideOfCollision = Side.Left; }                                           // Left of obj1 is touching obj2
                }

                // We have the two objects, its overlap rectangle, and its side. Now do its respective action
                handler.HandleCollision(obj1, obj2, sideOfCollision, overlap);

            }
        }

        public void DoAllComparisons()
        {
            // Compares Dynamic to Static objects 
            foreach (ICollidable dynamicObj in objectManager.dynamicCollidables.ToList())
            {
                foreach (ICollidable staticObj in objectManager.staticCollidables.ToList())
                {
                    DetectCollision(dynamicObj, staticObj);
                }
            }
            // Compares Dynamic to Dynamic objects
            foreach (ICollidable dynamicObj1 in objectManager.dynamicCollidables.ToList())
            {
                foreach (ICollidable dynamicObj2 in objectManager.dynamicCollidables.ToList())
                {
                    if (dynamicObj1 != dynamicObj2) DetectCollision(dynamicObj1, dynamicObj2);
                }
            }
        }  
    }
}
