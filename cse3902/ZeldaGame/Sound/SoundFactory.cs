using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Metadata;

namespace ZeldaGame
{
    public class SoundFactory
    {
        private static SoundFactory instance = new SoundFactory();

        public static SoundFactory Instance
        {
            get
            {
                return instance;
            }
        }
        public List<string> soundNames;
        public SoundXMLReader soundReader;
        public ContentManager contentMng;
        private SoundFactory()
        {
            soundNames = new List<string>();
            soundReader = new SoundXMLReader();
        }

        public void populateAllSoundLists(ContentManager content)
        {
            contentMng = content;
            soundReader.populateAllSoundLists();
        }

        public ISound getSound(Sounds sound)
        {
            int soundIdx = (int)sound;
            SoundEffect soundEffect = contentMng.Load<SoundEffect>(soundNames[soundIdx]);

            return new SoundProducer(soundEffect);
        }
    }
}