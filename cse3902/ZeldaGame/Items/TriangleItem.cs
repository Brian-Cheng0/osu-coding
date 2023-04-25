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
    public class TriangleItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        public ISound Sound { get; set; }
        public TriangleItem()
        {
            InUse = false;
            this.objectManager = GameObjectManager.Instance;
            sprite = SpriteFactory.Instance.getSprite(Sprite.Triangle);
            Location = new Vector2(600, 300);
            Sound = SoundFactory.Instance.getSound(Sounds.Fanfare);
        }

        public void Use()
        {

        }

        public void CollectItem()
        {
            objectManager.Remove(this);
        }
        public void Impact()
        {
            // Does not impact
        }
        public override String GetCollidableType()
        {
            return "TriforcePiece";
        }
    }
}
