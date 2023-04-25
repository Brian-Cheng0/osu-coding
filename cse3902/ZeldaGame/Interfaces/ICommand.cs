using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    // Interface for all keyboard commands
    public interface ICommand
    {
        public void Execute();
    }
}
