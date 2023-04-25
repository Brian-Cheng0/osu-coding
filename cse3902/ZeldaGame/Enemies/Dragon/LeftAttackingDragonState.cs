using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class LeftAttackingDragonState : IEnemyState
    {
        private MasterDragon dragon;
        public IEnemyState oppositeState { get; set; }
        public LeftAttackingDragonState(MasterDragon dragon)
        {
            this.dragon = dragon;
            dragon.sprite = SpriteFactory.Instance.getSprite(Sprite.DragonAttack);
        }
        public void WalkUp()
        {
            // Does not walk up
        }

        public void WalkDown()
        {
            // Does not walk down
        }

        public void WalkLeft()
        {
            dragon.state = new DragonRightState(dragon);
        }

        public void WalkRight()
        {
            dragon.state = new DragonRightState(dragon);
        }

        public void AttackState()
        {
            // TODO: implement attack state
        }
    
        public void DamageState()
        {
            //not implemented for dragon at this current moment he will just die from one hit
        }
        public void Update(int speed)
        {
            
        }
    }

}
