using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class DiagonalArrow : ArrowDecorator
    {
        public ArrowDecorator decoratedArrow;

        public Direction primaryDirection;
        public Direction secondaryDirection;

        public Boolean hasImpacted = false;
        public Boolean foundOriginalLocation = false;

        private float flightDuration = 0;
        public DiagonalArrow(ArrowDecorator decoratedArrow)
        {
            this.decoratedArrow = decoratedArrow;
            
            sprite = SpriteFactory.Instance.getSprite(Sprite.UpLeftArrow);
        }

        public override void Update(GameTime gameTime)
        {
            if (!foundOriginalLocation)
            {
                Location = decoratedArrow.Location;
                foundOriginalLocation = true;
            }
            
            // Updates the middle arrow like normal (Uses the Adjust methods from the normal arrow)
            sprite.Update(gameTime);


            if (hasImpacted)
            {
                GameObjectManager.Instance.Remove(this);
            }
            // Automatically impact after 300 ms
            flightDuration += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (flightDuration > 2000)
            {
                Impact();
            }
            // This does all the work for the left and right arrows
            switch (primaryDirection)
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Location);
        }
        // Draw method from GameObject
        public override void AdjustUp()    
        {
            // Arrow goes up and to the left
            if (secondaryDirection == Direction.Left)
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.UpLeftArrow);
                Location += new Vector2(-2, -5);
            }
            else // Arrow goes up and to the right
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.UpRightArrow);
                Location += new Vector2(2, -5);
            }
        }
        public override void AdjustDown()
        {
            if (secondaryDirection == Direction.Left)
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.DownLeftArrow);
                Location += new Vector2(-2, 5);
            }
            else 
            {   
                sprite = SpriteFactory.Instance.getSprite(Sprite.DownRightArrow);
                Location += new Vector2(2, 5);
            }
        }
        public override void AdjustLeft()
        {
            if (secondaryDirection == Direction.Up)
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.LeftDownArrow);
                Location += new Vector2(-5, 2);
            }
            else
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.LeftUpArrow);
                Location += new Vector2(-5, -2);
            }
        }
        public override void AdjustRight()
        {
            if (secondaryDirection == Direction.Up)
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.RightUpArrow);
                Location += new Vector2(5, -2);
            }
            else
            {
                sprite = SpriteFactory.Instance.getSprite(Sprite.RightDownArrow);
                Location += new Vector2(5, 2);
            }
        }

        public override void CollectItem()
        {
            // Does not collect
        }
        public override void Use()
        {
            // Does not use
        }
        public override void Impact()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.ItemImpact);
            hasImpacted = true;  
        }
        public override string GetCollidableType()
        {
            return "PlayerProjectile";
        }
        public void SetDirections(Direction primaryDirection, Direction secondaryDirection)
        {
            this.primaryDirection = primaryDirection;
            this.secondaryDirection = secondaryDirection;
        }
        public override void AddDecoratorToEnemy(IEnemy enemy)
        {
            // Does not add a decorator
        }
    }
}
