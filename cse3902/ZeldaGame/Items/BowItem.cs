using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    //Yuyang Pan
    public class BowItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public bool InUse { get; set; }
        public int Price { get; set; }
        private ISound Sound { get; set; }
        public BowItem()
        {
            InUse = false;
            objectManager = GameObjectManager.Instance;
            Sound = SoundFactory.Instance.getSound(Sounds.ItemSound);
            sprite = SpriteFactory.Instance.getSprite(Sprite.Bow);
            Location = new Vector2(200, 300);
        }

        public void Use()
        {
         
        }

        public void CollectItem()
        {
            objectManager.Remove(this);
            UIManager.Instance.selectableItemDisplay.AddSelectableBow();
            Sound.Play();
        }
        public void Impact()
        {
            // Does not impact
        }

        public override string GetCollidableType()
        {
            return "CollectableItem";
        }
    }
}
