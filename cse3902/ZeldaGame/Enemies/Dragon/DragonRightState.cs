using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class DragonRightState : IEnemyState
    {
        private MasterDragon dragon;
        public IEnemyState oppositeState { get; set; }

        public DragonRightState(MasterDragon dragon)
        {
            this.dragon = dragon;
            dragon.sprite = SpriteFactory.Instance.getSprite(Sprite.Dragon);
        }
        public void WalkLeft()
        {
            dragon.state = new DragonLeftState(dragon);
        }

        public void WalkRight()
        {
            // Already in right state
        }
        public void WalkUp()
        {
            //does not go up
        }

        public void WalkDown()
        {
            //does not go down
        }

        public void AttackState()
        {
            dragon.state = new RightAttackingDragonState(dragon);
        }

        public void DamageState()
        {
            //does not have a damage state
        }
        public void Update(int speed)
        {
            dragon.currentLocation.X += speed;
        }
    }

}
