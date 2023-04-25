using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class OpenInventory : ICommand
    {
        public OpenInventory()
        {

        }

        public void Execute()
        {
             LevelManager.Instance.toAndFromInventory("up");
        }
    }
}
