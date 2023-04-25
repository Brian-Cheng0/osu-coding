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
    public class UpExplodableWall : GameObject, IDoor, IUpdatable, IDrawable, ICollidable
    {
        public String CollidableType { get; set; }
        public UpExplodableWall()
        {
            CollidableType = "BombableWall"; // Always collides like a block
            sprite = SpriteFactory.Instance.getSprite(Sprite.UpWall);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
        }
        public void ExplodeDoor()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.UpBoomedDoor);
            CollidableType = "WalkableDoor";
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
            return CollidableType; // Will always collide like a block
        }
    }
}
