using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// Health subclass of Item parent class
    /// </summary>
    public class Health : Item
    {
        private int _healAmount;

        public Health(Game game, Point2D coordinates) : base(game, coordinates, "health")
        {
            _healAmount = 1;
        }

        /// <summary>
        /// adds health to player when this happens
        /// </summary>
        public override void PickUp()
        {
            _gamePanel.Player.Health += _healAmount;
            if (_gamePanel.Player.Health > 10)
            {
                _gamePanel.Player.Health = 10;
            }
        }

    }
}