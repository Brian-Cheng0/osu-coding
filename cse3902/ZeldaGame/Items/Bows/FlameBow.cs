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
    public class FlameBow : GameObject, IItem, IBow, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        public bool InUse { get; set; }
        public int Price { get; set; }
        private ISound Sound { get; set; }
        public FlameBow()
        {
            InUse = false;
            objectManager = GameObjectManager.Instance;
            sprite = SpriteFactory.Instance.getSprite(Sprite.Bow);
            Location = new Vector2(200, 300);
        }

        public void Use()
        {
            ShootArrow();
        }

        public void CollectItem()
        {
            // Does not collect
        }
        public void Impact()
        {   
            // Does not impact
        }
        public void ShootArrow()
        {
            ArrowDecorator arrow = new FlamingArrow(new NormalArrow());
            objectManager.Add(arrow);
            arrow.Use();
        }
        public override string GetCollidableType()
        {
            return "CollectableItem";
        }
    }
}
