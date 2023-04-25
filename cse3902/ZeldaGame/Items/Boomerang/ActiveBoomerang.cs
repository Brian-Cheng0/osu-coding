  using Microsoft.Xna.Framework;
using System;
using ZeldaGame.Objects;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace ZeldaGame
{
    public class ActiveBoomerang : BoomerangDecorator
    {
        private MasterLink link;
        public int Price { get; set; }
        private Vector2 originalLocation;
        private int range = 200;
        private bool hasImpacted = false;
    
        

        public ActiveBoomerang(MasterLink link)
        {
            this.link = link;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkBoomerang);
            InUse = false;
            Magnitude = 6;
        }
        public override void Update(GameTime gameTime)
        {
            
            sprite.Update(gameTime);
            if (hasImpacted)
            {
                GameObjectManager.Instance.Remove(this);
                soundInstance.Stop();
            }// This is so the impact can be shown for one frame
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
        public override void CollectItem()
        {
            // Does not collect
        }
        public override void Use()
        {
            // Resets the boomerang        
            currentLocation = link.currentLocation;
            originalLocation = link.currentLocation;
            direction = link.currentDirection;
            Magnitude = 6;
            hasImpacted = false;
            InUse = true;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkBoomerang);

            AttackSound = SoundFactory.Instance.getSound(Sounds.BoomerangSound);
            soundInstance = AttackSound.PlayLooped();
            GameObjectManager.Instance.Add(this);
                   
        }
        public override void Impact()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.ItemImpact);
            InUse = false;
            hasImpacted = true;
        }


        public override void AdjustUp()
        {
            // Once boomerang hits max range it turns around
            if (currentLocation.Y <= originalLocation.Y - range)
            {
                Magnitude--;
            }
            currentLocation.Y -= Magnitude;
            // If it get backs to link, make it disappear 
            if (currentLocation.Y >= link.currentLocation.Y)
            {
                Impact();
            }
        }
        public override void AdjustDown()
        {
            // Once boomerang hits max range it turns around
            if (currentLocation.Y >= originalLocation.Y + range)
            {
                Magnitude--;
            }
            currentLocation.Y += Magnitude;
            // If it get backs to link, make it disappear 
            if (currentLocation.Y <= link.currentLocation.Y)
            {
                Impact();
            }
        }
        public override void AdjustLeft()
        {

            // Once boomerang hits max range it turns around
            if (currentLocation.X <= originalLocation.X - range)
            {
               Magnitude--;
            }
            currentLocation.X -= Magnitude;

            // If it get backs to link, make it disappear 
            if (currentLocation.X >= link.currentLocation.X)
            {
                Impact();
            }
        }
        public override void AdjustRight()
        {
            // Once boomerang hits max range it turns around
            if (currentLocation.X >= originalLocation.X + range)
            {
                Magnitude--;
            }
            currentLocation.X += Magnitude;

            // If it get backs to link, make it disappear 
            if (currentLocation.X <= link.currentLocation.X)
            {
                Impact();
            }
        }
        public override void AddDecoratorToEnemy(IEnemy enemy)
        {
            // Does not add a decorator
        }
    }
}
