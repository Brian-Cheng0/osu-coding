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
    public class OldManNPC : GameObject, IUpdatable, IDrawable, ICollidable
    {
        public String CollidableType { get; set; }
        public OldManNPC()
        {
            CollidableType = "TransparentBlock"; // Always collides like a block
            sprite = SpriteFactory.Instance.getSprite(Sprite.OldMan);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
        }
        public override string GetCollidableType()
        {
            return CollidableType; // Will always collide like a block
        }
    }
}
