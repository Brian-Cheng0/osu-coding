using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeldaGame
{
    public class RightSkeletonState : IEnemyState
    {
        private MasterSkeleton skeleton;
        public int Speed { get; set; }

        public RightSkeletonState(MasterSkeleton skeleton)
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
            // Already in the right state 
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
            skeleton.currentLocation.X += speed;
        }

    }

}
