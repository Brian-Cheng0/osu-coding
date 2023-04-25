using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZeldaGame
{
    public class LeftMovingLinkState : ILinkState
    {
        private ILink link;

        public LeftMovingLinkState(ILink link)
        {
            this.link = link;
            link.LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkWalkingLeft);
            link.currentDirection = Direction.Left;
        }
        public void IdleState()
        {
            link.state = new LeftStandingLinkState(link);
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
            Vector2 tempLocation = link.Location;
            tempLocation.X -= 5;
            link.Location = tempLocation;
        }

        public void WalkRight()
        {
            link.state = new RightMovingLinkState(link);
        }
        public void AttackState()
        {
            link.state = new LeftAttackingLinkState(link);
        }
        public void DamageState()
        {
            // link.state = new LinkDamageState(link);
            //link.currentLocation.X += 20; // TODO: this movement should be brought into the PlayerTakeDamageCommand in collision handler and use the overlap rectangle
            link = new DamagedLink(link);
        }
        public void UseItemState()
        {
            link.state = new UseItemLeft(link);
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
