using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class MoveSelectorRight : ICommand
    {

        public MoveSelectorRight()
        {

        }

        public void Execute()
        {
            UIManager.Instance.selectableItemDisplay.MoveSelectorRight();
        }
    }
}
