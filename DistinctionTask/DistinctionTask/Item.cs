using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// item parent class
    /// </summary>
    public abstract class Item
    {
        protected Game _gamePanel;
        protected Sprite _sprite;
        private bool _isExist;

        public Item(Game game, Point2D coordinates, string spriteImage)
        {
            _gamePanel = game;

            _sprite = SplashKit.CreateSprite(spriteImage);
            _sprite.Scale = 0.5f;

            Random offsetX = new Random();
            Random offsetY = new Random();
            int offX = offsetX.Next(-20, 20);
            int offY = offsetY.Next(-20, 20);

            _sprite.Position = coordinates;
            _sprite.X += offX;
            _sprite.Y += offY;

            _isExist = true;

        }

        /// <summary>
        /// returns the sprite of the item
        /// </summary>
        /// <value></value>
        public Sprite Sprite
        {
            get
            {
                return _sprite;
            }
        }

        /// <summary>
        /// returns true if the item exists
        /// </summary>
        /// <value></value>
        public bool IsExist
        {
            get
            {
                return _isExist;
            }
        }

        /// <summary>
        /// all child classes are needed to implement different pick up effects
        /// </summary>
        public abstract void PickUp();

        /// <summary>
        /// call this when the item served its purpose
        /// </summary>
        public void Destroy()
        {
            _isExist = false;
        }

        /// <summary>
        /// checks if the player is close enough to pickup
        /// </summary>
        public void Check()
        {
            if (SplashKit.SpriteCollision(_gamePanel.Player.Sprite, _sprite))
            {
                PickUp();
                Destroy();
            }
        }

        /// <summary>
        /// draws the item
        /// </summary>
        public void Draw()
        {
            _sprite.Draw();
        }

    }
}