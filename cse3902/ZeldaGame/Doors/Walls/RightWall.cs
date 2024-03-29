﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class RightWall : GameObject, IDoor, IUpdatable, IDrawable, ICollidable
    {
        public String CollidableType { get; set; }
        public RightWall()
        {
            CollidableType = "Block"; // Always collides like a block
            sprite = SpriteFactory.Instance.getSprite(Sprite.RightWall);
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
            return CollidableType; // Will always collide like a block
        }
    }
}
