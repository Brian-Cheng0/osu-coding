using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using ZeldaGame.BlackClosingScreen;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class PlayerWinCommand : ICollisionCommand
    {
        private ILink player;

        public PlayerWinCommand()
        {

        }

        public void Execute(ICollidable obj1, ICollidable obj2, Rectangle overlap)
        {
            GameManager.Instance.gameState.Win();

        }
    }
}
