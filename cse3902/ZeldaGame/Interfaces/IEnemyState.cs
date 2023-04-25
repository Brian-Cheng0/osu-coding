using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public interface IEnemyState
    {
        void WalkUp();
        void WalkDown();
        void WalkLeft();
        void WalkRight();
        void AttackState();

        void DamageState();
        void Update(int speed);
    }
}
