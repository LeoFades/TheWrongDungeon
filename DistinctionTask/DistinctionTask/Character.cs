using System;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// Character class, parent of all characters
    /// </summary>
    public abstract class Character
    {
        protected Game _gamePanel;
        private int _health;
        protected Sprite _sprite;
        protected Circle _hitbox;

        public Character(Game game, int health, Point2D coordinates, string spriteImage)
        {
            _health = health;

            _sprite = SplashKit.CreateSprite(spriteImage);
            _sprite.Scale = 0.5f;
            _sprite.Position = coordinates;

            _hitbox = SplashKit.SpriteCollisionCircle(_sprite);

            _gamePanel = game;

            // Test center
            // Point2D center = SplashKit.BitmapCenter(_spriteBitmap);
            // Console.WriteLine("Player center : " + center.X + " " + center.Y);

        }

        /// <summary>
        /// property of health of character
        /// </summary>
        /// <value>health in int</value>
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }

        /// <summary>
        /// returns hitbox of character
        /// </summary>
        /// <value>circle range</value>
        public Circle Hitbox
        {
            get
            {
                return _hitbox;
            }
        }

        /// <summary>
        /// returns the sprite 
        /// </summary>
        /// <value>sprite</value>
        public Sprite Sprite
        {
            get
            {
                return _sprite;
            }
        }

        /// <summary>
        /// decreses health of character
        /// </summary>
        /// <param name="hp">health to decrease in int</param>
        public void DecreaseHealth(int hp)
        {
            _health -= hp;
        }

        /// <summary>
        /// knockback the character by a certain amount
        /// </summary>
        /// <param name="delta">direction that is less than 1</param>
        public void Knockback(Point2D delta)
        {
            _sprite.X += (float)delta.X * 10;
            _sprite.Y += (float)delta.Y * 10;

        }

        /// <summary>
        /// all child must implement attack method. it is different for each type of character
        /// </summary>
        public abstract void Attack();

        /// <summary>
        /// checks if the points passed in is a wall
        /// </summary>
        /// <param name="nextStepX">x coordinates</param>
        /// <param name="nextStepY">y coordinates</param>
        /// <returns></returns>
        public bool IsIntoWall(float nextStepX, float nextStepY)
        {

            int tileCol = (int)((nextStepX + 32) / 16);
            int tileRow = (int)((nextStepY + 32) / 16);

            if (tileCol > 200 || tileRow > 200)
            {
                return true;
            }

            int tileNum = _gamePanel.TileManager.MapTile[tileRow, tileCol];

            if (tileNum == 0)
            {
                return true;
            }

            //Console.WriteLine(tileCol + " " + tileRow);

            return false;
        }

        /// <summary>
        /// all child classes checks for necessary stuff
        /// </summary>
        public abstract void Check();

        /// <summary>
        /// all child classes checks for update
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// all child classes implement what to draw
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// child should be able to die.
        /// </summary>
        public abstract void Destroy();

    }
}