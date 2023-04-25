using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace ZeldaGame
{
    public abstract class Sword : GameObject, IUpdatable, IDrawable, ICollidable
    {
        public Direction direction;
        public bool directionChosen = false;
        public int damagePerSwing = 5;
        public List<SwordType> enchantsOnSword = new List<SwordType>();
        public override void Update(GameTime gameTime)
        {
            if (!directionChosen)
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

            sprite.Update(gameTime);
            if (!GameObjectManager.Instance.mLink.linkLock) { DestroySword(); }
        }

        // Draw method from GameObject  

        // The decorators will change these functions
        public abstract int GetAndSetDamage();
        public abstract void AdjustUp();
        public abstract void AdjustDown();
        public abstract void AdjustRight();
        public abstract void AdjustLeft();

        public abstract void AddDecoratorToEnemy(IEnemy enemy);
        public abstract void RemoveDecorator(SwordType swordType);

        // All swords Create, Destroy and have the same collidable type
        public void CreateSword()
        {
            GameObjectManager.Instance.Add(this);
            currentLocation = GameObjectManager.Instance.mLink.Location;
            direction = GameObjectManager.Instance.mLink.currentDirection;
            directionChosen = false;
        }
        public void DestroySword()
        {
            GameObjectManager.Instance.Remove(this);
        }

        public override String GetCollidableType()
        {
            return "PlayerSword";
        }
    }
}
