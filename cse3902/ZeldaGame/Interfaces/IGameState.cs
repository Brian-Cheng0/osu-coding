using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ZeldaGame
{
    public interface IGameState
    {
        void Play();
        void Win();
        void Lose();
        void Pause();
        void RoomTransition();
        void ItemSelection();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}  
