using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Serialization;
using ZeldaGame.Items;



// Nico Poggio
namespace ZeldaGame
{

    // Keyboard Class
    public class KeyController : IController
    {
        private GameObjectManager objectManager;
        private KeyboardState oldState;
        private Keys[] oldKeys;
        private Dictionary<Keys, ICommand> pressKeys;
        private Dictionary<Keys, ICommand> clickKeys;
        private Dictionary<Keys, ICommand> inventoryClickKeys;
        private Dictionary<Keys, ICommand> pauseClickKeys;
        private Dictionary<Keys, ICommand> releaseKeys;

        public KeyController()
        {
            this.objectManager = GameObjectManager.Instance;

            pressKeys = new Dictionary<Keys, ICommand>();
            clickKeys = new Dictionary<Keys, ICommand>();
            inventoryClickKeys = new Dictionary<Keys, ICommand>();
            pauseClickKeys = new Dictionary<Keys, ICommand>();
            releaseKeys = new Dictionary<Keys, ICommand>();

            // Link Movements
            pressKeys.Add(Keys.W, new ForwardMove(objectManager));
            pressKeys.Add(Keys.S, new BackwardMove(objectManager));
            pressKeys.Add(Keys.A, new LeftMove(objectManager));
            pressKeys.Add(Keys.D, new RightMove(objectManager));

            releaseKeys.Add(Keys.W, new LinkIdle(objectManager));
            releaseKeys.Add(Keys.S, new LinkIdle(objectManager));
            releaseKeys.Add(Keys.A, new LinkIdle(objectManager));
            releaseKeys.Add(Keys.D, new LinkIdle(objectManager));

            // Link Attacks
            clickKeys.Add(Keys.Enter, new LinkAttack(objectManager));
            clickKeys.Add(Keys.Z, new LinkAttack(objectManager));

            // Link Items
            clickKeys.Add(Keys.D1, new UseItemCommand(objectManager));

            // Link taking damage
            clickKeys.Add(Keys.E, new LinkDamage(objectManager));

            // You can pause/unpause from Play and Pause state
            clickKeys.Add(Keys.P, new PauseCommand());
            pauseClickKeys.Add(Keys.P, new PauseCommand());

            // You can open and close inventory base on which game state you are in
            clickKeys.Add(Keys.I, new OpenInventory());
            inventoryClickKeys.Add(Keys.I, new CloseInventory());

            // Item selector movement
            inventoryClickKeys.Add(Keys.Up, new MoveSelectorUp());
            inventoryClickKeys.Add(Keys.Down, new MoveSelectorDown());
            inventoryClickKeys.Add(Keys.Left, new MoveSelectorLeft());
            inventoryClickKeys.Add(Keys.Right, new MoveSelectorRight());

            // Select item
            inventoryClickKeys.Add(Keys.Enter, new SelectItem());

            // Quit game
            clickKeys.Add(Keys.Q, new QuitGame());

     
        }
        // Keyboard update
        public void Update(GameTime gameTime)
        {

            KeyboardState newState = Keyboard.GetState();
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                // Execute for any press Keys
                if (pressKeys.ContainsKey(key))
                {
                    if (GameManager.Instance.gameState is PlayState)
                    {
                        pressKeys[key].Execute();
                    }
                }

                IGameState gameState = GameManager.Instance.gameState;
                // Execute for any click keys in PlayState
                if (clickKeys.ContainsKey(key) && !oldKeys.Contains(key) && gameState is PlayState)
                {           
                    clickKeys[key].Execute();
                }
                // Execute for any click keys in ItemSelectionState
                if (inventoryClickKeys.ContainsKey(key) && !oldKeys.Contains(key) && gameState is ItemSelectionState)
                {
                    inventoryClickKeys[key].Execute();
                }
                // Execute for any click keys in PauseState
                if (pauseClickKeys.ContainsKey(key) && !oldKeys.Contains(key) && gameState is PauseState)
                {
                    pauseClickKeys[key].Execute();
                }

            }
            // Do task once key is no longer held
            foreach (Keys key in releaseKeys.Keys)
            {
                if (newState.IsKeyUp(key) && oldState.IsKeyDown(key))
                {
                    if (GameManager.Instance.gameState is PlayState)
                    {
                        releaseKeys[key].Execute();
                    }
                }
            }

            oldState = newState; // set the new state as the old state for next time      
            oldKeys = pressedKeys;
        }

    }  
}
