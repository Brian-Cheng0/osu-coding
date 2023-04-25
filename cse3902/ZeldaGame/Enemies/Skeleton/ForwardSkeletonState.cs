using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeldaGame
{
    public class ForwardSkeletonState : IEnemyState
    {
        private MasterSkeleton skeleton;

        public ForwardSkeletonState(MasterSkeleton skeleton)
        {
            this.skeleton = skeleton;
            // Sprite is the same, no need to change it
        }
        public void WalkLeft()
        {
            skeleton.state = new LeftSkeletonState(skeleton);
        }

        public void WalkRight()
        {
            skeleton.state = new RightSkeletonState(skeleton);
        }
        public void WalkUp()
        {
            // Already in forward state
        }

        public void WalkDown()
        {
            skeleton.state = new BackwardSkeletonState(skeleton);
        }
        public void AttackState()
        {
            //does not have an attack state
        }

        public void DamageState()
        {
            //does not have a damage state
        }

        public void Update(int speed)
        {
            skeleton.currentLocation.Y -= speed;
        }

    }

}
