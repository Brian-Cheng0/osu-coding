using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeldaGame
{
    public class ItemCounter : IUpdatable, IDrawable
    {
        private ItemCounterDisplay display;
        public string itemName;

        private Dictionary<int, ISprite> numbersToSprite;

        public Vector2 displacementFromCamera;
        public Vector2 tensDigitLocation;
        public Vector2 onesDigitLocation;
        private ISprite tensDigit;
        private ISprite onesDigit;

        private int itemCount = 0;

        public ItemCounter(ItemCounterDisplay display, string itemName, Dictionary<int, ISprite> numbersToSprite)
        {
            this.display = display;
            this.itemName = itemName;
            this.numbersToSprite = numbersToSprite;

            tensDigit = SpriteFactory.Instance.getSprite(Sprite.Num0);
            onesDigit = SpriteFactory.Instance.getSprite(Sprite.Num0);


        }
        public void DetermineNumberSprites()
        {
            // Finds out which numbers to pull from the dictionary based on the item name
            itemCount = UIManager.Instance.objToCount[itemName];
      
            int onesDigitInt = itemCount % 10;
            int tensDigitInt = (itemCount - onesDigitInt) / 10;

            tensDigit = numbersToSprite[tensDigitInt];
            onesDigit = numbersToSprite[onesDigitInt];
        }

        public void IncrementCount()
        {
            itemCount++;
        }
        public void DecrementCount()
        {
            itemCount--;
        }

        public void Update(GameTime gameTime)
        {
            DetermineNumberSprites();

            tensDigitLocation = UIManager.Instance.baseLocation + displacementFromCamera;
            onesDigitLocation = tensDigitLocation + new Vector2(24, 0);
                     
            tensDigit.Update(gameTime);
            onesDigit.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            tensDigit.Draw(spriteBatch, tensDigitLocation);
            onesDigit.Draw(spriteBatch, onesDigitLocation);
        } 
         
        public void SetDisplacement(Vector2 displacementFromCamera)
        {
            this.displacementFromCamera = displacementFromCamera;
            tensDigitLocation = UIManager.Instance.baseLocation + displacementFromCamera;
            onesDigitLocation = tensDigitLocation + new Vector2(24, 0);
        }
    }
}
