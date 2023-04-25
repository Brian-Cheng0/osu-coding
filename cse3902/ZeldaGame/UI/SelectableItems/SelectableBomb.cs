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
using ZeldaGame.Objects;

namespace ZeldaGame
{

    public class SelectableBomb : ISelectable
    {
        private static SelectableBomb instance = new SelectableBomb();

        public static SelectableBomb Instance
        {
            get
            {
                return instance;
            }
        }

        private ISprite inventoryBombSprite;

        public Vector2 selectableItemLocation; // Location in top right blue box of inventory
        private Vector2 currentItemLocation;    // Location in top left blue box of inventory
        private Vector2 BItemLocation;  // Location in "B" on overhead inventory 

        private Vector2 selectableItemDisplacement;
        private Vector2 currentItemDisplacement;
        private Vector2 BItemDisplacement;

        public Boolean hasBeenCollected { get; set; }
        public Boolean isInUse { get; set; }
        public SelectableBomb()
        {
            hasBeenCollected = false;
            isInUse = false;
            inventoryBombSprite = SpriteFactory.Instance.getSprite(Sprite.Bomb);

            selectableItemLocation = Vector2.Zero;
            currentItemLocation = Vector2.Zero;
            BItemLocation = Vector2.Zero;

            selectableItemDisplacement = new Vector2(390, -380);
            currentItemDisplacement = new Vector2(208, -380);
            BItemDisplacement = new Vector2(385, 70);

        }

        public void Update(GameTime gameTime)
        {
            //LinkBomb should be created as an object like LinkSword in MasterLink
            //if (GameObjectManager.Instance.mLink.LinkBomb is RangedBomb)
            //{
            //    inventoryBombSprite = SpriteFactory.Instance.getSprite(Sprite.RangedBombDisplay);
            //}
            selectableItemLocation = UIManager.Instance.baseLocation + selectableItemDisplacement;
            currentItemLocation = UIManager.Instance.baseLocation + currentItemDisplacement;
            BItemLocation = UIManager.Instance.baseLocation + BItemDisplacement;


            inventoryBombSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (hasBeenCollected)
            {
                inventoryBombSprite.Draw(spriteBatch, selectableItemLocation);
                if (isInUse)
                {
                    inventoryBombSprite.Draw(spriteBatch, currentItemLocation);
                    inventoryBombSprite.Draw(spriteBatch, BItemLocation);
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
