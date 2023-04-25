using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class RightStandingLinkState : ILinkState
    {
        private ILink link;

        public RightStandingLinkState(ILink link)
        {
            this.link = link;
            link.LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkRightIdle);
            link.currentDirection = Direction.Right;
        }
        public void IdleState()
        {

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
            link.state = new RightMovingLinkState(link);
        }
        public void AttackState()
        {
            link.state = new RightAttackingLinkState(link);
        }
        public void DamageState()
        {
            //link.state = new LinkDamageState(link);
            link = new DamagedLink(link);
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
