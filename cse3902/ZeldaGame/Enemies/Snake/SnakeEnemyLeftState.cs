using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class SnakeEnemyLeftState : IEnemyState
    {
        private MasterSnake snake;
        public IEnemyState oppositeState { get; set; }

        public SnakeEnemyLeftState(MasterSnake snake)
        {
            this.snake = snake;
            snake.sprite = SpriteFactory.Instance.getSprite(Sprite.SnakeLeft);
        }
        public void WalkLeft()
        {
            // already in left state
        }

        public void WalkRight()
        {
            snake.state = new SnakeEnemyRightState(snake);
        }
        public void WalkUp()
        {
            //does not go up
        }

        public void WalkDown()
        {
            //does not go down
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
            snake.currentLocation.X -= speed;
        }

    }

}
