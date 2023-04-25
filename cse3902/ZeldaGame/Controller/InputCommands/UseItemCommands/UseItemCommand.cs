using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class UseItemCommand : ICommand
    {
        public GameObjectManager objectManager;
        public UseItemCommand(GameObjectManager objectManager)
        {
            this.objectManager = objectManager;
        }

        public void Execute()
        { 
            // TODO: if you remove boomerang from the dict in UI manager then this will break

            if (SelectableBoomerang.Instance.isInUse)
            { 
                objectManager.mLink.UseItem(objectManager.mLink.LinkBoomerang);
            }
            else if (SelectableBomb.Instance.isInUse && UIManager.Instance.objToCount["Bomb"] > 0)
            {
                objectManager.mLink.UseItem(new BombItem());
            }   
            else if (SelectableBow.Instance.isInUse)
            {
                objectManager.mLink.UseItem((IItem)objectManager.mLink.LinkBow);
            }
        }
    }
}
