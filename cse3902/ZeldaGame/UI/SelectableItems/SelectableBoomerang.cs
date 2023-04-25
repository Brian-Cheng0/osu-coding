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
   
    public class SelectableBoomerang : ISelectable
    {
        private static SelectableBoomerang instance = new SelectableBoomerang();

        public static SelectableBoomerang Instance
        {
            get
            {
                return instance;
            }
        }

        private ISprite inventoryBoomerangSprite;
        public Vector2 selectableItemLocation; // Location in top right blue box of inventory
        private Vector2 currentItemLocation;    // Location in top left blue box of inventory
        private Vector2 BItemLocation;  // Location in "B" on overhead inventory 

        private Vector2 selectableItemDisplacement;
        private Vector2 currentItemDisplacement;
        private Vector2 BItemDisplacement;

        public Boolean isInUse { get; set; }
        private List<Vector2> locationsToDisplaySprite;

        public Boolean hasBeenCollected { get; set; }
        public SelectableBoomerang()
        {
            hasBeenCollected = false;
            isInUse = false; 

            inventoryBoomerangSprite = SpriteFactory.Instance.getSprite(Sprite.Boomerang);

            selectableItemLocation = Vector2.Zero;
            currentItemLocation = Vector2.Zero;
            BItemLocation = Vector2.Zero;

            selectableItemDisplacement = new Vector2(390, -370); 
            currentItemDisplacement = new Vector2(208, -365);
            BItemDisplacement = new Vector2(385, 85);

            locationsToDisplaySprite = new List<Vector2>();
            locationsToDisplaySprite.Add(selectableItemLocation);  // By default show the sprite in the item selection

        }

        public void Update(GameTime gameTime)
        {
            selectableItemLocation = UIManager.Instance.baseLocation + selectableItemDisplacement;
            currentItemLocation = UIManager.Instance.baseLocation + currentItemDisplacement;
            BItemLocation = UIManager.Instance.baseLocation + BItemDisplacement;
            
            inventoryBoomerangSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (hasBeenCollected)
            {                
                inventoryBoomerangSprite.Draw(spriteBatch, selectableItemLocation);

                if (isInUse)
                {
                    inventoryBoomerangSprite.Draw(spriteBatch, currentItemLocation);
                    inventoryBoomerangSprite.Draw(spriteBatch, BItemLocation);
                }              
            }
        }

        public void ChangeSelectionLocation(int i, int j)
        {
            // Note: The x and y are flopped due to how to the 2D array works
            selectableItemDisplacement.Y += (i * 50);
            selectableItemDisplacement.X += (j * 50);
        }

        public void SelectItem()
        {
            isInUse = true;
        }
        public void DeselectItem()
        {
            isInUse = false;
        }
    }

}
