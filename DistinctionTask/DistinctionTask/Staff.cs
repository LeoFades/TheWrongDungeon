using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// staff child class of weapon parent class
    /// </summary>
    public class Staff : Weapon
    {
        private int _manaRequired;

        public Staff(Game game, int manaRequired, int damage, float atkDelay, Point2D coordinates, string spriteImage, string spriteAttackingImage) :
            base(game, damage, atkDelay, coordinates, spriteImage, spriteAttackingImage)
        {
            _manaRequired = manaRequired;

        }

        /// <summary>
        /// when the staff attacks
        /// </summary>
        /// <param name="enemies"></param>
        public override void Attack(List<Enemy> enemies)
        {
            if (_equipped && !_attacking)
            {
                _startTime = DateTime.Now;
                _attacking = true;
                //Console.WriteLine("Casting");

                //staff attack here
                // basically summoning a few projectiles in random direction, mostly towards the cursor, hopefully

                Random noOfProjectiles = new Random();
                for (int i = 0; i < noOfProjectiles.Next(3, 7); ++i)
                {
                    Projectile magicProjectile = new Projectile(_gamePanel, _sprite.Position, 5, true, ProjectileBehaviour.Spread, "magicProjectile", _damage);
                    _gamePanel.AllProjectiles.AddProjectile(magicProjectile);
                }


            }
        }

        /// <summary>
        /// override method of update of object
        /// </summary>
        public override void Update()
        {
            base.Update();

            TimeSpan elapsedTime = DateTime.Now - _startTime;
            if (elapsedTime.TotalSeconds >= _atkDelay && _attacking)
            {
                _attacking = false;
                //Console.WriteLine("Cooldown Complete");
            }
        }

    }
}