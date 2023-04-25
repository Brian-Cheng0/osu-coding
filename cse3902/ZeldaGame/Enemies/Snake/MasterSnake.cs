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
    public class MasterSnake : GameObject, IEnemy, IUpdatable, IDrawable, ICollidable
    {
        GameObjectManager objectManager;
        public IEnemyState state { get; set; }
        public Boolean isMoving = false;
        private Random random;
        public float stateTimer = 0;
        private Boolean isAlive = true;
        private String collidableType = "Enemy";
        private ISound Sound { get; set; }
        public int Speed { get; set; }
        private Boolean isHit = false;

        private float hitTimer = 0;
        public int Health { get; set; }

        public MasterSnake()
        {
            this.objectManager = GameObjectManager.Instance;
            state = new SnakeEnemyLeftState(this);
            sprite = SpriteFactory.Instance.getSprite(Sprite.SnakeLeft); // sprite variable comes from GameObject class
            currentLocation = new Vector2(400, 200); // currentLocation variable comes from GameObject class

            random = new Random();
            Health = 10;
            Speed = 2;
        }
        public override void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);

            stateTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (stateTimer >= 500 && !isAlive) // Enemy is dead
            {
                objectManager.Remove(this);
                stateTimer = 0;
            }
            if (isHit)
            {
                hitTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (hitTimer >= 500)
                {
                    isHit = false;
                }
            }
            else state.Update(Speed);
            
        }
        
        // Changes to walking left state
        public void MoveLeft()
        {
            state.WalkLeft();
        }
        // Changes to walking right state
        public void MoveRight()
        {
            state.WalkRight();
        }
        public void MoveUp()
        {
            //does not move up
        }

        public void MoveDown()
        {
            //does not move down
        }
        public void Attack()
        {
            //not implemented right now
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
            int ranNum = rnd.Next(0, 6);
            for (int i = 0; i <= ranNum; i++)
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
            speed = 1;
        }
    }

}