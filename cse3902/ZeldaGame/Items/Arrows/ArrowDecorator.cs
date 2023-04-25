using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public abstract class ArrowDecorator : GameObject, IProjectile, IItem, IUpdatable, IDrawable, ICollidable
    {
        public bool InUse { get; set; }
        public int Price { get; set; }
        public Vector2 originalLocation { get; set; }
        public Direction direction { get; set; }
        public abstract void AdjustUp();
        public abstract void AdjustDown();
        public abstract void AdjustLeft();
        public abstract void AdjustRight();

        public abstract void CollectItem();
        public abstract void Use();
        public abstract void Impact();
        public abstract void AddDecoratorToEnemy(IEnemy enemy);

        public override string GetCollidableType()
        {
            return "PlayerProjectile";
        }
    }
}
