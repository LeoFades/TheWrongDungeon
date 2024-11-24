using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace DistinctionTask
{
    /// <summary>
    /// tile class 
    /// </summary>
    public class Tile
    {
        private Sprite _sprite;
        private bool _collision;
        private Circle _range;

        public Tile(string tileImage)
        {
            _sprite = SplashKit.CreateSprite(tileImage);
            _collision = false;

            _range = new Circle();
            _range.Center = _sprite.CenterPoint;
            _range.Radius = 16;
        }

        /// <summary>
        /// property of coordinate of tile
        /// </summary>
        /// <value>point2d</value>
        public Point2D Coordinates
        {
            get
            {
                return _sprite.Position;
            }
            set
            {
                _sprite.Position = value;
            }
        }

        /// <summary>
        /// property of tile if collision is enabled
        /// </summary>
        /// <value>bool</value>
        public bool Collision
        {
            set
            {
                _collision = value;
            }
            get
            {
                return _collision;
            }
        }

        /// <summary>
        /// returns the sprite of the tile
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
        /// draw the tile
        /// </summary>
        public void Draw()
        {
            _sprite.Draw();
        }
    }
}