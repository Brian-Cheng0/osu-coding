using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZeldaGame
{
    public class ForwardMovingLinkState : ILinkState
    {
        private ILink link;

        public ForwardMovingLinkState(ILink link)
        {
            this.link = link;
            link.LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingForward);
            link.currentDirection = Direction.Up;
        }
        public void IdleState()
        {
            link.state = new ForwardStandingLinkState(link);
        }
        public void WalkUp()
        {
            Vector2 tempLocation = link.Location;
            tempLocation.Y -= 5;
            link.Location = tempLocation;
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
            link.state = new RightMovingLinkState(link);
        }
        public void AttackState()
        {
            link.state = new ForwardAttackingLinkState(link);
        }
        public void DamageState()
        {
        //    link.state = new LinkDamageState(link);
        //    link.currentLocation.Y += 20; // TODO: this movement should be brought into the PlayerTakeDamageCommand in collision handler and use the overlap rectangle
            link = new DamagedLink(link);
        }
        public void UseItemState()
        {
            link.state = new UseItemUp(link);
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
