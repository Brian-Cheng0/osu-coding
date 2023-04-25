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
using System.IO;
using Microsoft.Xna.Framework.Media;

namespace ZeldaGame
{
    public class MasterSkeleton : GameObject, IEnemy, IUpdatable, IDrawable, ICollidable
    {
        public GameObjectManager objectManager;
        public IEnemyState state;
        private Random random;
        private int directionChooser; 
        private Boolean isAlive = true;
        private String collidableType = "Enemy";
        private ISound Sound { get; set; }
        public int Speed { get; set; }

        public float stateTimer = 0;

        private Boolean isHit = false;
        private float hitTimer = 0;
        public int Health { get; set; }

        public MasterSkeleton()
        {
            this.objectManager = GameObjectManager.Instance;
            state = new LeftSkeletonState(this);
            sprite = SpriteFactory.Instance.getSprite(Sprite.Skeleton); // sprite variable comes from GameObject class
            currentLocation = new Vector2(400, 200); // currentLocation variable comes from GameObject class

            random = new Random();
            Health = 10;
            Speed = 2;
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
            if (stateTimer >= 300)             // Find next location to move to
            {
                directionChooser = random.Next(0, 4);
                if (directionChooser == 0) MoveUp();
                else if (directionChooser == 1) MoveDown();
                else if (directionChooser == 2) MoveLeft();
                else if (directionChooser == 3) MoveRight();

                stateTimer = 0;
            }
            state.Update(Speed);
            if (isHit)
            {
                hitTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(hitTimer >= 500)
                {
                    isHit = false;
                    hitTimer = 0;
                }
            }
 

        }
        public void MoveUp()
        {
            state.WalkUp();
        }
        public void MoveDown()
        {
            state.WalkDown();
        }
        public void MoveLeft()
        {
            state.WalkLeft();
        }
        public void MoveRight()
        { 
            state.WalkRight();
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
            int ranNum = rnd.Next(0, 5);
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
