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
    public class AnimatedHeartItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        private ISound Sound { get; set; }

        public AnimatedHeartItem()
        {
            InUse = false;
            Sound = SoundFactory.Instance.getSound(Sounds.HeartSound);
            this.objectManager = GameObjectManager.Instance;
            sprite = SpriteFactory.Instance.getSprite(Sprite.AnimHearts);
            Location = new Vector2(100, 400);
        }

        public void Use()
        {

        }

        public void CollectItem()
        {
            objectManager.Remove(this);
            UIManager.Instance.AddHealth();

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
