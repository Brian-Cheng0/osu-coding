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
    public class DamagedLink : GameObject, ILink, IUpdatable, IDrawable, ICollidable
    {
        ILink decoratedLink;
        public ISprite LinkSprite { get; set;}
        public ILinkState state { get; set; }
        public Vector2 Location { get; set; }
        public Direction currentDirection { get; set; }
  
        private ISound DamageSound { get; set; }
        private ISound SlashSound { get; set; }

        public Sword LinkSword { get; set; }
        public BoomerangDecorator LinkBoomerang { get; set; }
        public IBow LinkBow { get; set; }

        public bool linkLock { get; set; }

        private int DamageTimer = 10 ;

        public DamagedLink(ILink decoratedLink)
        {
            this.decoratedLink = decoratedLink;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingDamageBackward);
            LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingDamageBackward);


            LinkSword = decoratedLink.LinkSword;
            LinkBoomerang = decoratedLink.LinkBoomerang;
            LinkBow = decoratedLink.LinkBow;

        }

        public void Idle()
        {
            decoratedLink.Idle();          
        }
        // Changes to walking up state
        public void MoveUp()
        {
            
            decoratedLink.MoveUp();
        }
        // Changes to walking down state
        public void MoveDown()
        {
           
            decoratedLink.MoveDown();
        }
        // Changes to walking left state
        public void MoveLeft()
        {
           
            decoratedLink.MoveLeft();
        }
        // Changes to walking right state
        public void MoveRight()
        {
            
            decoratedLink.MoveRight();
        }
        // Changes to attacking state
        public void Attack()
        {
            // Can't attack when taking damage
        }
        public void UseItem(IItem item)
        {
          // Can't use item when taking damage
        }

        public void Damage()
        {
            // Does not take damage
        }

        public void Win()
        {
            decoratedLink.Win();
        }
        
        public void RemoveDecorator()
        {
            GameObjectManager.Instance.Remove(this);
            GameObjectManager.Instance.mLink = decoratedLink;
            GameObjectManager.Instance.Add((GameObject)GameObjectManager.Instance.mLink);           
        }
        public override void Update(GameTime gameTime)
        {
            DamageTimer--;
            if (DamageTimer == 0)
            {
                RemoveDecorator();
            }  
            switch (decoratedLink.currentDirection)
            {
                case Direction.Up:
                    KnockBackDown();
                    break;
                case Direction.Down:
                    KnockBackUp();
                    break;
                case Direction.Left:
                    KnockBackRight();
                    break;
                case Direction.Right:
                    KnockBackLeft();
                    break;
            }
            decoratedLink.Update(gameTime);      
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
             LinkSprite.Draw(spriteBatch, decoratedLink.Location);
        }

        public override String GetCollidableType()
        {
            return "Player";
        }

        public void KnockBackUp()
        {
            LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingDamageForward);
            Vector2 tempLocation = decoratedLink.Location;
            tempLocation.Y -= 6;
            decoratedLink.Location = tempLocation;
        }
        public void KnockBackDown()
        {
            LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingDamageBackward);
            Vector2 tempLocation = decoratedLink.Location;
            tempLocation.Y += 6;
            decoratedLink.Location = tempLocation;
        }
        public void KnockBackLeft()
        {
            LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingDamageLeft);
            Vector2 tempLocation = decoratedLink.Location;
            tempLocation.X -= 6;
            decoratedLink.Location = tempLocation;
        }
        public void KnockBackRight()
        {
            LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingDamageRight);
            Vector2 tempLocation = decoratedLink.Location;
            tempLocation.X += 6;
            decoratedLink.Location = tempLocation;
        }
    }

}
  