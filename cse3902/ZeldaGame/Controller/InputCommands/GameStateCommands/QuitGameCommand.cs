using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class QuitGame : ICommand
    {
        public QuitGame()
        {

        }

        public void Execute()
        {
            System.Environment.Exit(0);
        }
    }
}
