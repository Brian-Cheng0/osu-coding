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
    public class FrostSwordItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        private ISound Sound { get; set; }       
        public FrostSwordItem()
        {
            InUse = false;
            this.objectManager = GameObjectManager.Instance;
            Sound = SoundFactory.Instance.getSound(Sounds.ItemSound);
            sprite = SpriteFactory.Instance.getSprite(Sprite.FrostSwordDisplay);
            Location = new Vector2(200, 300);
            Price = 2;
        }

        public void Use()
        {

        }

        public void CollectItem()
        {
            objectManager.Remove(this);
            GameObjectManager.Instance.mLink.LinkSword = new FrostSword(GameObjectManager.Instance.mLink.LinkSword);
            Sound.Play();
        }
        public void Impact()
        {
            // Does not impact
        }
        public override String GetCollidableType()
        {
            return "PurchasableItem";
        }
    }
}
