using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class MasterBat : GameObject, IEnemy, IUpdatable, IDrawable, ICollidable
    {
        GameObjectManager objectManager;
        private Random random;
        private double randomX;
        private double randomY;
        private Boolean isAlive = true;
        private String collidableType = "Enemy";
        private Vector2 moveVector;
        private ISound Sound { get; set; }
        public int Speed { get; set; }

        private Boolean isHit = false;
        private float hitTimer = 0;

        public float stateTimer = 0;
        public int Health { get; set; }

        public MasterBat()
        {
            this.objectManager = GameObjectManager.Instance;
            sprite = SpriteFactory.Instance.getSprite(Sprite.Bat); // sprite variable comes from GameObject class
           // currentLocation = new Vector2(400, 200); // currentLocation variable comes from GameObject class

            random = new Random();
            moveVector= new Vector2(0, 0);
            Health = 5;
        }

        private void MoveToPoint(Vector2 moveVector)
        {
            double dx = moveVector.X;
            double dy = moveVector.Y;
             
           // double angle = Math.Atan2(dy, dx);
           double magnitude = Math.Sqrt((moveVector.X * moveVector.X) + (moveVector.Y * moveVector.Y));

            if (dx != 0) dx = (dx / (float)magnitude) * 2;
            if (dy != 0) dy = (dy / (float)magnitude) * 2;

            currentLocation.X += (float)dx;
            currentLocation.Y += (float)dy; 
        }
          
        public override void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);

            stateTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (stateTimer >= 200 && !isAlive)  // Enemy is dead
            {
                objectManager.Remove(this);
                stateTimer = 0;
            }
            if (stateTimer >= 1000)             // Find next location to move to
            {
                randomX = random.Next(-3, 3);
                randomY = random.Next(-3, 3);
                moveVector = new Vector2((float)randomX, (float)randomY);
                stateTimer = 0;
            }
            if (isHit)
            {
                hitTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (hitTimer >= 500)
                {
                    isHit = false;
                    hitTimer = 0;
                }
            }
            if (isAlive) MoveToPoint(moveVector);         // Enemy is alive, so make it move 
        }
        public void MoveUp()
        {
            moveVector.Y = -moveVector.Y;
        }
        public void MoveDown()
        {
            moveVector.Y = -moveVector.Y;
        }
        public void MoveLeft()
        {
            moveVector.X = -moveVector.X;
        }
        public void MoveRight()
        {
            moveVector.X = -moveVector.X;
        }
        public void Attack()
        {
            // Does not attack
        }
        public void TakeDamage(int damageTaken)
        {
            if (Health > 0 && !isHit)
            {
                Health -= damageTaken;
                isHit = true;
                Sound = SoundFactory.Instance.getSound(Sounds.EnemyHitSound);
                Sound.Play();
            }
            else if (Health <= 0)
            {
                Die();
            }
        }
        public void Die()
        {
            
            sprite = SpriteFactory.Instance.getSprite(Sprite.EnemyDeath);
            isAlive = false;
            collidableType = "DeadEnemy";
            Sound = SoundFactory.Instance.getSound(Sounds.EnemyDieSound);
            Sound.Play();
            Random rnd = new Random();
            int ranNum = rnd.Next(0, 2);
            for(int i = 0; i<=ranNum; i++)
            {
                RupeeItem enemyDrop = new RupeeItem();
                enemyDrop.setLocation(currentLocation);
                GameObjectManager.Instance.Add(enemyDrop);
            }
        }
        public override string GetCollidableType()
        {
            return collidableType;
        }

        public void FreezeEnemy(int speed)
        {
        }
    }

}
