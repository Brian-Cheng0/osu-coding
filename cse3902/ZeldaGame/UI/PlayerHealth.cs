using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections;

namespace ZeldaGame
{
    public class PlayerHealth : IUpdatable, IDrawable
    {
        public Vector2 displacementFromCamera;
        public Vector2 firstHeartLocation;
        public Vector2 secondHeartLocation;
        public Vector2 thirdHeartLocation;
        public Vector2 fourthHeartLocation;
        public Vector2 fifthHeartLocation;
        public Vector2 sixthHeartLocation;
        public Vector2 seventhHeartLocation;
        public Vector2 eighthHeartLocation;

        private ISprite firstHeart;
        private ISprite secondHeart;
        private ISprite thirdHeart;
        private ISprite fourthHeart;
        private ISprite fifthHeart;
        private ISprite sixthHeart;
        private ISprite seventhHeart;
        private ISprite eighthHeart;

        private Stack<ISprite> Health;
        private Stack<ISprite> tempStack;
        private Stack<ISprite> CollectableHealth;

        private int emptyHeartCount = 0;
        private int halfHeartCount = 0;
        private int fullHeartCount = 0;
        private int totalHearts;
        private Vector2[] locations;

        private int startingHealth;

        public PlayerHealth()
        {
            Health = new Stack<ISprite>();
            CollectableHealth = new Stack<ISprite>();
            tempStack = new Stack<ISprite>();

            firstHeart = SpriteFactory.Instance.getSprite(Sprite.FullHeart);
            secondHeart = SpriteFactory.Instance.getSprite(Sprite.FullHeart);
            thirdHeart = SpriteFactory.Instance.getSprite(Sprite.FullHeart);
            fourthHeart = SpriteFactory.Instance.getSprite(Sprite.NonexistantHeart);
            fifthHeart = SpriteFactory.Instance.getSprite(Sprite.NonexistantHeart);
            sixthHeart = SpriteFactory.Instance.getSprite(Sprite.NonexistantHeart);
            seventhHeart = SpriteFactory.Instance.getSprite(Sprite.NonexistantHeart);
            eighthHeart = SpriteFactory.Instance.getSprite(Sprite.NonexistantHeart);

            locations = new Vector2[8];
            totalHearts = 3;
            startingHealth = 6;
        }
        public void SetDisplacement(Vector2 displacementFromCamera)
        {
            this.displacementFromCamera = displacementFromCamera;
            firstHeartLocation = UIManager.Instance.baseLocation + displacementFromCamera;
            secondHeartLocation = firstHeartLocation + new Vector2(24, 0);
            thirdHeartLocation = secondHeartLocation + new Vector2(24, 0);
            fourthHeartLocation = thirdHeartLocation + new Vector2(24, 0);
            fifthHeartLocation = fourthHeartLocation + new Vector2(24, 0);
            sixthHeartLocation = fifthHeartLocation + new Vector2(24, 0);
            seventhHeartLocation = sixthHeartLocation + new Vector2(24, 0);
            eighthHeartLocation = seventhHeartLocation + new Vector2(24, 0);
        }

        public void InitializeHealthBarList()
        {
            Health.Push(firstHeart);
            Health.Push(secondHeart);
            Health.Push(thirdHeart);

            CollectableHealth.Push(eighthHeart);
            CollectableHealth.Push(seventhHeart);
            CollectableHealth.Push(sixthHeart);
            CollectableHealth.Push(fifthHeart);
            CollectableHealth.Push(fourthHeart);

            SetDisplacement(new Vector2(528, 95));

            locations[0] = firstHeartLocation;
            locations[1] = secondHeartLocation;
            locations[2] = thirdHeartLocation;
            locations[3] = fourthHeartLocation;
            locations[4] = fifthHeartLocation;
            locations[5] = sixthHeartLocation;
            locations[6] = seventhHeartLocation;
            locations[7] = eighthHeartLocation;
            
        }

        public void SetHeartSpriteType()
        {
            // Finds out which numbers to pull from the dictionary based on the item name
            emptyHeartCount = (UIManager.Instance.LinkMaxHealth - UIManager.Instance.LinkHealth) / 2;
            fullHeartCount = (UIManager.Instance.LinkHealth / 2);

            //To figure out if a heart container needs to be added
            int dummy = UIManager.Instance.LinkMaxHealth - startingHealth;
            if (dummy > 0)
            {
                Health.Push(CollectableHealth.Pop());
                startingHealth += 2;
                dummy--;
            }


            if (UIManager.Instance.LinkHealth % 2 == 0)
            {
                halfHeartCount = 0;
            }
            else
            {
                halfHeartCount = 1;
            }

            totalHearts = fullHeartCount + halfHeartCount + emptyHeartCount;

            int i = totalHearts;
            i = emptyHeartCount;
            while (i > 0)
            {
                var heart = Health.Pop();
                heart = SpriteFactory.Instance.getSprite(Sprite.EmptyHeart);
                tempStack.Push(heart);
                i--;
            }
            if (halfHeartCount == 1)
            {
                var heart = Health.Pop();
                heart = SpriteFactory.Instance.getSprite(Sprite.HalfHeart);
                tempStack.Push(heart);
            }
            i = fullHeartCount;
            while (i > 0)
            {
                var heart = Health.Pop();  
                heart = SpriteFactory.Instance.getSprite(Sprite.FullHeart);
                tempStack.Push(heart);
                i--;
            }
            int j = 0;
            while(j < totalHearts)
            {
                Health.Push(tempStack.Pop());
                j++;
            }
        }

        public void Update(GameTime gameTime)
        {
            SetHeartSpriteType();

            locations[0] = UIManager.Instance.baseLocation + displacementFromCamera;

            for (int i = 1; i < locations.Count(); i++)
            {
                locations[i] = locations[i - 1] + new Vector2(24, 0); ;
            }

            foreach (ISprite heart in Health)
            {
                heart.Update(gameTime);
            }
            
            foreach (ISprite heart in CollectableHealth)
            {
                heart.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int i = Health.Count() - 1;
            foreach (ISprite heart in Health)
            {
                heart.Draw(spriteBatch, locations[i]);
                i--;
            }
            if (CollectableHealth.Count() > 0)
            {
                i = 7;
                foreach (ISprite heart in CollectableHealth)
                {
                    heart.Draw(spriteBatch, locations[i]);
                    i--;
                }
            }
        }

    }
}
