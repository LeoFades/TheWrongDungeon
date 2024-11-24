using System;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// sword child class of weapon parent class
    /// </summary>
    public class Sword : Weapon
    {
        public Sword(Game game, int damage, float atkDelay, Point2D coordinates, string spriteImage, string spriteAttackingImage)
            : base(game, damage, atkDelay, coordinates, spriteImage, spriteAttackingImage)
        {

        }

        /// <summary>
        /// override method for weapon attack
        /// </summary>
        /// <param name="enemies"></param>
        public override void Attack(List<Enemy> enemies)
        {

            if (_equipped && !_attacking)
            {
                _startTime = DateTime.Now;
                _attacking = true;
                //Console.WriteLine("Swung");

                foreach (Enemy e in enemies)
                {
                    if (SplashKit.SpriteCollision(_spriteAttacking, e.Sprite))
                    {
                        //Console.WriteLine("Target hit");
                        e.DecreaseHealth(_damage);
                    }
                }

            }


        }

        /// <summary>
        /// override method to update the object every cycle
        /// </summary>
        public override void Update()
        {
            base.Update();

            TimeSpan elapsedTime = DateTime.Now - _startTime;
            if (elapsedTime.TotalSeconds >= _atkDelay && _attacking)
            {
                _attacking = false;
                //Console.WriteLine("Cooldown complete");
            }
        }

    }
}