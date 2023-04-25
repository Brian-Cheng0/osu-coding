using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class DownNormalDoor : GameObject, IDoor, IUpdatable, IDrawable, ICollidable
    {
        public String CollidableType { get; set; }
        public DownNormalDoor()
        {
            CollidableType = "WalkableDoor"; // lLnk can walk through the door
            sprite = SpriteFactory.Instance.getSprite(Sprite.DownNormalDoor);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
        }
        public void ExplodeDoor()
        {
            // Does not explode
        }
        public void UnlockDoor()
        {
            // Does not unlock
        }
        public void LockDoor()
        {
            // Does not lock
        }
        public override string GetCollidableType()
        {
            return CollidableType;
        }
    }
}
