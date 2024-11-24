using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// spawn child class of structure parent class
    /// </summary>
    public class Spawn : Structure
    {
        public Spawn(Game game, Point2D coordinates, string spriteImage) :
            base(game, coordinates, spriteImage)
        {
            _sprite.Scale = 0.125f;
            _noEnemyRange.Radius = 500;
        }

        /// <summary>
        /// interaction with spawn
        /// </summary>
        public override void Interact()
        {
            //spawn do nothing bro
        }
    }
}