using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// shop child class of the structure parent class
    /// </summary>
    public class Shop : Structure
    {
        public Shop(Game game, Point2D coordinates, string spriteImage) :
            base(game, coordinates, spriteImage)
        {
            _sprite.Scale = 0.3f;
            _noEnemyRange.Radius = 50;
        }

        /// <summary>
        /// interaction 
        /// </summary>
        public override void Interact()
        {
            if (_gamePanel.Player.Coin > 20 && SplashKit.SpriteCollision(_sprite, _gamePanel.Player.Sprite))
            {
                Transaction();
            }
        }

        /// <summary>
        /// when u buy stuff
        /// </summary>
        private void Transaction()
        {
            _gamePanel.Player.RemoveCoin(20);

            Random weapon = new Random();
            int weaponType = weapon.Next(0, 3);

            Point2D coordinates;
            coordinates.X = _sprite.CenterPoint.X - 35;
            coordinates.Y = _sprite.CenterPoint.Y + 30;

            switch (weaponType)
            {
                case 0:
                    _gamePanel.AllWeapons.Add(new Sword(_gamePanel, 5, 1, coordinates, "sword", "swordSwing"));
                    break;
                case 1:
                    _gamePanel.AllWeapons.Add(new Bow(_gamePanel, 10, 3, 1, coordinates, "bow", "bowDrawn"));
                    break;
                case 2:
                    _gamePanel.AllWeapons.Add(new Staff(_gamePanel, 1, 1, 2, coordinates, "staff", "staffCast"));
                    break;
            }
        }
    }
}