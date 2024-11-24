using System;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// knight subclass of enemy parent class
    /// </summary>
    public class Knight : Enemy
    {
        private int _damage;
        public Knight(Game game, double sightRadius, double movementSpeed, int health, Point2D coordinates, string spriteImage) :
            base(game, sightRadius, movementSpeed, health, coordinates, spriteImage)
        {
            _damage = 2;
        }

        /// <summary>
        /// override method for knight attacks
        /// </summary>
        public override void Attack()
        {
            if (SplashKit.SpriteCollision(_sprite, _gamePanel.Player.Sprite))
            {
                _gamePanel.Player.DecreaseHealth(_damage);
            }
        }

        /// <summary>
        /// override method for knight updates
        /// </summary>
        public override void Update()
        {
            base.Update();

            Move();
        }


    }
}