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
    public class FlamingArrow : ArrowDecorator
    {
        private ArrowDecorator decoratedArrow;


        private ISound Sound;
        private int magnitude = 6;

        private Boolean hasImpacted = false;
        private Boolean directionChosen = false;

        private float flightTime = 0;

        public FlamingArrow(ArrowDecorator decoratedArrow)
        {
            this.decoratedArrow = decoratedArrow;
     
            sprite = SpriteFactory.Instance.getSprite(Sprite.DownFlameArrow);
            currentLocation = decoratedArrow.currentLocation;
            
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

            // Find out which direction the original arrow was used in 
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
            directionChosen = true;
         
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
             sprite.Draw(spriteBatch, currentLocation);
        }
        public override void AdjustUp()
        {
            if (!directionChosen) sprite = SpriteFactory.Instance.getSprite(Sprite.UpFlameArrow);
            currentLocation.Y -= magnitude;

        }
        public override void AdjustDown()
        {
            if (!directionChosen) sprite = SpriteFactory.Instance.getSprite(Sprite.DownFlameArrow);
            currentLocation.Y += magnitude;
        }
        public override void AdjustLeft()
        {
            if (!directionChosen) sprite = SpriteFactory.Instance.getSprite(Sprite.LeftFlameArrow);
            currentLocation.X -= magnitude;
        }
        public override void AdjustRight()
        {
            if (!directionChosen) sprite = SpriteFactory.Instance.getSprite(Sprite.RightFlameArrow);
            currentLocation.X += magnitude;
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
            if (enemy is not IgnitedEnemy)
            {
                GameObjectManager.Instance.Remove((GameObject)enemy);
                enemy = new IgnitedEnemy(enemy);
                GameObjectManager.Instance.Add((GameObject)enemy);
            }
        }
    }
}
