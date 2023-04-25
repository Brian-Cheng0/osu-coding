using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class MoveSelectorLeft : ICommand
    {
        public MoveSelectorLeft()
        {

        }

        public void Execute()
        {
            UIManager.Instance.selectableItemDisplay.MoveSelectorLeft();
        }
    }
}
