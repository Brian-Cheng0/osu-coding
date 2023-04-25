using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ZeldaGame.Objects;
using System.Diagnostics;

namespace ZeldaGame
{
    public class NormalArrow : ArrowDecorator
    {
        private GameObjectManager objectManager;
        private ILink link;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        public ISound Sound { get; set; }
        private Vector2 originalLocation;
        private int range = 300;
        private int magnitude = 6;
        private bool hasImpacted = false;

        private float flightTime = 0;

        public NormalArrow()
        {
            this.link = GameObjectManager.Instance.mLink;
            InUse = false;

            sprite = SpriteFactory.Instance.getSprite(Sprite.DownArrow);
            Sound = SoundFactory.Instance.getSound(Sounds.BoomerangSound);

            originalLocation = link.Location;
            currentLocation = link.Location;
            direction = link.currentDirection;

        }

        public override void Update(GameTime gameTime)
        {

            sprite.Update(gameTime);

            // Stop updating and drawing item if it impacts
            if (hasImpacted)
            {
                GameObjectManager.Instance.Remove(this);
            }
            // Automatically impact after 2000 ms
            flightTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (flightTime > 2000)
            {
                Impact();
            }
           
            // Depending on direction update position in that way
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

        public override void AdjustUp()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.UpArrow);
            currentLocation.Y -= magnitude;
        }
        public override void AdjustDown()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.DownArrow);
            currentLocation.Y += magnitude;
        }
        public override void AdjustLeft()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.LeftArrow);
            currentLocation.X -= magnitude;
        }
        public override void AdjustRight()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.RightArrow);
            currentLocation.X += magnitude;
        }

        public override void CollectItem()
        {
            // Does not collect
        }
        public override void Use()
        {
            Sound = SoundFactory.Instance.getSound(Sounds.BoomerangSound);
            Sound.Play();
        }
        public override void Impact()
        { 
            sprite = SpriteFactory.Instance.getSprite(Sprite.ItemImpact);
            hasImpacted = true;
        }
        public override void AddDecoratorToEnemy(IEnemy enemy)
        {
            // Does not add a decorator
        }
    }
}
