using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{
    public interface IEnemy
    {
        public int Health { get; set; }
        public int Speed { get; set; }
        public Vector2 Location { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void Attack();
        void TakeDamage(int damageTaken);
        void FreezeEnemy(int speed);
        void Die(); 
    }
}

