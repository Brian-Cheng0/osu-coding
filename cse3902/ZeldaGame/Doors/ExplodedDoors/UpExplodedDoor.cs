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
    public class UpExplodedDoor : GameObject, IDoor, IUpdatable, IDrawable, ICollidable
    {
        public String CollidableType { get; set; }
        public UpExplodedDoor()
        {
            CollidableType = "WalkableDoor";
            sprite = SpriteFactory.Instance.getSprite(Sprite.UpBoomedDoor); 
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
        }
        public void ExplodeDoor()
        {
            // TODO: implement explodable wall
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
            return CollidableType; // Door can be exploded but not exploded yet
        }
    }
}
