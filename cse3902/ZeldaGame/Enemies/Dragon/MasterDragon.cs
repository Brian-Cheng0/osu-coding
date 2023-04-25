using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace ZeldaGame
{
    public class MasterDragon : GameObject, IEnemy, IUpdatable, IDrawable, ICollidable
    {
        public GameObjectManager objectManager;
        public IEnemyState state { get; set; }
        public Boolean isMoving = false;
        private Boolean isAlive = true;
        public float stateTimer = 0;
        public float attackTimer = 0;
        private Random random;
        private String collidableType = "Enemy";
        private ISound DieSound { get; set; }
        private ISound HitSound { get; set; }
        private ISound ScreamSound { get; set; }
        private Boolean isHit = false;

        private float hitTimer = 0;
        public int Health { get; set; }
        public int Speed { get; set; }


        public MasterDragon()
        {
            this.objectManager = GameObjectManager.Instance;

            state = new DragonLeftState(this);
            sprite = SpriteFactory.Instance.getSprite(Sprite.Dragon); // sprite variable comes from GameObject class
            currentLocation = new Vector2(400, 200); // currentLocation variable comes from GameObject class
            random = new Random();
            Health = 30;
            
        }

        public override void Update(GameTime gameTime)
        {
            stateTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            attackTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (stateTimer >= 400 && !isAlive) objectManager.Remove(this); // Enemy is dead
            if (stateTimer >= 500)                 // Enemy is still alive, keep moving it
            {
                SwitchDirection();
                stateTimer = 0;
            }  
            if (attackTimer >= 3000)
            {
                Attack();
                attackTimer = 0;
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
            sprite.Update(gameTime);
            state.Update(Speed);
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
            // TODO: switch to attack state
            objectManager.Add(new DragonFireBall(this, new Vector2(-4, -2)));
            objectManager.Add(new DragonFireBall(this, new Vector2(-4, 0)));
            objectManager.Add(new DragonFireBall(this, new Vector2(-4, 2)));
            ScreamSound = SoundFactory.Instance.getSound(Sounds.BossScream);
            ScreamSound.Play();
           // state.AttackState();
        }
        public void SwitchDirection()
        {
            // TOOD: not easily expandable but it works for now.
            if (state is DragonLeftState) { state = new DragonRightState(this); }
            else if (state is DragonRightState) { state = new DragonLeftState(this); }
        }
         
        public void TakeDamage(int damageTaken)
        {
            if (Health > 0 && !isHit)
            {
                Health -= damageTaken;
                HitSound = SoundFactory.Instance.getSound(Sounds.EnemyHitSound);
                HitSound.Play();
                isHit = true;
            }
            if (Health <= 0) { Die(); }
        }
        public void Die()
        {
            sprite = SpriteFactory.Instance.getSprite(Sprite.EnemyDeath);
            isAlive = false;
            collidableType = "DeadEnemy";
            DieSound = SoundFactory.Instance.getSound(Sounds.EnemyDieSound);
            DieSound.Play();
           
            HeartContainerItem enemyDrop = new HeartContainerItem();
            enemyDrop.setLocation(currentLocation);
            GameObjectManager.Instance.Add(enemyDrop);
            
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
