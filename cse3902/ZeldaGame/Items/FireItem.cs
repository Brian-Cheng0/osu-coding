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
    public class FireItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        public FireItem()
        {
            InUse = false;
            this.objectManager = GameObjectManager.Instance;
            sprite = SpriteFactory.Instance.getSprite(Sprite.LinkFire);
            Location = new Vector2(200, 300);
        }  

        public void Use()
        {

        }

        public void CollectItem()
        {
            objectManager.Remove(this);
            //TODO: implement the functionality, are we actually using this?
        }
        public void Impact()
        {
            // Does not impact
        }
        public override String GetCollidableType()
        {
            return "TransparentBlock";
        }
    }
}
