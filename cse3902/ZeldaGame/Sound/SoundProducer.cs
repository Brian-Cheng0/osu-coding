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

namespace ZeldaGame
{
    public class SoundProducer : ISound
    {
        public SoundEffect Sound { get; set; }
        public SoundEffectInstance SoundInstance;
      

        public SoundProducer(SoundEffect sound)
        {

            Sound = sound;
        }

        public void Play()
        {
            
            Sound.Play();
        }

        public SoundEffectInstance PlayLooped(){
      
            SoundInstance = Sound.CreateInstance();
            SoundInstance.IsLooped = true;
            SoundInstance.Play();

            return SoundInstance;
        }

        


    }
}
