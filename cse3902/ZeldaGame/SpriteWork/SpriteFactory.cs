using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ZeldaGame
{
    public class SpriteFactory
    {
        private static SpriteFactory instance = new SpriteFactory();

        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        // Lists to store sprite info
        public List<string> textureNames;
        public List<List<Rectangle>> arraysOfFrames;
        public List<int> delays;
        public List<double> scales;

        // Text
        public SpriteFont zeldaText;
        public SpriteXMLReader spriteReader;
        public ContentManager contentMng;

        private SpriteFactory()
        {
            textureNames = new List<string>();
            arraysOfFrames = new List<List<Rectangle>>();
            delays = new List<int>();
            scales = new List<double>();

            spriteReader = new SpriteXMLReader();
        }

        public void LoadAllTextures(ContentManager content)
        {
            contentMng = content;
            spriteReader.populateAllSpriteLists();

            // Text
            zeldaText = content.Load<SpriteFont>("PressStart2P");    
        }

        public ISprite getSprite(Sprite sprite)
        {
            int spriteIdx = (int)sprite;
            Texture2D texture = contentMng.Load<Texture2D>(textureNames[spriteIdx]);

            List<Rectangle> frames = new List<Rectangle>();
            for (int i = 0; i < arraysOfFrames[spriteIdx].Count; i++)
            {
                frames.Add(arraysOfFrames[spriteIdx][i]);
            }

            return new SpriteAnimator(texture, frames, delays[spriteIdx], scales[spriteIdx]);
        }                               
    }
}
