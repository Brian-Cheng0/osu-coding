using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class LinkLoseState : ILinkState
    {
        private ILink link;

        public LinkLoseState(ILink link)
        {
            this.link = link;
            link.LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkLose);
        }
        public void IdleState()
        {
            if (!link.linkLock)
            {
                link.state = new BackwardStandingLinkState(link);
            }
        }

        public void WalkUp()
        {
            if (!link.linkLock)
            {
                link.state = new ForwardMovingLinkState(link);
            }
        }

        public void WalkDown()
        {
            if (!link.linkLock)
            {
                link.state = new BackwardMovingLinkState(link);
            }
        }

        public void WalkLeft()
        {
            if (!link.linkLock)
            {
                link.state = new LeftMovingLinkState(link);
            }
        }

        public void WalkRight()
        {
            if (!link.linkLock)
            {
                link.state = new RightMovingLinkState(link);
            }
        }
        public void AttackState()
        {
            if (!link.linkLock)
            {
                link.state = new BackwardAttackingLinkState(link);
            }
        }
        public void DamageState()
        {
            link = new DamagedLink(link);
        }
        public void UseItemState()
        {
            if (!link.linkLock)
            {
                link.state = new UseItemDown(link);
            }
        }
        public void WinState()
        {

        }

        public void LoseState()
        {

        }
    }
}
