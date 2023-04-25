using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class Camera
    {
        public int x;
        public int y;
        public int z;
        public Matrix Transform { get; private set; }
        public Matrix targetDestination { get; private set; }
        public Camera() {
            x = 0;
            y = 0;
            z = 0;
            Transform = Matrix.CreateTranslation(x, y, z);      
        }

        // Only for testing purposes
        public void MoveCameraInstant(string direction)
        {
            if (direction.Equals("left"))
            {
                x += 768;
            }
            else if (direction.Equals("right"))
            {
                x -= 768;
            }
            else if (direction.Equals("up"))
            {
                y += 528;
            }
            else if (direction.Equals("down"))
            {
                y -= 528;
            }
            Transform = Matrix.CreateTranslation(x, y, z);
        }

        public void SetCameraDestination(string direction)
        {
            if (direction.Equals("left"))
            {
                targetDestination = Matrix.CreateTranslation(x + 768, y, z);
            }
            else if (direction.Equals("right"))
            {
                targetDestination = Matrix.CreateTranslation(x - 768, y, z);
            }
            else if (direction.Equals("up"))
            {
                targetDestination = Matrix.CreateTranslation(x, y + 528, z);
            }
            else if (direction.Equals("down"))
            {
                targetDestination = Matrix.CreateTranslation(x, y - 528, z);
            }
        }

        public void DisplaceCamera(string direction)
        {
            if (direction.Equals("left"))
            {
                x += 8;
            }
            else if (direction.Equals("right"))
            {
                x -= 8;
            }
            else if (direction.Equals("up"))
            {
                y += 8;
            }
            else if (direction.Equals("down"))
            {
                y -= 8;
            }
            Transform = Matrix.CreateTranslation(x, y, z);
        }

        public bool stopMoving()
        {
            bool stop = false;
            if (Transform == targetDestination)
            {
                stop = true;
            }
            return stop;
        }

        public void SwitchCameraBasement(bool enterBasement)
        {

        }

        public void Clear()
        {
            x = 0;
            y = 0;
            z = 0;
            Transform = Matrix.CreateTranslation(x, y, z);
        }
    }
}
