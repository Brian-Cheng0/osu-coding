using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame.Items
{
    public class LinkFire : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {

        private GameObjectManager objectManager;
        private ILink link;
        public Boolean InUse { get; set; }
        public int Price { get; set; }

        private Vector2 originalLocation;
        private int range = 200;
        private int magnitude = 6;
        private int fireTimer = 3;
        private Direction direction;


        public LinkFire()
        {
            this.link = GameObjectManager.Instance.mLink;

            InUse = false;

            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkFire);
            originalLocation = link.Location;
            currentLocation = link.Location;
            direction = link.currentDirection;

        }
        public override void Update(GameTime gameTime)
        {
            sprite.Update(gameTime); // Updates frame of sprite
            switch (direction)
            {
                case Direction.Up:
                    AdjustUp();
                    break;
                case Direction.Down:
                    AdjustDown();
                    break;
                case Direction.Left:
                    AdjustLeft();
                    break;
                case Direction.Right:
                    AdjustRight();
                    break;
            }

        }

        // Draw method from GameObject

        public void AdjustUp()
        {
            currentLocation.Y -= magnitude;
            if (currentLocation.Y <= originalLocation.Y - range) objectManager.Remove(this);
        }

        public void AdjustDown()
        {
            currentLocation.Y += magnitude;
            if (currentLocation.Y >= originalLocation.Y + range) { objectManager.Remove(this); }

        }
        public void AdjustLeft()
        {
            currentLocation.X -= magnitude;
            if (currentLocation.X <= originalLocation.X - range) { objectManager.Remove(this); }
        }
        public void AdjustRight()
        {
            currentLocation.X += magnitude;
            if (currentLocation.X >= originalLocation.X + range) { objectManager.Remove(this); }
        }

        public void CollectItem()
        {
            // TODO: implement this
        }
        public void Use()
        {
            // TODO: Does this need to be implemented?
            // InUse = true;
        }
        public void Impact()
        {
            // TODO: Does this impact?
        }

        public override string GetCollidableType()
        {
            return "PlayerProjectile";
        }

    }
}

