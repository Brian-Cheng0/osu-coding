using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    // Boomerang bounces of target and goes to next closest enemy if within range
    // I tried making this a decorator but its basically just a subclass now
    public class RicochetBoomerang : BoomerangDecorator
    {
        private BoomerangDecorator decoratedBoomerang;
        private Boolean hasImpacted = false;
         
        private IEnemy nextEnemyToHit = null;
        private Boolean hasRicocheted = false;
        private int numberOfRicochets = 3;
        private int boomerangRange = 200;
        public RicochetBoomerang(BoomerangDecorator decoratedBoomerang)
        {
            this.decoratedBoomerang = decoratedBoomerang;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkBoomerang);
            InUse = false;
           
        }
        public override void Update(GameTime gameTime)
        {           
            sprite.Update(gameTime);

            
            // This is so the impact can be shown for one frame
            if (hasImpacted)
            {
                GameObjectManager.Instance.Remove(this);
                soundInstance.Stop();
            }

            // If it doesn't ricochet, move like normal
            if (!hasRicocheted)
            {
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
            else
            {
                MoveToNearestEnemy();
            }
           
        }
        public override void CollectItem()
        {
            // Does not collect
        }
        public override void Use()
        {
            // Resets the boomerang        
            currentLocation =  GameObjectManager.Instance.mLink.Location;
            originalLocation = GameObjectManager.Instance.mLink.Location;
            direction = GameObjectManager.Instance.mLink.currentDirection;
            Magnitude = 6;
            hasImpacted = false;
            hasRicocheted = false;
            InUse = true;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkBoomerang);

            AttackSound = SoundFactory.Instance.getSound(Sounds.BoomerangSound);
            soundInstance = AttackSound.PlayLooped();
            GameObjectManager.Instance.Add(this);
           
        }
        public override void Impact()
        {
            // If there are still ricochets left, check for where to go to
            if (numberOfRicochets > 0 )
            {             
                FindNearestTarget();              
            }
            else
            {
               FinalImpact();
            }
            numberOfRicochets--;

        }
        public void FinalImpact()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.ItemImpact);
            InUse = false;
            hasImpacted = true;

        }

        public override void AdjustUp()
        {
            // Once boomerang hits max range it turns around
            if (currentLocation.Y <= originalLocation.Y - boomerangRange)
            {
                Magnitude--;
            }
            currentLocation.Y -= Magnitude;
            // Tons of coupling :(
            if (currentLocation.Y > GameObjectManager.Instance.mLink.Location.Y)
            {
                FinalImpact();
            }
        }
        public override void AdjustDown()
        {
            // Once boomerang hits max range it turns around
            if (currentLocation.Y >= originalLocation.Y + boomerangRange)
            {
                Magnitude--;
            }
            currentLocation.Y += Magnitude;
            // Tons of coupling :(
            if (currentLocation.Y < GameObjectManager.Instance.mLink.Location.Y)
            {
                FinalImpact();
            }
        }
        public override void AdjustLeft()
        {
            if (currentLocation.X <= originalLocation.X - boomerangRange)
            {
                Magnitude--;
            }
            currentLocation.X -= Magnitude;
            // Tons of coupling :(
            if (currentLocation.X > GameObjectManager.Instance.mLink.Location.X)
            {
                FinalImpact();
            }
        }
        public override void AdjustRight()
        {
            // Once boomerang hits max range it turns around
            if (currentLocation.X >= originalLocation.X + boomerangRange)
            {
                Magnitude--;
            }
            currentLocation.X += Magnitude;
            // Tons of coupling :(
            if (currentLocation.X < GameObjectManager.Instance.mLink.Location.X)
            {
                FinalImpact();
            }
        }

        public override void AddDecoratorToEnemy(IEnemy enemy)
        {
            decoratedBoomerang.AddDecoratorToEnemy(enemy);
        }

        private void FindNearestTarget()
        {
            float range = 300;
            float blindRange = 50; // Ignore all enemies that are closer than this number
            float closestDistance = 100000; // Arbitrary max that will be replaced soon

            foreach (GameObject obj in GameObjectManager.Instance.dynamicCollidables)
            {
                // Direct distance from enemy to boomerang
                float magnitude = 0;
                // This only cares about the enemies in the dynamic list 
                if (obj is IEnemy)
                {
                    float xFromEnemy = Math.Abs(obj.Location.X - Location.X);
                    float yFromEnemy = Math.Abs(obj.Location.Y - Location.Y);

                    // Enemy is in ricochet range
                    if ((xFromEnemy <= range && xFromEnemy >= blindRange)
                        && (yFromEnemy <= range && yFromEnemy >= blindRange))
                    {

                        // Find hypotenuse from boomerang to enemy
                        magnitude = (float)Math.Sqrt((xFromEnemy * xFromEnemy) + (yFromEnemy * yFromEnemy));

                        // Checks if this is the closest enemy, if it is then set it as the target
                        if (magnitude <= closestDistance)
                        {
                            closestDistance = magnitude;
                            nextEnemyToHit = (IEnemy)obj;
                        }

                        // Once the closest enemy is picked, set it as the next enemy to move to
                        if (nextEnemyToHit != null) hasRicocheted = true;
                    }
                }
            } 
        }

        public void MoveToNearestEnemy()
        {
            if (nextEnemyToHit != null)
            {

                // Gets the displacement from enemy to boomerang
                float XToMove = nextEnemyToHit.Location.X - currentLocation.X;
                float YToMove = nextEnemyToHit.Location.Y - currentLocation.Y; 

                // Finds the hypotenus between the two 
                float magnitude = (float)Math.Sqrt((XToMove * XToMove) + (YToMove * YToMove));

                XToMove /= magnitude;
                YToMove /=magnitude;

                currentLocation.X += (XToMove * 6);
                currentLocation.Y += (YToMove * 6);

            }
        }
    }
}
