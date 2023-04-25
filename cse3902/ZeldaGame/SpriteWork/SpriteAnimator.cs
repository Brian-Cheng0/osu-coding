using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
     
    internal class SpriteAnimator : ISprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Size { get; set; }
        private List<Rectangle> frames;

        private int currentFrame;
        private int totalFrames;
        private int width; 
        private int height;

        private float elapsed;
        private float delay;

        private double scale;
        public SpriteAnimator(Texture2D texture, List<Rectangle> frames, int delay, double scale)
        {
            Texture = texture;

            this.frames = frames;
            currentFrame = 0;
            totalFrames = frames.Count;

            width = frames[currentFrame].Width; 
            height = frames[currentFrame].Height;

            this.delay = delay;
            this.scale = scale;

            this.Size = new Vector2(width * (int)scale, height * (int)scale); // Sets the size of the sprite
         
        }
        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                currentFrame++;
                if (currentFrame >= totalFrames)
                {
                    currentFrame = 0;
                }
                elapsed = 0;
            }
          
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        { 
            Rectangle sourceRectangle = frames[currentFrame];

            width = frames[currentFrame].Width;
            height = frames[currentFrame].Height;
            
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * (int) scale, height * (int) scale);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

    }
}
