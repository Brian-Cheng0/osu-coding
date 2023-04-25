using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{
    public class ShopManager
    {
        private static ShopManager instance = new ShopManager();

        public static ShopManager Instance
        {
            get { return instance; }
        }

        public bool restock;

        public List<GameObject> enchantementsNotPlacedInShop;

        public Dictionary<int, Vector2> pricesToDraw;
        public GameObject[] itemsInShop;

        public GameObject itemToRemove;
        public int numOfItemsPurchased;

        public ShopManager()
        {
            enchantementsNotPlacedInShop = new List<GameObject>();
            pricesToDraw = new Dictionary<int, Vector2>(); 

            enchantementsNotPlacedInShop.Add(new FlameSwordItem());
            enchantementsNotPlacedInShop.Add(new FrostSwordItem());
            enchantementsNotPlacedInShop.Add(new ReinforcedSwordItem());
            enchantementsNotPlacedInShop.Add(new RicochetBoomerangItem());
            enchantementsNotPlacedInShop.Add(new FlameBowItem());
            enchantementsNotPlacedInShop.Add(new TriBowItem());

            itemsInShop = new GameObject[3];

            restock = true;

            numOfItemsPurchased = 0;
        }

        public List<GameObject> LoadAndStockShop()
        {

            List<GameObject> itemsToPutInShop = new List<GameObject>();
            int x = -1260, y = -1160;

            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                int index = rnd.Next(0, enchantementsNotPlacedInShop.Count);
                GameObject itemToPlace = enchantementsNotPlacedInShop[index];
                itemToPlace.currentLocation = new Vector2(x, y);
                IItem itemToGetPriceFrom = (IItem)itemToPlace;
                int price = itemToGetPriceFrom.Price;
                pricesToDraw.Add(price, new Vector2(x + 5, y + 75));
                x += 95;
                itemsToPutInShop.Add(itemToPlace);
                enchantementsNotPlacedInShop.Remove(itemToPlace);
                itemsInShop[i] = itemToPlace;
            }
            return itemsToPutInShop;
        }

        public void RestockShop()
        {
            int x = -1260, y = -1160;
            int slotToAdd = 0;
            
            for (int i = 0; i < itemsInShop.Length; i++)
            {
                if (itemsInShop[i].GetType() == itemToRemove.GetType())
                {
                    slotToAdd = i;                   
                    break;
                }
            }

            if (enchantementsNotPlacedInShop.Count != 0)
            {
                Random rnd = new Random();

                int index = rnd.Next(0, enchantementsNotPlacedInShop.Count);
                GameObject itemToPlace = enchantementsNotPlacedInShop[index];

                itemsInShop[slotToAdd] = itemToPlace;

                enchantementsNotPlacedInShop.Remove(itemToPlace);

                x += (95 * slotToAdd);

                itemToPlace.currentLocation = new Vector2(x, y);
                IItem itemToGetPriceFrom = (IItem)itemToPlace;
                int price = itemToGetPriceFrom.Price;
                y += 75;
                pricesToDraw.Add(price, new Vector2(x + 5, y));

                GameObjectManager.Instance.Add(itemToPlace);
            }
            else if (enchantementsNotPlacedInShop.Count == 0 && slotToAdd == 0)
            {
                itemsInShop[slotToAdd] = null;
            }
            numOfItemsPurchased = 0;
            itemToRemove = null;
            restock = true;
        }

        public void Reset()
        {
            pricesToDraw.Clear();
            enchantementsNotPlacedInShop.Clear();
            enchantementsNotPlacedInShop.Add(new FlameSwordItem());
            enchantementsNotPlacedInShop.Add(new FrostSwordItem());
            enchantementsNotPlacedInShop.Add(new ReinforcedSwordItem());
            enchantementsNotPlacedInShop.Add(new RicochetBoomerangItem());
            enchantementsNotPlacedInShop.Add(new FlameBowItem());
            enchantementsNotPlacedInShop.Add(new TriBowItem());
            numOfItemsPurchased = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<int, Vector2> price in pricesToDraw)
            {
                spriteBatch.DrawString(SpriteFactory.Instance.zeldaText, price.Key.ToString(), price.Value, Color.White);
            }
        }
    }
}
