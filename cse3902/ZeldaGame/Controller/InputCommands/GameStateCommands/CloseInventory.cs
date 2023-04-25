using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class CloseInventory : ICommand
    {
        public CloseInventory()
        {

        }

        public void Execute()
        {
            LevelManager.Instance.toAndFromInventory("down");
        }
    }
}

