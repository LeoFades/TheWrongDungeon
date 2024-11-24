using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// coin subclass of Item parent class
    /// </summary>
    public class Coin : Item
    {
        public Coin(Game game, Point2D coordinates) : base(game, coordinates, "coin")
        {
            Random rotation = new Random();
            float degree = rotation.Next(0, 360);
            float angle = (float)(degree * 3.142 / 180);
            _sprite.Rotation = angle;
        }

        /// <summary>
        /// adds the coin into the player object
        /// </summary>
        public override void PickUp()
        {
            _gamePanel.Player.AddCoin(this);
        }
    }
}