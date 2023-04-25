using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class DragonFireBall : GameObject, IItem, IUpdatable, IDrawable, ICollidable 
    {
        private MasterDragon dragon;

        public bool InUse { get; set; }
        public int Price { get; set; }
        //private Vector2 currentPosition;
        private Vector2 positionIncrement;
        

        public DragonFireBall(MasterDragon dragon, Vector2 positionIncrement)
        {
            this.dragon = dragon;
            InUse = false;
            currentLocation = dragon.currentLocation;
           
            this.positionIncrement = positionIncrement;

            sprite = SpriteFactory.Instance.getSprite(Sprite.DragonFire);
        }

        public override void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
            AdjustPosition();
        }

        // Draw method from GameObject
        public void AdjustPosition()
        {
            currentLocation.X += positionIncrement.X;
            currentLocation.Y += positionIncrement.Y;

        }
       
        public void CollectItem()
        {
            // Does not collect
        }
        public void Use()
        {
            // Does not use
        }
        public void Impact()
        {
            // TODO: major coupling here, maybe make fireball take in objectManager as a parameter instead
            dragon.objectManager.Remove(this);
        }

        public override string GetCollidableType()
        {
            return "EnemyProjectile";
        }
    }
}
