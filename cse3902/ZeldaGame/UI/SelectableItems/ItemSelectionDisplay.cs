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
using System.Xml.Serialization;

namespace ZeldaGame
{
   
    public class ItemSelectionDisplay : IUpdatable, IDrawable
    {
        public ISelectable[,] selectableItemsArray;
        public Vector2[,] displacementArray;
         
        public ItemSelector selector;

        private int row, col = 0;
        public int selectorRow, selectorCol = 0;

        public ItemSelectionDisplay()
        {         

            // Add more items to be selected here
            selectableItemsArray = new ISelectable[2, 5] {
                                                            { null, null, null, null, null },
                                                            { null, null, null, null, null }

            };
            displacementArray = new Vector2[2, 5]
            {
                { new Vector2(375, -385),new Vector2(425, -385),new Vector2(475, -385),new Vector2(525, -385),new Vector2(575, -385)},
                 { new Vector2(375, -323),new Vector2(425, -323),new Vector2(475, -323),new Vector2(525, -323),new Vector2(575, -323)}
            };
            
            selector = new ItemSelector(this);
        }
        

        public void Update(GameTime gameTime)
        {
            selector.Update(gameTime);

            foreach(ISelectable item in selectableItemsArray)
            {
                if (item != null) item.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            selector.Draw(spriteBatch);
            for (int i = 0; i < selectableItemsArray.GetLength(0); i++) 
            {
                for (int j = 0; j < selectableItemsArray.GetLength(1); j++)
                {
                    if (selectableItemsArray[i, j] != null)
                    {
                        selectableItemsArray[i, j].Draw(spriteBatch);
                    }             
                }
            }
        }

        public void AddSelectableBoomerang()
        {
            if (!SelectableBoomerang.Instance.hasBeenCollected)
            {
                selectableItemsArray[row, col] = SelectableBoomerang.Instance;
                selectableItemsArray[row, col].ChangeSelectionLocation(row, col);

                SelectableBoomerang.Instance.hasBeenCollected = true;

                col++;
                if (col > 4)
                {
                    col = 0;
                    row++;
                }
            }
                   
        }
        public void AddSelectableBomb()
        {
            if (!SelectableBomb.Instance.hasBeenCollected)
            {
                selectableItemsArray[row, col] = SelectableBomb.Instance;
                selectableItemsArray[row, col].ChangeSelectionLocation(row, col);
                SelectableBomb.Instance.hasBeenCollected = true;

                col++;
                if (col > 4)
                {
                    col = 0;
                    row++;
                }
            }        
        }
        public void AddSelectableBow()
        {
            if (!SelectableBow.Instance.hasBeenCollected)
            {
                selectableItemsArray[row, col] = SelectableBow.Instance;
                selectableItemsArray[row, col].ChangeSelectionLocation(row, col);
                SelectableBow.Instance.hasBeenCollected = true;

                col++;
                if (col > 4)
                {
                    col = 0;
                    row++;
                }
            }                        
        }

        public void MoveSelectorRight()
        {
            // move the selector right if its not at the end
            if (selectorCol < displacementArray.GetLength(1) - 1)
            {
                selectorCol++;
            } 
            else // Selector wraps around
            {
                selectorCol = 0;
            }
        }

        public void MoveSelectorLeft()
        {
            // move the selector right if its not at the end
            if (selectorCol > 0)
            {
                selectorCol--;
            }
            else // Selector wraps around
            {
                selectorCol = displacementArray.GetLength(1) - 1;
            }
        }
        public void MoveSelectorUp()
        {
            if (selectorRow > 0)
            {
                selectorRow--;
            }
            else // Selector wraps around
            {
                selectorRow = displacementArray.GetLength(0) - 1;
            }
        }

        public void MoveSelectorDown()
        {
            if (selectorRow < displacementArray.GetLength(0) - 1)
            {
                selectorRow++;
            }
            else // Selector wraps around
            {
                selectorRow = 0;
            }
        }

        public void SelectItem()
        {
            // Deselect all items
            foreach (ISelectable item in selectableItemsArray)
            {
                if (item != null) item.DeselectItem();
            }

            // Select the item we want
            if (selectableItemsArray[selectorRow, selectorCol] != null)
            {
                selectableItemsArray[selectorRow, selectorCol].SelectItem(); 
            }              
        }
    }
}
 