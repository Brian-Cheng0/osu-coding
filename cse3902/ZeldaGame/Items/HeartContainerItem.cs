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
    public class HeartContainerItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        public ISound Sound { get; set; }
        public HeartContainerItem()
        {
            InUse = false;
            this.objectManager = GameObjectManager.Instance;
            sprite = SpriteFactory.Instance.getSprite(Sprite.Heart);
            Location = new Vector2(500, 300);
            Sound = SoundFactory.Instance.getSound(Sounds.ItemSound);
        }

        public void Use()
        {
            // Is not used
        }

        public void CollectItem()
        {
            objectManager.Remove(this);
            UIManager.Instance.AddHeartContainer();
            Sound.Play();

        }
        public void setLocation(Vector2 location)
        {
            Location = location;
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
