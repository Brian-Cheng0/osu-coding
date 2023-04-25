﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace ZeldaGame
{
	public interface ISound
	{
		void Play();
		SoundEffectInstance PlayLooped();
	}
}

