using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class LoseState : IGameState
    {
        public void Play()
        {
            GameManager.Instance.gameState = new PlayState();
        }

        public void Win()
        {
            GameManager.Instance.gameState = new WinState();
        }

        public void Lose()
        {
            
        }

        public void Pause()
        {
            GameManager.Instance.gameState = new PauseState();
        }

        public void ItemSelection()
        {
            GameManager.Instance.gameState = new ItemSelectionState();
        }

        public void RoomTransition()
        {
            GameManager.Instance.gameState = new RoomTransitionState();
        }

        public void Update(GameTime gameTime)
        {
            GameObjectManager.Instance.mLink.Update(gameTime);
            LevelManager.Instance.Update();
            UIManager.Instance.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameObjectManager.Instance.Draw(spriteBatch);
            UIManager.Instance.Draw(spriteBatch);
            ShopManager.Instance.Draw(spriteBatch);
        }
    }
}
