using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class GameManager
    {
        private static GameManager instance = new GameManager();

        public bool isUpdatingAndDrawing;
        private int timer;
        public string winOrLose;
        public SoundEffectInstance soundInstance;
        public ISound sound;

        public static GameManager Instance
        {
            get { return instance; }
        }

        public IGameState gameState;
        public GameManager()
        {
            gameState = new PlayState();
            isUpdatingAndDrawing = true;
            timer = 800;
            winOrLose = "neither";
            sound = SoundFactory.Instance.getSound(Sounds.UndergroundSound);
            soundInstance = sound.PlayLooped();
        }

        public void ResetGame()
        {
            gameState.Play();
            isUpdatingAndDrawing = true;
            timer = 800;
            winOrLose = "neither";
            GameObjectManager.Instance.Reset();
            LevelManager.Instance.Reset();
            LevelManager.Instance.LoadRoom();
            UIManager.Instance.Reset();
            UIManager.Instance.LoadContent();
            ShopManager.Instance.Reset();
            SelectableBoomerang.Instance.hasBeenCollected = false;
            SelectableBomb.Instance.hasBeenCollected = false;
            SelectableBow.Instance.hasBeenCollected = false;
            soundInstance.Play();
        }

        public void Update(GameTime gameTime)
        {
            if (isUpdatingAndDrawing)
            {
                gameState.Update(gameTime);
            }   
            else
            {
                // Start win countdown
                timer--;
            }
            if (winOrLose.Equals("lose") && timer > 650)
            {
                gameState.Update(gameTime);
            }
            else if (timer <= 0)
            {
                if (winOrLose.Equals("win"))
                {
                    System.Environment.Exit(0);
                }
                else if (winOrLose.Equals("lose")) 
                {                    
                    ResetGame();
                }               
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (timer > 300)
            {
                gameState.Draw(spriteBatch);
                spriteBatch.DrawString(SpriteFactory.Instance.zeldaText, "Buy some enchantments!", new Vector2(-1358, -1287), Color.White);
                spriteBatch.DrawString(SpriteFactory.Instance.zeldaText, "X", new Vector2(-1333, -1084), Color.White);
            }   
            else if (timer <= 200 && timer >= 400 && winOrLose.Equals("lose"))
            {
                gameState.Draw(spriteBatch);
                GameObjectManager.Instance.mLink.state.IdleState();
            }
            else if (timer < 250) 
            {
                if (winOrLose.Equals("win"))
                {
                    spriteBatch.DrawString(SpriteFactory.Instance.zeldaText, "Thanks for playing the game!", new Vector2(2450, -1800), Color.White);                 
                }
                else if (winOrLose.Equals("lose"))
                {
                    int x = (LevelManager.Instance.camera.x * -1) + 300;
                    int y = (LevelManager.Instance.camera.y * -1) + 350;
                    spriteBatch.DrawString(SpriteFactory.Instance.zeldaText, "GAME OVER", new Vector2(x, y), Color.White);
                }                
            }
        }
    }
}
