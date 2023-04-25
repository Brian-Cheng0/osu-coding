using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class PlayState : IGameState
    {
        public void Play()
        {
            
        }

        public void Win()
        {
            GameManager.Instance.gameState = new WinState();
            GameObjectManager.Instance.mLink.Win();
            GameManager.Instance.isUpdatingAndDrawing = false;
            GameManager.Instance.winOrLose = "win";
            GameManager.Instance.soundInstance.Stop();
            GameManager.Instance.sound = SoundFactory.Instance.getSound(Sounds.WinSound);
            GameManager.Instance.sound.Play();
        }

        public void Lose()
        {
            GameManager.Instance.gameState = new LoseState();
            GameManager.Instance.isUpdatingAndDrawing = false;
            GameManager.Instance.winOrLose = "lose";
            GameManager.Instance.soundInstance.Stop();
            GameManager.Instance.sound = SoundFactory.Instance.getSound(Sounds.LinkDeathSound);
            GameManager.Instance.sound.Play();
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

            GameObjectManager.Instance.Update(gameTime);
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
