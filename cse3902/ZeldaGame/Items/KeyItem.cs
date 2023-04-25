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
    public class KeyItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        public ISound Sound { get; set; }
        public KeyItem()
        {
            InUse = false;
            this.objectManager = GameObjectManager.Instance;
            sprite = SpriteFactory.Instance.getSprite(Sprite.Key);
            Location = new Vector2(200, 300);
            Sound = SoundFactory.Instance.getSound(Sounds.ItemSound);
        }
        public override void Update(GameTime gameTime)
        {
            if (InUse)
            {
                sprite.Update(gameTime);
                objectManager.Remove(this);
            }
        }
        public void Use()
        {
            InUse = true;
            UIManager.Instance.DecrementItemCount("Key");

        }

        public void CollectItem()
        {
            // Removes item from floor and adds it to UI inventory
            objectManager.Remove(this);
            UIManager.Instance.IncrementItemCount("Key");
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
