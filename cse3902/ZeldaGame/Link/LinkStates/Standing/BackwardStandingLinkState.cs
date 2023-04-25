using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class BackwardStandingLinkState : ILinkState
    {
        private ILink link;

        public BackwardStandingLinkState(ILink link)
        {
            this.link = link;
            link.LinkSprite = SpriteFactory.Instance.getSprite(Sprite.LinkBackwardIdle);
            link.currentDirection = Direction.Down;
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
            link.state = new BackwardAttackingLinkState(link);
        }

        public void DamageState()
        {
            //link.state = new LinkDamageState(link);
            link = new DamagedLink(link);
        }
        public void UseItemState()
        {
            link.state = new UseItemDown(link);
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
