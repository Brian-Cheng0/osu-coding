using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public abstract class EnemyDecorator : GameObject, IEnemy, IUpdatable, IDrawable, ICollidable
    {
        public IEnemyState state;
        public int Health { get; set; }
        public int Speed { get; set; }
        public abstract void MoveUp();
        public abstract void MoveDown();
        public abstract void MoveLeft();
        public abstract void MoveRight();
        public abstract void Attack(); // Enemy attack may change
        public abstract void TakeDamage(int damageTaken);
        public abstract void Die();
        public abstract void FreezeEnemy(int speed);

        public override String GetCollidableType()
        {
            return "Enemy";
        }
    }
}
