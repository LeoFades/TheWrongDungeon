using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace DistinctionTask
{
    /// <summary>
    /// karl the boss subclass of enemy parent class
    /// </summary>
    public class Karl : Enemy
    {
        private int _atkDelay;
        public Karl(Game game, double sightRadius, double movementSpeed, int health, Point2D coordinates, string spriteImage) :
            base(game, sightRadius, movementSpeed, health, coordinates, spriteImage)
        {
            _atkDelay = 2;

        }

        /// <summary>
        /// override method for the boss movement
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

            //updates sightline here
            _sightRange.Center.X = _sprite.X + 32;
            _sightRange.Center.Y = _sprite.Y + 32;
        }

        /// <summary>
        /// override method for boss attack
        /// </summary>
        public override void Attack()
        {
            base.Attack();

            Random type = new Random();
            int randomType = type.Next(0, 3);

            switch (randomType)
            {
                case 0:
                    //tree
                    for (int i = 0; i < 9; i++)
                    {
                        Projectile tree = new Projectile(_gamePanel, _sprite.CenterPoint, 6, false, ProjectileBehaviour.Tree, "magicProjectile2", 2);
                        tree.AtkAngle2 = i;
                        _gamePanel.AllProjectiles.AddProjectile(tree);
                    }
                    //Console.WriteLine("TREE");
                    break;
                case 1:
                    //moving eight directions
                    Projectile octaMove = new Projectile(_gamePanel, _sprite.CenterPoint, 5, false, ProjectileBehaviour.OctaMove, "magicProjectile2", 2);
                    _gamePanel.AllProjectiles.AddProjectile(octaMove);
                    //Console.WriteLine("OCTA");
                    break;
                case 2:
                    //tp behind
                    Random direction = new Random();
                    int dir = direction.Next(0, 4);

                    if (dir == 0 && !IsIntoWall((float)_gamePanel.Player.Coordinates.X, (float)_gamePanel.Player.Coordinates.Y - 75))
                    {
                        _sprite.X = (float)_gamePanel.Player.Coordinates.X;
                        _sprite.Y = (float)_gamePanel.Player.Coordinates.Y - 75;
                    }
                    else if (dir == 1 && !IsIntoWall((float)_gamePanel.Player.Coordinates.X + 75, (float)_gamePanel.Player.Coordinates.Y))
                    {
                        _sprite.X = (float)_gamePanel.Player.Coordinates.X + 75;
                        _sprite.Y = (float)_gamePanel.Player.Coordinates.Y;
                    }
                    else if (dir == 2 && !IsIntoWall((float)_gamePanel.Player.Coordinates.X, (float)_gamePanel.Player.Coordinates.Y + 75))
                    {
                        _sprite.X = (float)_gamePanel.Player.Coordinates.X;
                        _sprite.Y = (float)_gamePanel.Player.Coordinates.Y + 75;
                    }
                    else if (dir == 3 && !IsIntoWall((float)_gamePanel.Player.Coordinates.X - 75, (float)_gamePanel.Player.Coordinates.Y))
                    {
                        _sprite.X = (float)_gamePanel.Player.Coordinates.X - 75;
                        _sprite.Y = (float)_gamePanel.Player.Coordinates.Y;
                    }


                    break;
            }
        }

        /// <summary>
        /// override method for update of object
        /// </summary>
        public override void Update()
        {
            base.Update();
            Move();
            _sprite.Scale = 1f;

            TimeSpan elapsedTime = DateTime.Now - _startTime;
            if (elapsedTime.TotalSeconds >= _atkDelay && _attacking)
            {
                _attacking = false;

            }
        }
    }
}