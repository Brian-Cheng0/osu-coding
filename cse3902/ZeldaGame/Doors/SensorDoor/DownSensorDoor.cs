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
    public class DownSensorDoor : GameObject, IDoor, IUpdatable, IDrawable, ICollidable
    {
        public String CollidableType { get; set; }
        public DownSensorDoor()
        {
            CollidableType = "Block"; // Originally collides like a block
            sprite = SpriteFactory.Instance.getSprite(Sprite.DownSensorDoor);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
        }
        public void ExplodeDoor()
        {
            // Does not explode
        }
        public void UnlockDoor()
        {
            // TODO: implement unlocking
        }
        public void LockDoor()
        {
            // TODO: implement locking
        }
        public override string GetCollidableType()
        {
            return CollidableType;
        }
    }
}
