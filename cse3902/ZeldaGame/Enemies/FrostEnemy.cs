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
    public class FrostEnemy : EnemyDecorator
    {
        public IEnemy decoratedEnemy;

        public ISprite backFrost;
        public ISprite FrostParticles;
        public float FrostTick = 0;
        public int FrostHitsTaken = 5;
        public FrostEnemy(IEnemy decoratedEnemy)
        {
            this.decoratedEnemy = decoratedEnemy;
            sprite = SpriteFactory.Instance.getSprite(Sprite.AnimCrystal);
            backFrost = SpriteFactory.Instance.getSprite(Sprite.LinkFrost);//need frost sprite
            FrostParticles = SpriteFactory.Instance.getSprite(Sprite.FrostParticles);
        }


        public override void Update(GameTime gameTime)
        {
            // Updates the enemy as usual

            decoratedEnemy.Update(gameTime);
            // Needs to update its location based on enemy location. This bug took FOREVER to find
            Location = decoratedEnemy.Location;


            // Adds the frost effect
            backFrost.Update(gameTime);
            FrostParticles.Update(gameTime);
            FrostTick += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Enemy takes frost damage every 1000 ms
            if (FrostTick >= 1000 && FrostHitsTaken > 0)
            {
                decoratedEnemy.FreezeEnemy(1);
                FrostHitsTaken--;
            }
            else
            {
                decoratedEnemy.FreezeEnemy(2);
            }


            // Removes the decorator once the frost effect is over
            if (FrostHitsTaken <= 0)
            {
                RemoveDecorator();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // These extra vectors are used to displace the fire to cover the full enemy

            backFrost.Draw(spriteBatch, decoratedEnemy.Location - new Vector2(10, 10));
            decoratedEnemy.Draw(spriteBatch);
            FrostParticles.Draw(spriteBatch, decoratedEnemy.Location - new Vector2(10, 10));

        }

        public void RemoveDecorator()
        {
            GameObjectManager.Instance.Remove(this);
            FreezeEnemy(2);
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

