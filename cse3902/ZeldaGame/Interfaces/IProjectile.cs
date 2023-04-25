using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public interface IProjectile
    {
        void AdjustUp();
        void AdjustDown();
        void AdjustLeft();
        void AdjustRight();
    }
}
