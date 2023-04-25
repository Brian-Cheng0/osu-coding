using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ZeldaGame
{
    public class MouseController : IController
    {
        MouseState oldState;

        Rectangle upDoor = new Rectangle(317, 150, 130, 130);
        Rectangle downDoor = new Rectangle(317, 600, 130, 130);
        Rectangle rightDoor = new Rectangle(635, 350, 130, 130);
        Rectangle leftDoor = new Rectangle(0, 350, 130, 130);

        // TODO: bring level loading out of MouseController
        public MouseController() 
        {

        }
        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            int x = mouseState.X;
            int y = mouseState.Y;
            var mousePosition = new Point(x, y);

            bool isClicking = mouseState.LeftButton == ButtonState.Released;
            bool hasBeenClicked = oldState.LeftButton == ButtonState.Pressed;

            if (isClicking && hasBeenClicked)
            {
/*                objManager.updatables.Clear();
                objManager.drawables.Clear();
                objManager.staticCollidables.Clear();
                objManager.dynamicCollidables.Clear();*/
                if (leftDoor.Contains(mousePosition))
                {
                    LevelManager.Instance.SwitchRoom("left");
                    GameObjectManager.Instance.mLink.Location -= new Vector2(768,0);
                }
                else if (rightDoor.Contains(mousePosition))
                {
                    LevelManager.Instance.SwitchRoom("right");
                    GameObjectManager.Instance.mLink.Location += new Vector2(768, 0);
                }
                else if (upDoor.Contains(mousePosition))
                {
                    LevelManager.Instance.SwitchRoom("up");
                    GameObjectManager.Instance.mLink.Location -= new Vector2(0, 528);
                }
                else if (downDoor.Contains(mousePosition))
                {
                    LevelManager.Instance.SwitchRoom("down");
                    GameObjectManager.Instance.mLink.Location += new Vector2(0, 528);
                }                
            }
            oldState = mouseState;
        }
    }
}
