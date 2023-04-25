using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class IgnitedEnemy : EnemyDecorator
    {
        public IEnemy decoratedEnemy;

        public ISprite backFire;
        public ISprite flameParticles;
        public float flameTick = 0;
        public int flameHitsTaken = 5;

        public IgnitedEnemy(IEnemy decoratedEnemy)
        {
            this.decoratedEnemy = decoratedEnemy;
            sprite = SpriteFactory.Instance.getSprite(Sprite.AnimCrystal); // Place holder sprite
            backFire = SpriteFactory.Instance.getSprite(Sprite.LinkFire);
            flameParticles = SpriteFactory.Instance.getSprite(Sprite.FlameParticles);
        }
        public override void Update(GameTime gameTime)
        {

            // Updates the enemy as usual
            decoratedEnemy.Update(gameTime);

            // Needs to update its location based on enemy location. This bug took FOREVER to find
            Location = decoratedEnemy.Location;

            // Adds the flame effect
            backFire.Update(gameTime);
            flameParticles.Update(gameTime);
            flameTick += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Enemy takes flame damage every 1000 ms
            if (flameTick >= 1000 && flameHitsTaken > 0)
            {
                TakeDamage(1);
                flameTick = 0;
                flameHitsTaken--;
            }

            // Removes the decorator once the flame effect is over
            if (flameHitsTaken <= 0)
            {
                RemoveDecorator(); 
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // These extra vectors are used to displace the fire to cover the full enemy

            backFire.Draw(spriteBatch, decoratedEnemy.Location - new Vector2(10, 10));
            decoratedEnemy.Draw(spriteBatch);
            flameParticles.Draw(spriteBatch, decoratedEnemy.Location - new Vector2(10, 10));
           
        }

        public void RemoveDecorator()
        {
            GameObjectManager.Instance.Remove(this);
            GameObjectManager.Instance.Add((GameObject)decoratedEnemy);
        }
        public override void MoveUp()
        {
            decoratedEnemy.MoveUp();
        }
        public override void MoveDown()
        {
            decoratedEnemy.MoveDown();
        }
        public override void MoveLeft()
        {
            decoratedEnemy.MoveLeft();
        }
        public override void MoveRight()
        {
            decoratedEnemy.MoveRight();
        }

        public override void Attack()
        {
            // Does not attack
        }
        public override void TakeDamage(int damageTaken)
        {
            decoratedEnemy.TakeDamage(damageTaken);
            if (decoratedEnemy.Health <= 0) { Die(); }
        }
        public override void Die()
        {
            GameObjectManager.Instance.Remove(this);
            decoratedEnemy.Die();
        }

        public override void FreezeEnemy(int speed)
        {

            decoratedEnemy.Speed = speed;

        }

    }
}
