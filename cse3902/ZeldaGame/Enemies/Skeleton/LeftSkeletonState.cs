using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class LeftSkeletonState : IEnemyState
    {
        private MasterSkeleton skeleton;

        public LeftSkeletonState(MasterSkeleton skeleton)
        {
            this.skeleton = skeleton;
            // Sprite is the same, no need to change it
        }
        public void WalkLeft()
        {
            // already in left state
        }

        public void WalkRight()
        {
            skeleton.state = new RightSkeletonState(skeleton);
        }
        public void WalkUp()
        {
            skeleton.state = new ForwardSkeletonState(skeleton);
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
            skeleton.currentLocation.X -= speed;
        }

    }

}
