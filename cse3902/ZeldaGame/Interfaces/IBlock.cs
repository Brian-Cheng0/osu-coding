using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ZeldaGame
{
    public interface IBlock
    {
        void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch);

    }
}  
