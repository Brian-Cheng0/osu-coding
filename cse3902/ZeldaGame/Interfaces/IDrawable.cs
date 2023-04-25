using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ZeldaGame
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
