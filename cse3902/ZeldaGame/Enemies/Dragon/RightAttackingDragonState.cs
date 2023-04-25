using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class RightAttackingDragonState : IEnemyState
    {
        private MasterDragon dragon;
        public IEnemyState oppositeState { get; set; }
        public RightAttackingDragonState(MasterDragon dragon)
        {
            this.dragon = dragon;
            dragon.sprite = SpriteFactory.Instance.getSprite(Sprite.DragonAttack);
        }
        public void WalkUp()
        {
            //not implemented for dragon
        }

        public void WalkDown()
        {
            //not implemented for dragon
        }

        public void WalkLeft()
        {
            dragon.state = new DragonLeftState(dragon);
        }

        public void WalkRight()
        {
            dragon.state = new DragonRightState(dragon);
        }
        public void AttackState()
        {
          
        }
        public void AdjustPosition()
        {
            // come back to this, can link use item while attacking?
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
