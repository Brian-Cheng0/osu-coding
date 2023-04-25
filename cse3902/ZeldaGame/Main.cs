using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using ZeldaGame.Objects;
using Microsoft.Xna.Framework.Audio;

namespace ZeldaGame
{
    public class Game1 : Game
    {
        public ArrayList controllerList;
        public KeyController keyBoard;

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; 
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 768;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 700;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            controllerList = new ArrayList();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteFactory.Instance.LoadAllTextures(Content);
            UIManager.Instance.LoadContent(); // Used to initialize the inventory screen     
            GameObjectManager.Instance.LoadContent(); // Loads link
            SoundFactory.Instance.populateAllSoundLists(Content);

            controllerList.Add(new KeyController());
            controllerList.Add(new MouseController());
            LevelManager.Instance.LoadRoom();
        }

        protected override void Update(GameTime gameTime)
        {
            // Updates all controllers
            foreach (IController controller in controllerList)
            {
                controller.Update(gameTime);
            }

            GameManager.Instance.Update(gameTime);           

            base.Update(gameTime);
        }
         
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(transformMatrix: LevelManager.Instance.camera.Transform);

            GameManager.Instance.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
