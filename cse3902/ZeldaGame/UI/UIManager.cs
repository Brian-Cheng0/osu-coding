using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;


namespace ZeldaGame
{
    public class UIManager
    {
        public List<ITranslatable> translatables;
        public List<IUpdatable> updatables;
        public List<IDrawable> drawables;
        public List<GameObject> playerInventory; // All the items that link currently has stored in his inventory

        public int LinkHealth;
        public int LinkMaxHealth;
        public PlayerHealth playerHealth;
       
        public ItemCounterDisplay itemDisplay;
        public ItemSelectionDisplay selectableItemDisplay;
        public MapDisplay mapDisplay;

        public Dictionary<String, int> objToCount;
        public Boolean mapIsActive = false;
        public Boolean compassIsActive = false;

        // To know where the base all the drawings off of
        public Vector2 baseLocation = new Vector2(-LevelManager.Instance.camera.x, -LevelManager.Instance.camera.y);
        public Boolean followCamera = true;

        // Singleton class
        private static UIManager instance = new UIManager();

        public static UIManager Instance
        {
            get { return instance; }
            set { instance = value; }
        }
        // Acts as the Initialize() function in main
        public UIManager()
        {
            translatables = new List<ITranslatable>();
            updatables = new List<IUpdatable>();
            drawables = new List<IDrawable>();

            itemDisplay = new ItemCounterDisplay();
            selectableItemDisplay = new ItemSelectionDisplay();
         //   selectableItems = new List<ISelectable>();
            mapDisplay = new MapDisplay();

            objToCount = new Dictionary<string, int>();

            playerHealth = new PlayerHealth();

            updatables.Add(playerHealth);
            drawables.Add(playerHealth);

            LinkHealth = 6;
            LinkMaxHealth = 6;
          
            objToCount.Add("Map", 0);
            objToCount.Add("Compass", 0);
            objToCount.Add("Bow", 0);
            objToCount.Add("Boomerang", 0);
            objToCount.Add("Rupee", 0);
            objToCount.Add("Key", 0);
            objToCount.Add("Bomb", 0);           
        }
     
        public void LoadContent()
        { 
            Add(new InventoryScreen()); // Creates the base template for the overhead inventory
            Add(new PrimaryItem());           
            Add(selectableItemDisplay);

            itemDisplay.InitializeCounters();
            mapDisplay.LoadInitialMaps();
            Add(new CompassDisplay()); // Adding compass after map so it draws on top
            playerHealth.InitializeHealthBarList();                               
        }

        public void Update(GameTime gameTime)
        {
            if (followCamera) UpdateBaseLocation();
 
            playerHealth.Update(gameTime);

            foreach (IUpdatable icon in updatables.ToList())
            {
                icon.Update(gameTime);
            }
             
            if (objToCount["Map"] > 0 && !mapIsActive) 
            {
                mapIsActive = true;
            }
            if (objToCount["Compass"] > 0 && !compassIsActive)
            {
                compassIsActive = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IDrawable icon in drawables.ToList())
            {
                icon.Draw(spriteBatch);
            }
            playerHealth.Draw(spriteBatch);
        }

        //Health
        public void SetHealth(int Health)
        {
            LinkHealth = Health;
        }

        public void AddHeartContainer()
        {
            if (LinkMaxHealth < 15)
            {
                LinkHealth += 2;
                LinkMaxHealth += 2;
            }
        }

        public void AddHealth()
        {
            if (LinkHealth < LinkMaxHealth-1)
            {
                LinkHealth += 2;
                
            }else if(LinkHealth<LinkMaxHealth)
            {
                LinkHealth++;
            }
        }

        public void Add(object obj)
        {
            if (obj is ITranslatable) translatables.Add((ITranslatable)obj);
            if (obj is IUpdatable) updatables.Add((IUpdatable)obj);          
            if (obj is IDrawable) drawables.Add((IDrawable)obj);    
        }
        public void Remove(object obj)
        {
            if (obj is ITranslatable) translatables.Remove((ITranslatable)obj);
            if (obj is IUpdatable) updatables.Remove((IUpdatable)obj);
            if (obj is IUpdatable) drawables.Remove((IDrawable)obj);
        }  

        public void IncrementItemCount(String itemName)
        {
            if (objToCount.ContainsKey(itemName))
            {
                if (objToCount[itemName] < 99) objToCount[itemName]++;
            }
        }
        public void DecrementItemCount(String itemName)
        {
            if (objToCount.ContainsKey(itemName))
            {
                if (objToCount[itemName] > 0) objToCount[itemName]--;
            }
        }

        public void UpdateBaseLocation()
        {
             baseLocation = new Vector2(-LevelManager.Instance.camera.x, -LevelManager.Instance.camera.y);
        }

        public void Reset()
        {
            Instance = new UIManager();
        }
    }
}
