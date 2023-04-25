using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class RightLockedDoor : GameObject, IDoor, IUpdatable, IDrawable, ICollidable
    {
        public String CollidableType { get; set; }
        public ISound Sound { get; set; }
        public RightLockedDoor()
        {
            CollidableType = "LockedDoor"; // Originally collides like a block
            sprite = SpriteFactory.Instance.getSprite(Sprite.RightLockedDoor);
            Location = new Vector2(200, 100); // currentLocation or Location? does it matter?
            Sound = SoundFactory.Instance.getSound(Sounds.DoorUnlockedSound);
        }
        public void ExplodeDoor()
        {
            // Does not explode
        }
        public override void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
        public void UnlockDoor()
        {
            // TODO: implement unlocking
            if (UIManager.Instance.objToCount["Key"] > 0)
            {
                UIManager.Instance.DecrementItemCount("Key");
                sprite = SpriteFactory.Instance.getSprite(Sprite.RightNormalDoor);
                CollidableType = "WalkableDoor";
                Sound.Play();
            }
        }
        public void LockDoor()
        {
            // TODO: implement locking
            CollidableType = "LockedDoor";
        }
        public override string GetCollidableType()
        {
            return CollidableType;
        }
    }
}
