﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class BackwardAttackingLinkState : ILinkState
    {
        private ILink link;

        public BackwardAttackingLinkState(ILink link)
        {
            this.link = link;
            link.LinkSprite = SpriteFactory.Instance.getSprite(Sprite.DownLinkAttack);
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
            // cant attack
        }

        public void UseItemState()
        {
            if (!link.linkLock)
            {
                link.state = new UseItemDown(link);
            }
        }

        public void DamageState()
        {
            //   link.state = new LinkDamageState(link);
            link = new DamagedLink(link);

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
