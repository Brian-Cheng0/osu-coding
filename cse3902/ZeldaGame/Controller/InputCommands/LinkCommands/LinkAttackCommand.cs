using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class LinkAttack : ICommand
    {
        public GameObjectManager objectManager;
        public LinkAttack(GameObjectManager objectManager)
        {
            this.objectManager = objectManager;
        }

        public void Execute()
        {
            objectManager.mLink.Attack();
        }
    }
}
