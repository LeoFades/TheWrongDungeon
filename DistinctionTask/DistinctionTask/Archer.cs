using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// Archer subclass with Enemy as Parent class
    /// </summary>
    public class Archer : Enemy
    {

        private int _atkDelay;
        private int _damage;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="game">gamePanel</param>
        /// <param name="sightRadius">Range of sight</param>
        /// <param name="movementSpeed">speed in pixel </param>
        /// <param name="health">health</param>
        /// <param name="coordinates">spawn point</param>
        /// <param name="spriteImage">image path</param>
        /// <returns></returns>
        public Archer(Game game, double sightRadius, double movementSpeed, int health, Point2D coordinates, string spriteImage) :
            base(game, sightRadius, movementSpeed, health, coordinates, spriteImage)
        {
            _atkDelay = 2;
            _damage = 2;
        }

        /// <summary>
        /// override method for move as archer moves differently
        /// </summary>
        public override void Move()
        {
            base.SeePlayer(_gamePanel.Player.Coordinates);
            TimeSpan elapsedTime = DateTime.Now - _startTime;
            if (elapsedTime.TotalSeconds >= _atkDelay && _attacking)
            {
                Random dirY = new Random();
                Random dirX = new Random();
                Random type = new Random();

                double dx = dirX.NextDouble();
                double dy = dirY.NextDouble();
                int mathType = type.Next(1, 3);

                _sprite.Dx = 0;
                _sprite.Dy = 0;

                switch (mathType)
                {
                    case 1:
                        _sprite.Dx += (float)dx;
                        _sprite.Dy += (float)dy;
                        break;
                    case 2:
                        _sprite.Dx -= (float)dx;
                        _sprite.Dy -= (float)dy;
                        break;
                }
            }

            if (_playerInSight && !IsIntoWall(_sprite.X + (float)(_sprite.Dx * _movementSpeed), _sprite.Y + (float)(_sprite.Dy * _movementSpeed)))
            {

                //now we move by adding the unitvector to coordinates
                _sprite.X += (float)(_sprite.Dx * _movementSpeed);
                _sprite.Y += (float)(_sprite.Dy * _movementSpeed);

                _hitbox = SplashKit.SpriteCollisionCircle(_sprite);

            }
            else if (IsIntoWall(_sprite.X, _sprite.Y)) //checks if the dude is currently in a wall
            {

                if (!IsIntoWall((float)_sprite.X, (float)_sprite.Y - 75))
                {
                    _sprite.X = (float)_sprite.X;
                    _sprite.Y = (float)_sprite.Y - 75;
                }
                else if (!IsIntoWall((float)_sprite.X + 75, (float)_sprite.Y))
                {
                    _sprite.X = (float)_sprite.X + 75;
                    _sprite.Y = (float)_sprite.Y;
                }
                else if (!IsIntoWall((float)_sprite.X, (float)_sprite.Y + 75))
                {
                    _sprite.X = (float)_sprite.X;
                    _sprite.Y = (float)_sprite.Y + 75;
                }
                else if (!IsIntoWall((float)_sprite.X - 75, (float)_sprite.Y))
                {
                    _sprite.X = (float)_sprite.X - 75;
                    _sprite.Y = (float)_sprite.Y;
                }
            }


            //updates sightline here
            _sightRange.Center.X = _sprite.X + 32;
            _sightRange.Center.Y = _sprite.Y + 32;

        }

        /// <summary>
        /// Update the object every cycle 
        /// </summary>
        public override void Update()
        {
            base.Update();

            Move();

            TimeSpan elapsedTime = DateTime.Now - _startTime;
            if (elapsedTime.TotalSeconds >= _atkDelay && _attacking)
            {
                _attacking = false;
                Projectile arrow = new Projectile(_gamePanel, _sprite.Position, 15, false, ProjectileBehaviour.ToPlayer, "arrow", _damage);
                _gamePanel.AllProjectiles.AddProjectile(arrow);
            }
        }


    }
}