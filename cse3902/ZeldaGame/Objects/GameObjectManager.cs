using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZeldaGame.Objects
{
    public class GameObjectManager
    {
        private static GameObjectManager instance = new GameObjectManager();

        public static GameObjectManager Instance
        {
            get { return instance; }
        }
        
        public CollisionDetector collisionDetector;

        public ILink mLink;

        public List<IRoom> loadedRooms;

        public List<IUpdatable> updatables;
        public List<IDrawable> drawables;
        public List<ICollidable> staticCollidables;
        public List<ICollidable> dynamicCollidables;

        // Acts as the Initialize() function in main
        public GameObjectManager()
        {
            collisionDetector = new CollisionDetector(this);
            
            updatables = new List<IUpdatable>();
            drawables = new List<IDrawable>();
            staticCollidables = new List<ICollidable>();
            dynamicCollidables = new List<ICollidable>();
            loadedRooms = new List<IRoom>();
        }
        public void LoadContent()
        {
            mLink = new MasterLink(this);
            dynamicCollidables.Add((ICollidable)mLink);
        }

        public void Update(GameTime gameTime) 
        {
            // TODO: Should link be apart of IUpdatable and IDrawable list? probably, but issues with how room switching works
            mLink.Update(gameTime); // Updates Link

            foreach (IUpdatable obj in updatables.ToList())
            {
                obj.Update(gameTime); 
            }
            // Deals with all object collisions
            collisionDetector.DoAllComparisons();         
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IRoom room in loadedRooms)
            {
                room.Draw(spriteBatch);
            }
         
            foreach (IDrawable obj in drawables.ToList())
            {
                obj.Draw(spriteBatch);
            }
            mLink.Draw(spriteBatch); // Draws Link
        }

        public void Add(GameObject gameObject)
        {
            if (gameObject is IUpdatable) updatables.Add((IUpdatable)gameObject); // Adds to updatable list
            if (gameObject is IDrawable) drawables.Add((IDrawable) gameObject);  // Adds to drawable list

            if (gameObject is ICollidable)
            {
                String collidableType = gameObject.GetCollidableType();
                // TODO: this string comparison is definitly not efficient, what to do?
                if (collidableType.Contains("Block") || collidableType == "CollectableItem" || gameObject is IDoor) staticCollidables.Add((ICollidable)gameObject); // Adds to static list       
                else dynamicCollidables.Add((ICollidable)gameObject); // Adds to dynamic list 
            }
           
        }
        public void Remove(GameObject gameObject)
        {
            if (gameObject is IUpdatable) updatables.Remove((IUpdatable)gameObject);
            if (gameObject is IDrawable) drawables.Remove((IDrawable)gameObject);

            if (gameObject is ICollidable)
            {
                if (staticCollidables.Contains((ICollidable)gameObject)) staticCollidables.Remove((ICollidable)gameObject); // Removes from static list
                else if (dynamicCollidables.Contains((ICollidable)gameObject)) dynamicCollidables.Remove((ICollidable)gameObject); // Removes from dynamic list                          
            }         
        }

       

        public void Reset()
        {
            updatables.Clear();
            drawables.Clear();
            staticCollidables.Clear();
            dynamicCollidables.Clear();
            loadedRooms.Clear();
            LoadContent();          
        }
    }
}
