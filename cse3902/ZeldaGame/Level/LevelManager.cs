using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Resolvers;
using Microsoft.Xna.Framework;
using ZeldaGame.Objects;

namespace ZeldaGame
{
    public class LevelManager
    {
        private static LevelManager instance = new LevelManager();

        public static LevelManager Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        public LevelLoader lvlLoader;
        public LevelReader lvlParser;
        public Camera camera;
        public NextRoomFinder nextRoomFinder;

        public int currentRoom;
        public int[,] mapArray;
        public Dictionary<int, bool> nodesVisited;

        public List<Vector2> gameObjectLocations;
        public List<string> gameObjectNames;

        public bool isMoving;
        public bool viewInv;
        public string cameraDirection;

        public LevelManager()
        {
            lvlLoader = new LevelLoader(this);
            currentRoom = 1;
            gameObjectLocations = new List<Vector2>();
            gameObjectNames = new List<string>();
            camera = new Camera();
            isMoving = false;
            viewInv = false;

            // We are aware this is garbage(sowwy). We might make it more data driven in the future
            mapArray = new int[6, 6] {
                                     { 18, 15, 14, 0, 0, 0},
                                     { 0, 0, 13, 0, 16, 17 },
                                     { 8, 9, 10, 11, 12, 0 },
                                     { 0, 5, 6, 7, 0, 0 },
                                     { 0, 0, 4, 0, 0, 0 },
                                     { 0, 2, 1, 3, 0, 0 }
                                     };

            // Initializes all the nodes with visited or not-visited
            nodesVisited = new Dictionary<int, bool>();
            for (int i = 1; i <= 18; i++)
            {
                if (i == 1)
                {
                    nodesVisited.Add(i, true);
                }
                else
                {
                    nodesVisited.Add(i, false);
                }
            }

            nextRoomFinder = new NextRoomFinder(mapArray, nodesVisited);
        }

        public void LoadRoom()
        {
            gameObjectLocations.Clear();
            gameObjectNames.Clear();
            lvlParser = new LevelReader(this);
            lvlParser.parseOneRoom(currentRoom);
            lvlLoader.LoadGameObjects();   
        }

        // Only for testing purposes
        public void SwitchRoom(string direction)
        {
            UIManager.Instance.followCamera = true;
            camera.MoveCameraInstant(direction);

            checkIfRoomVisited(direction);
        }


        public void SwitchRoomSmooth(string direction)
        {
            isMoving = true;
            cameraDirection = direction;
            camera.SetCameraDestination(direction);
            checkIfRoomVisited(direction);
            GameManager.Instance.gameState.RoomTransition();
        }

        public void moveToAndFromBasement(string direction)
        {
            camera.MoveCameraInstant(direction);
   
            if (direction.Equals("left"))
            {
                GameObjectManager.Instance.mLink.Location -= new Vector2(1000,190);
            }
            else if (direction.Equals("right"))
            {
                GameObjectManager.Instance.mLink.Location += new Vector2(910,300);
            }
            checkIfRoomVisited(direction);
        }

        public void toAndFromInventory(string direction)
        {
            isMoving = true;
            viewInv = true;
            cameraDirection = direction;
            camera.SetCameraDestination(direction);
            GameManager.Instance.gameState.Pause();
        }

        public void checkIfRoomVisited(string direction)
        {
            bool hasBeenVisited = nextRoomFinder.traverseRoom(direction);
            if (!hasBeenVisited)
            {
                LoadRoom();
            }
        }

        public void Reset()
        {           
            Instance = new LevelManager();
        }

        public void Update()
        {
            if (isMoving)
            {
                // This makes the UI follow the camera only when transitioning rooms
                if (GameManager.Instance.gameState is PauseState)
                {
                     UIManager.Instance.followCamera = false;
                } else
                {
                    UIManager.Instance.followCamera = true;
                }
                camera.DisplaceCamera(cameraDirection);
                
                // Locks link when transitioning rooms
                GameObjectManager.Instance.mLink.linkLock = true;

                if (camera.stopMoving())
                {
                    GameObjectManager.Instance.mLink.linkLock = false;

                    isMoving = false;
                    if (viewInv)
                    {
                        viewInv = false;
                    }
                    if (GameManager.Instance.gameState is PauseState)
                    {
                        if (cameraDirection.Equals("up"))
                        {
                            GameManager.Instance.gameState.ItemSelection();
                        }
                        else if (cameraDirection.Equals("down"))
                        {
                            GameManager.Instance.gameState.Play();
                        }                        
                    }
                    else if (GameManager.Instance.gameState is RoomTransitionState)
                    {
                        GameManager.Instance.gameState.Play();
                    }
                }

            } 
           
                
        }
    }
}
