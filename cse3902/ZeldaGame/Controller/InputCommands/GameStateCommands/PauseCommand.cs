using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class PauseCommand : ICommand
    {
        public PauseCommand()
        {

        }

        public void Execute()
        {
            if (GameManager.Instance.gameState is PauseState)
            {
                GameManager.Instance.gameState.Play();
            }
            else
            {
                GameManager.Instance.gameState.Pause();
            } 
        }
    }

}
