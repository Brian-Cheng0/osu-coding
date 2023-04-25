using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeldaGame
{

    public class ItemSelector : IUpdatable, IDrawable
    {
        private ItemSelectionDisplay selectionDisplay;
        private ISprite selector;
        private Vector2 Location;

        public ItemSelector(ItemSelectionDisplay selectionDisplay)
        {
            this.selectionDisplay = selectionDisplay;
            selector = SpriteFactory.Instance.getSprite(Sprite.ItemSelector);
            Location = selectionDisplay.displacementArray[0, 0];

        }

        public void Update(GameTime gameTime)
        {
            UpdateLocation();
            selector.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            selector.Draw(spriteBatch, Location);
        }

        public void UpdateLocation()
        {
            int row = selectionDisplay.selectorRow;
            int col = selectionDisplay.selectorCol; 
            Location = UIManager.Instance.baseLocation + selectionDisplay.displacementArray[row, col];
        }
    }
}
