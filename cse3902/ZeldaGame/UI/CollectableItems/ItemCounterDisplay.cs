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
    public class ItemCounterDisplay : IUpdatable
    {
        private Dictionary<int, ISprite> numbersToSprite;

        private ItemCounter rupeeCount;
        private ItemCounter keyCount;
        private ItemCounter bombCount;

        public ItemCounterDisplay()
        {
            numbersToSprite = new Dictionary<int, ISprite>();

            numbersToSprite.Add(0, SpriteFactory.Instance.getSprite(Sprite.Num0));
            numbersToSprite.Add(1, SpriteFactory.Instance.getSprite(Sprite.Num1));
            numbersToSprite.Add(2, SpriteFactory.Instance.getSprite(Sprite.Num2));
            numbersToSprite.Add(3, SpriteFactory.Instance.getSprite(Sprite.Num3));
            numbersToSprite.Add(4, SpriteFactory.Instance.getSprite(Sprite.Num4));
            numbersToSprite.Add(5, SpriteFactory.Instance.getSprite(Sprite.Num5));
            numbersToSprite.Add(6, SpriteFactory.Instance.getSprite(Sprite.Num6));
            numbersToSprite.Add(7, SpriteFactory.Instance.getSprite(Sprite.Num7));
            numbersToSprite.Add(8, SpriteFactory.Instance.getSprite(Sprite.Num8));
            numbersToSprite.Add(9, SpriteFactory.Instance.getSprite(Sprite.Num9));

            rupeeCount = new ItemCounter(this, "Rupee", numbersToSprite);
            keyCount = new ItemCounter(this, "Key", numbersToSprite);
            bombCount = new ItemCounter(this, "Bomb", numbersToSprite);

        }
        public void Update(GameTime gameTime)
        {
           
            rupeeCount.Update(gameTime);
            keyCount.Update(gameTime);
            bombCount.Update(gameTime); 
           
        }
        public void InitializeCounters()
        {
            UIManager.Instance.Add(this);

            // Adding counters to updatable and drawable lists
            UIManager.Instance.Add(rupeeCount);
            rupeeCount.SetDisplacement(new Vector2(312, 48));

            UIManager.Instance.Add(keyCount);
            keyCount.SetDisplacement(new Vector2(312, 94));

            UIManager.Instance.Add(bombCount);
            bombCount.SetDisplacement(new Vector2(312, 120));
       
        }

       
    }
}
