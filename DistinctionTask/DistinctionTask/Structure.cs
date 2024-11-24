using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// structure class
    /// </summary>
    public abstract class Structure
    {
        protected Game _gamePanel;
        protected Sprite _sprite;
        protected Circle _noEnemyRange;

        public Structure(Game game, Point2D coordinates, string spriteImage)
        {
            _gamePanel = game;
            _sprite = SplashKit.CreateSprite(spriteImage);
            _sprite.X = (float)coordinates.X;
            _sprite.Y = (float)coordinates.Y;


            _noEnemyRange = new Circle();
            _noEnemyRange.Center = _sprite.CenterPoint;

        }

        /// <summary>
        /// property of range where no enemy can spawn
        /// </summary>
        /// <value>circle</value>
        public Circle NoEnemyRange
        {
            get
            {
                return _noEnemyRange;
            }
            set
            {
                _noEnemyRange = value;
            }
        }

        /// <summary>
        /// coordinates of the structure
        /// </summary>
        /// <value>point2d</value>
        public Point2D Coordinates
        {
            get
            {
                return _sprite.CenterPoint;
            }
        }

        /// <summary>
        /// interaction with the structures
        /// </summary>
        public abstract void Interact();

        /// <summary>
        /// draw the structure
        /// </summary>
        public void Draw()
        {
            // _noEnemyRange.Draw(Color.Yellow);
            _sprite.Draw();
        }
    }

}