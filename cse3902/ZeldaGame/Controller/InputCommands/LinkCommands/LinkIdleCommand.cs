using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class LinkIdle : ICommand
    {
        public GameObjectManager objectManager;
        public LinkIdle(GameObjectManager objectManager)
        {
            this.objectManager = objectManager;
        }

        public void Execute()
        {
            objectManager.mLink.Idle();
        }

    }
}
