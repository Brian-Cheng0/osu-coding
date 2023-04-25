using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class DragonLeftState : IEnemyState
    {
        private MasterDragon dragon;
        public IEnemyState oppositeState { get; set; }
        public DragonLeftState(MasterDragon dragon)
        {
            this.dragon = dragon;
            dragon.sprite = SpriteFactory.Instance.getSprite(Sprite.Dragon);
        }
        public void WalkLeft()
        {
            // Already in left state
        }
        public void WalkRight()
        {
            dragon.state = new DragonRightState(dragon);
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
            dragon.state = new LeftAttackingDragonState(dragon);
        }
        public void DamageState()
        {
            //does not have a damage state
        }
        public void Update(int speed)
        {
            dragon.currentLocation.X -= speed;
        }

        
    }
}
