using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{

    public interface ILink
    {
        ISprite LinkSprite { get; set; }
        Vector2 Location { get; set; }
        Direction currentDirection { get; set; }
        ILinkState state { get; set; }
        Sword LinkSword { get; set; }
        BoomerangDecorator LinkBoomerang { get; set; }
        IBow LinkBow { get; set; }
        Boolean linkLock { get; set; }
        void Idle();
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void Attack();
        void Damage();
        void UseItem(IItem item);
        void Win();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
         

    }
}
