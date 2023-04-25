using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;
using ZeldaGame.Objects;
using System.Transactions;

namespace ZeldaGame
{
    //Yuyang Pan
    public class BoomerangItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        private ISound Sound { get; set; }
        public BoomerangItem()
        {
            InUse = false;
            this.objectManager = GameObjectManager.Instance;
            Sound = SoundFactory.Instance.getSound(Sounds.ItemSound);
            sprite = SpriteFactory.Instance.getSprite(Sprite.Boomerang);
            Location = new Vector2(200, 400);
        }

        public void Use()
        {
             // Does not use, technically it should but ActiveBoomerang does this
        }

        public void CollectItem()
        {
            objectManager.Remove(this);
            //UIManager.Instance.playerInventory.Add(this);
            UIManager.Instance.IncrementItemCount("Boomerang");
            UIManager.Instance.selectableItemDisplay.AddSelectableBoomerang();

            Sound.Play();
        }
        public void Impact()
        {

        }
        public override String GetCollidableType()
        {
            return "CollectableItem";
        }
    }
}
