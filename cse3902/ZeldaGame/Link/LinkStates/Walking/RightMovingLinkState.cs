using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class RightMovingLinkState : ILinkState
    {
        private ILink link;

        public RightMovingLinkState(ILink link)
        {
            this.link = link;
            link.LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingRight);
            link.currentDirection = Direction.Right;
        }
        public void IdleState()
        {
            link.state = new RightStandingLinkState(link);
        }
        public void WalkUp()
        {
            link.state = new ForwardMovingLinkState(link);
        }

        public void WalkDown()
        {
            link.state = new BackwardMovingLinkState(link);
        }

        public void WalkLeft()
        {
            link.state = new LeftMovingLinkState(link);
        }

        public void WalkRight()
        {
            Vector2 tempLocation = link.Location;
            tempLocation.X += 5;
            link.Location = tempLocation;
        }
        public void AttackState()
        {
            link.state = new RightAttackingLinkState(link);
        }
        public void DamageState()
        {
            // link.state = new LinkDamageState(link);
            //link.currentLocation.X -= 20; // TODO: this movement should be brought into the PlayerTakeDamageCommand in collision handler and use the overlap rectangle  
            GameObjectManager.Instance.mLink = new DamagedLink(link); 
        }
        public void UseItemState()
        {
            link.state = new UseItemRight(link);
        }
        public void WinState()
        {
            link.state = new LinkWinState(link);
        }
        public void LoseState()
        {
            link.state = new LinkLoseState(link);
        }
    }

}
