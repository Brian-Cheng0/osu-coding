using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class TripleArrow : ArrowDecorator
    {
        private ArrowDecorator decoratedArrow;
        private ISound Sound;

        private DiagonalArrow LeftArrow;
        private DiagonalArrow RightArrow;

        private Boolean hasImpacted = false;
        private Boolean addedDiagonalArrows = false;

        private float flightDuration = 0;

        public TripleArrow(ArrowDecorator decoratedArrow) 
        {
            this.decoratedArrow = decoratedArrow;
            sprite = decoratedArrow.sprite;

            LeftArrow = new DiagonalArrow(this);
            RightArrow = new DiagonalArrow(this);

        }
        public override void Update(GameTime gameTime)
        {
            // This is a way to add to the GameObjectManager the diagonal arrows only once
            if (!addedDiagonalArrows)
            {
                GameObjectManager.Instance.Add(LeftArrow);
                GameObjectManager.Instance.Add(RightArrow);
                addedDiagonalArrows = true;
            }

            // Updates the middle arrow like normal (Uses the Adjust methods from the normal arrow)
            decoratedArrow.Update(gameTime);

            if (hasImpacted) 
            {
                GameObjectManager.Instance.Remove(this);     
            }

            // Automatically impact after 2000 ms
            flightDuration += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (flightDuration > 2000)
            {
                Impact();
            }
  

            Location = decoratedArrow.Location;
            // This does all the work for the left and right arrows
            switch (decoratedArrow.direction)
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
            // Draw the middle arrow
            decoratedArrow.Draw(spriteBatch);
        }
        public override void AdjustUp()
        {
            // Only adjust the left and right arrows. The middle arrow is adjusted with the Update
            LeftArrow.SetDirections(Direction.Up, Direction.Left);
            RightArrow.SetDirections(Direction.Up, Direction.Right);
        }
        public override void AdjustDown()
        {
            // Only adjust the left and right arrows. The middle arrow is adjusted with the Update
            LeftArrow.SetDirections(Direction.Down, Direction.Left);
            RightArrow.SetDirections(Direction.Down, Direction.Right);
        }
        public override void AdjustLeft()
        {
            // Only adjust the left and right arrows. The middle arrow is adjusted with the Update
            LeftArrow.SetDirections(Direction.Left, Direction.Up);
            RightArrow.SetDirections(Direction.Left, Direction.Down);
        }
        public override void AdjustRight()
        {
            // Only adjust the left and right arrows. The middle arrow is adjusted with the Update
            LeftArrow.SetDirections(Direction.Right, Direction.Up);
            RightArrow.SetDirections(Direction.Right, Direction.Down);
        }

        public override void CollectItem()
        {
            decoratedArrow.CollectItem();
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
