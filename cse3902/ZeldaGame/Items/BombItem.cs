using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ZeldaGame.Objects;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace ZeldaGame
{
    public class BombItem : GameObject, IItem, IUpdatable, IDrawable, ICollidable
    {
        private GameObjectManager objectManager;
        private ILink link;
        public Boolean InUse { get; set; }
        public int Price { get; set; }
        private ISound BlowSound { get; set; }
        private ISound DropSound { get; set; }
        private ISound PickedSound { get; set; }

        private int bombDetonateTimer;
        private int magnitudeX = 45;
        private int magnitudeY = 45;
        private Direction direction;
        private int explosionTimer;
        private bool startDetonation;
        private String collidableType = "CollectableItem";

        public BombItem()
        {
            this.objectManager = GameObjectManager.Instance;
            link = objectManager.mLink;
            InUse = false;
            BlowSound = SoundFactory.Instance.getSound(Sounds.BombBlowSound);
            DropSound = SoundFactory.Instance.getSound(Sounds.BombDropSound);
            PickedSound = SoundFactory.Instance.getSound(Sounds.ItemSound);
            sprite = SpriteFactory.Instance.getSprite(Sprite.Bomb);
            explosionTimer = 20;
            bombDetonateTimer = 60;
            startDetonation = false;
        }
        public override void Update(GameTime gameTime)
        {
            if (InUse) // The bomb was placed
            {
                collidableType = "PassiveItem"; // This name is not in the collision dicitonary
                if (!startDetonation) 
                {
                    sprite.Update(gameTime);
                }
                if (bombDetonateTimer == 0) // The bomb detonates
                {
                    collidableType = "Explosive"; // It now has functionality
                    BlowSound.Play();
                    if (explosionTimer != 0)
                    {
                        sprite.Update(gameTime);
                        explosionTimer--;
                    }
                    else
                    {
                        objectManager.Remove(this); // finished exploding, removes bomb
                    }
                }
                switch (direction)
                {
                    case Direction.Up:
                        AdjustUp();
                        break;
                    case Direction.Down:
                        AdjustDown();
                        break;
                    case Direction.Left:
                        AdjustLeft();
                        break;
                    case Direction.Right:
                        AdjustRight();
                        break;
                }
            }
        }

        // Draw call from GameObject
     
        
        public void CollectItem()
        {
            // Removes item from floor and adds it to UI inventory
            objectManager.Remove(this);
            UIManager.Instance.IncrementItemCount("Bomb");
            UIManager.Instance.selectableItemDisplay.AddSelectableBomb();
            PickedSound.Play();
        }
        public void Use()
        {
            InUse = true;
            currentLocation = link.Location;
            direction = link.currentDirection;
            sprite = SpriteFactory.Instance.getSprite(Sprite.BombExplosion); // Now when its used it will animate
            DropSound.Play();
            objectManager.Add(this);
            UIManager.Instance.DecrementItemCount("Bomb");
        }
        public void Impact()
        { 
            // TODO: does not impact like other projectiles
        }
        public override String GetCollidableType()
        {
            return collidableType;
        }
        public void AdjustUp()
        {
            if (!startDetonation)
            {
                base.currentLocation.Y -= magnitudeY;
                currentLocation.X -= magnitudeX;
                startDetonation = true;
            }
            if (bombDetonateTimer != 0)
            {
                bombDetonateTimer--;
            }
        }
        public void AdjustDown()
        {
            if (!startDetonation)
            {
                currentLocation.Y += magnitudeY;
                currentLocation.X -= magnitudeX;
                startDetonation = true;
            }
            if (bombDetonateTimer != 0)
            {
                bombDetonateTimer--;
            }
        }
        public void AdjustLeft()
        {
            if (!startDetonation)
            {
                currentLocation.X -= magnitudeX;
                currentLocation.Y -= magnitudeY;
                startDetonation = true;
            }
            if (bombDetonateTimer != 0)
            {
                bombDetonateTimer--;
            }
        }
        public void AdjustRight()
        {
            if (!startDetonation)
            {
                currentLocation.X += magnitudeX;
                currentLocation.Y -= magnitudeY;
                startDetonation = true;
            }
            if (bombDetonateTimer != 0)
            {
                bombDetonateTimer--;
            }
        }

    }
}

