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
    public class RupeeItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        private ISound Sound { get; set; }

        public RupeeItem()
        {
            InUse = false;
            Sound = SoundFactory.Instance.getSound(Sounds.RupeeSound);
            this.objectManager = GameObjectManager.Instance;
            sprite = SpriteFactory.Instance.getSprite(Sprite.AnimCrystal);
            Location = new Vector2(200, 400);
        }

        public void Use()
        {

        }

        public void setLocation(Vector2 location)
        {
            Location = location;
        }

        public void CollectItem()
        {
            objectManager.Remove(this);
            UIManager.Instance.IncrementItemCount("Rupee");
            Sound.Play();
        } 
        public void Impact()
        {
            // Does not impact
        }
        public override String GetCollidableType()
        {
            return "CollectableItem";
        }
    }
}
