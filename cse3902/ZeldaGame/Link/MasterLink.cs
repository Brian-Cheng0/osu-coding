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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using ZeldaGame;

namespace ZeldaGame
{
    public class MasterLink : GameObject, ILink, IUpdatable, IDrawable, ICollidable
    {

        public GameObjectManager objectManager;
        public ISprite LinkSprite { get; set; }
        public ILinkState state { get; set; }  
        public Direction currentDirection { get ; set; }    
        private int health = 6;
        private ISound DamageSound { get; set; }
        private ISound SlashSound { get; set; }

        public Sword LinkSword { get; set; }
        public BoomerangDecorator LinkBoomerang { get; set; }
        public IBow LinkBow { get; set; }

     
        private float timer;
        private bool timerOn;
        public bool linkLock { get; set; }

        public MasterLink(GameObjectManager objectmanager)
        {

            this.objectManager = objectmanager;
            sprite = SpriteFactory.Instance.getSprite(ZeldaGame.Sprite.LinkBackwardIdle); // sprite variable comes from GameObject class
            state = new BackwardStandingLinkState(this);        
            currentLocation = new Vector2(400, 400); // currentLocation variable comes from GameObject class
            
            currentDirection = Direction.Down;
            linkLock = false;

            LinkSword = new NormalSword(this);
            LinkBoomerang = new ActiveBoomerang(this); // Link is given a rochet boomerang by default
            LinkBow = new NormalBow();
        }

        public void Idle()
        {
           if (!linkLock) state.IdleState();        
        }
        // Changes to walking up state
        public void MoveUp()
        {
            if (!linkLock) state.WalkUp();
        }
        // Changes to walking down state
        public void MoveDown()
        {
            if (!linkLock) state.WalkDown();
        }
        // Changes to walking left state
        public void MoveLeft()
        {
            if (!linkLock) state.WalkLeft();
        }
        // Changes to walking right state
        public void MoveRight()
        {
            if (!linkLock) state.WalkRight();
        }
        // Changes to attacking state
        public void Attack()
        {
            SlashSound = SoundFactory.Instance.getSound(Sounds.SwordSlashSound);
            if (!linkLock)
            {
                state.AttackState();
                timerOn = true;
                timer = 25;
                linkLock = true;

                LinkSword.CreateSword();
            }
            SlashSound.Play();
        }
        public void UseItem(IItem item)
        {
            if (!linkLock)
            {

                // Only use the item if it isn't already in use
               if (!item.InUse)
               {
                    state.UseItemState();

                    if (item is ActiveBoomerang) item = new RicochetBoomerang((BoomerangDecorator)item);
                    item.Use(); 

                    timerOn = true;
                    timer = 20;
                    linkLock = true;
               }
            }                         
        }
        
        public void Damage()
        {
            DamageSound = SoundFactory.Instance.getSound(Sounds.LinkHurtSound);

            health = UIManager.Instance.LinkHealth;
            if (health > 0)
            {
                health--;
            }

            UIManager.Instance.SetHealth(health);
            DamageSound.Play();
            
            GameObjectManager.Instance.Remove(this);
            GameObjectManager.Instance.mLink = new DamagedLink(this);
            GameObjectManager.Instance.Add((GameObject)GameObjectManager.Instance.mLink);
        }
        public void Die()
        {
            state.LoseState();
            GameManager.Instance.gameState.Lose();
            linkLock = false;
        }

        public void Win()
        {
            linkLock = true;
            if (!(state is LinkWinState))
            {
                state.WinState();
            }
        }

        public override void Update(GameTime gameTime)
        {
           // Checks for any timers on link (attacking, using item, taking damage)
            if (timerOn)
            {
                if (timer > 0)
                {
                    sprite.Update(gameTime);
                    timer--;
                }
                else // Unlocks link after timer is done
                {
                    linkLock = false;
                    Idle();
                    timerOn = false;               
                }
            }
            else
            {
                LinkSprite.Update(gameTime);
            }

            // Dies when health reaches 0
            if (health <= 0)
            {
                Die();
            }

        }
        // Draw method from GameObject
        public override void Draw(SpriteBatch spriteBatch)
        {
            LinkSprite.Draw(spriteBatch, Location);
        }

        public override String GetCollidableType()
        {
            return "Player";
        }

      

    }

}
