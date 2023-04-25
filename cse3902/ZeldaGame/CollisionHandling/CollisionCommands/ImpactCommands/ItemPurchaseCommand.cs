using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class ItemPurchaseCommand : ICollisionCommand
    {
        private IItem item;
        public ItemPurchaseCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            item = obj2 as IItem;
            if (UIManager.Instance.objToCount["Rupee"] >= item.Price && ShopManager.Instance.numOfItemsPurchased < 1)
            {
                item.CollectItem();
                for (int i = 0; i < item.Price; i++)
                {
                    UIManager.Instance.DecrementItemCount("Rupee");
                }
                ShopManager.Instance.pricesToDraw.Remove(item.Price);
                if (ShopManager.Instance.restock)
                {
                    ShopManager.Instance.itemToRemove = (GameObject)item;
                    ShopManager.Instance.restock = false;
                    ShopManager.Instance.numOfItemsPurchased++;
                }               
            }
        }

    }
}

