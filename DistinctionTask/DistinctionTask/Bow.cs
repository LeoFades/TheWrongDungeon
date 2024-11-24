using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace DistinctionTask
{
    /// <summary>
    /// Bow subclass of the Weapon Parent class
    /// </summary>
    public class Bow : Weapon
    {
        private int _ammo;

        public Bow(Game game, int ammo, int damage, float atkDelay, Point2D coordinates, string spriteImage, string spriteAttackingImage) :
            base(game, damage, atkDelay, coordinates, spriteImage, spriteAttackingImage)
        {
            _ammo = ammo;

        }

        /// <summary>
        /// When attacking, call this
        /// </summary>
        /// <param name="enemies"></param>
        public override void Attack(List<Enemy> enemies)
        {
            if (_equipped && !_attacking)
            {
                _startTime = DateTime.Now;
                _attacking = true;


            }

        }

        /// <summary>
        /// update the object every cycle
        /// </summary>
        public override void Update()
        {
            base.Update();

            TimeSpan elapsedTime = DateTime.Now - _startTime;
            if (elapsedTime.TotalSeconds >= _atkDelay && _attacking)
            {
                _attacking = false;

                Projectile arrow = new Projectile(_gamePanel, _spriteAttacking.Position, 10, true, ProjectileBehaviour.ToCursor, "arrow", _damage);
                _gamePanel.AllProjectiles.AddProjectile(arrow);
            }
        }

    }
}