using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class SnakeEnemyRightState : IEnemyState
    {
        private MasterSnake snake;
        public IEnemyState oppositeState { get; set; }
        public SnakeEnemyRightState(MasterSnake snake)
        {
            this.snake = snake;
            snake.sprite = SpriteFactory.Instance.getSprite(Sprite.SnakeRight);
        }
        public void WalkLeft()
        {
            snake.state = new SnakeEnemyLeftState(snake);
        }
        public void WalkRight()
        {
            // already in right state 
        }
        public void WalkUp()
        {
            //does not go up
        }
        public void WalkDown()
        {
            //does not go down
        }
        public void WalkNE()
        {
            // does not go NE
        }
        public void WalkNW()
        {
            // does not go NW
        }
        public void WalkSE()
        {
            // does not go SE
        }
        public void WalkSW()
        {
            // does not go SW
        }
        public void AttackState()
        {
            //does not have an attack state
        }

        public void DamageState()
        {
            //does not have a damage state
        }
        public void Update(int speed)
        {
            snake.currentLocation.X += speed;
        }
    }
}
