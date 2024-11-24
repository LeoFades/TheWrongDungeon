using System;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// projectile class 
    /// </summary>
    public class Projectile
    {
        private Game _gamePanel;
        private Point2D _delta;
        private double _atkAngle;
        private double _atkAngle2;
        private float _speed;
        private bool _isExist;
        private bool _initialised;
        private Sprite _sprite;
        private bool _originPlayer;
        private ProjectileBehaviour _behaviour;
        private int _damage;
        private DateTime _startTime;

        public Projectile(Game game, Point2D coordinates, float speed, bool isFromPlayer, ProjectileBehaviour behaviour, string spriteImageName, int damage)
        {
            _gamePanel = game;

            _speed = speed;
            _originPlayer = isFromPlayer;

            _sprite = SplashKit.CreateSprite(spriteImageName);
            _sprite.Position = coordinates;
            _sprite.CollisionKind = CollisionTestKind.PixelCollisions;
            _sprite.Scale = 0.3f;

            _atkAngle = 0;
            _atkAngle2 = 0;
            _isExist = true;
            _initialised = false;

            _behaviour = behaviour;
            _damage = damage;

            _startTime = DateTime.Now;

        }

        /// <summary>
        /// returns sprite of projectile
        /// </summary>
        /// <value></value>
        public Sprite Sprite
        {
            get
            {
                return _sprite;
            }
        }

        /// <summary>
        /// checks if the projectile is from player
        /// </summary>
        /// <value>boolean</value>
        public bool isFromPlayer
        {
            get
            {
                return _originPlayer;
            }

        }

        /// <summary>
        /// checks the delta of travel of projectile
        /// </summary>
        /// <value>poin2d</value>
        public Point2D Delta
        {
            get
            {
                return _delta;
            }
            set
            {
                _delta = value;
            }
        }

        /// <summary>
        /// returns damage of projectile
        /// </summary>
        /// <value></value>
        public int Damage
        {
            get
            {
                return _damage;
            }
        }

        /// <summary>
        /// checks the attack angle of the projectile, rotation angle of the sprite
        /// </summary>
        /// <value>double</value>
        public double AtkAngle
        {
            get
            {
                return _atkAngle;
            }
            set
            {
                _atkAngle = value;
            }
        }

        /// <summary>
        /// a predetermined attack angle , usually is changed when instantiating a projectile
        /// </summary>
        /// <value>double</value>
        public double AtkAngle2
        {
            get
            {
                return _atkAngle2;
            }
            set
            {
                _atkAngle2 = value;
            }
        }

        /// <summary>
        /// draws the projectile
        /// </summary>
        public void Draw()
        {
            if (_isExist)
            {
                _sprite.Draw();
            }

        }

        /// <summary>
        /// next movement of projectile
        /// </summary>
        /// <returns></returns>
        public Point2D NextMove()
        {
            Point2D nextMove;
            nextMove.X = _sprite.X + ((float)_delta.X * _speed);
            nextMove.Y = _sprite.Y + ((float)_delta.Y * _speed);
            return nextMove;
        }

        /// <summary>
        /// initiate the projectile behaviour 
        /// </summary>
        private void InitialCalculation()
        {
            if (_behaviour == ProjectileBehaviour.ToCursor)
            {
                Point2D mouseLocation = SplashKit.ToWorld(SplashKit.MousePosition());

                // use vector to find the angle between player and cursor
                Point2D vectorMouseToPlayer;
                vectorMouseToPlayer.X = mouseLocation.X - (_sprite.X + 32); //adds 32 to get to the center of bitmap 
                vectorMouseToPlayer.Y = mouseLocation.Y - (_sprite.Y + 32);

                // math to make arrow fly at cursor
                _atkAngle = (Math.Atan2(vectorMouseToPlayer.Y, vectorMouseToPlayer.X) * 180) / 3.142;

                // so we use unit vector here to count how much it should move in one pixel to go towards player
                //angle of coordinate change basically
                double magnitude = Math.Sqrt(Math.Pow(vectorMouseToPlayer.X, 2) + Math.Pow(vectorMouseToPlayer.Y, 2));

                _delta.X = vectorMouseToPlayer.X / magnitude;
                _delta.Y = vectorMouseToPlayer.Y / magnitude;

                _sprite.Rotation = (float)_atkAngle;
                _sprite.Scale = 1f;

                _initialised = true;
            }
            else if (_behaviour == ProjectileBehaviour.Spread)
            {
                Point2D mouseLocation = SplashKit.ToWorld(SplashKit.MousePosition());

                //to simulate a spreading shot
                Random offsetSpread = new Random();

                // use vector to find the angle between player and cursor
                Point2D vectorMouseToPlayer;
                vectorMouseToPlayer.X = (mouseLocation.X + offsetSpread.Next(-100, 100)) - (_sprite.X + 32); //adds 32 to get to the center of bitmap 
                vectorMouseToPlayer.Y = (mouseLocation.Y + offsetSpread.Next(-100, 100)) - (_sprite.Y + 32);

                // so we use unit vector here to count how much it should move in one pixel to go towards player
                //angle of coordinate change basically
                double magnitude = Math.Sqrt(Math.Pow(vectorMouseToPlayer.X, 2) + Math.Pow(vectorMouseToPlayer.Y, 2));

                _delta.X = vectorMouseToPlayer.X / magnitude;
                _delta.Y = vectorMouseToPlayer.Y / magnitude;

                _speed += offsetSpread.Next(-4, 5);

                _sprite.Rotation = (float)_atkAngle;


                _initialised = true;
            }
            else if (_behaviour == ProjectileBehaviour.ToPlayer)
            {
                // use vector to find the angle between player and enemy
                Point2D vectorEnemyToPlayer;
                vectorEnemyToPlayer.X = _gamePanel.Player.Coordinates.X + 32 - (_sprite.X + 32); //adds 32 to get to the center of bitmap 
                vectorEnemyToPlayer.Y = _gamePanel.Player.Coordinates.Y + 32 - (_sprite.Y + 32);

                // math to make arrow fly at player
                _atkAngle = (Math.Atan2(vectorEnemyToPlayer.Y, vectorEnemyToPlayer.X) * 180) / 3.142;

                // so we use unit vector here to count how much it should move in one pixel to go towards player
                //angle of coordinate change basically
                double magnitude = Math.Sqrt(Math.Pow(vectorEnemyToPlayer.X, 2) + Math.Pow(vectorEnemyToPlayer.Y, 2));

                _delta.X = vectorEnemyToPlayer.X / magnitude;
                _delta.Y = vectorEnemyToPlayer.Y / magnitude;

                _sprite.Rotation = (float)_atkAngle;
                _sprite.Scale = 1f;

                _initialised = true;
            }
            else if (_behaviour == ProjectileBehaviour.EightDirection)
            {
                switch ((int)AtkAngle)
                {
                    case 0:
                        _delta.X = 0;
                        _delta.Y = 1;
                        break;
                    case 1:
                        _delta.X = 0.5;
                        _delta.Y = 0.5;
                        break;
                    case 2:
                        _delta.X = 1;
                        _delta.Y = 0;
                        break;
                    case 3:
                        _delta.X = 0.5;
                        _delta.Y = -0.5;
                        break;
                    case 4:
                        _delta.X = 0;
                        _delta.Y = -1;
                        break;
                    case 5:
                        _delta.X = -0.5;
                        _delta.Y = -0.5;
                        break;
                    case 6:
                        _delta.X = -1;
                        _delta.Y = 0;
                        break;
                    case 7:
                        _delta.X = -0.5;
                        _delta.Y = 0.5;
                        break;
                }

                _sprite.Rotation = (float)_atkAngle;
                _sprite.Scale = 0.5f;

                _initialised = true;
            }
            else if (_behaviour == ProjectileBehaviour.Tree)
            {
                //basically the same as to player
                // use vector to find the angle between player and enemy
                Point2D vectorEnemyToPlayer;
                int offset = 0;
                switch ((int)AtkAngle2)
                {
                    case 0:
                        offset = 0;
                        break;
                    case 1:
                        offset = 30;
                        break;
                    case 2:
                        offset = 60;
                        break;
                    case 3:
                        offset = 90;
                        break;
                    case 4:
                        offset = 120;
                        break;
                    case 5:
                        offset = 150;
                        break;
                    case 6:
                        offset = 180;
                        break;
                    case 7:
                        offset = 210;
                        break;
                    case 8:
                        offset = 240;
                        break;
                }
                vectorEnemyToPlayer.X = _gamePanel.Player.Coordinates.X + 32 + offset - (_sprite.X + 32); //adds 32 to get to the center of bitmap 
                vectorEnemyToPlayer.Y = _gamePanel.Player.Coordinates.Y + 32 + offset - (_sprite.Y + 32);

                // math to make arrow fly at player
                _atkAngle = (Math.Atan2(vectorEnemyToPlayer.Y, vectorEnemyToPlayer.X) * 180) / 3.142;

                // so we use unit vector here to count how much it should move in one pixel to go towards player
                //angle of coordinate change basically
                double magnitude = Math.Sqrt(Math.Pow(vectorEnemyToPlayer.X, 2) + Math.Pow(vectorEnemyToPlayer.Y, 2));

                _delta.X = vectorEnemyToPlayer.X / magnitude;
                _delta.Y = vectorEnemyToPlayer.Y / magnitude;

                _sprite.Rotation = (float)_atkAngle;
                _sprite.Scale = 0.4f;

                _initialised = true;
            }
            else if (_behaviour == ProjectileBehaviour.OctaMove)
            {
                // use vector to find the angle between player and enemy
                Point2D vectorEnemyToPlayer;
                vectorEnemyToPlayer.X = _gamePanel.Player.Coordinates.X + 32 - (_sprite.X + 32); //adds 32 to get to the center of bitmap 
                vectorEnemyToPlayer.Y = _gamePanel.Player.Coordinates.Y + 32 - (_sprite.Y + 32);

                // math to make arrow fly at player
                _atkAngle = (Math.Atan2(vectorEnemyToPlayer.Y, vectorEnemyToPlayer.X) * 180) / 3.142;

                // so we use unit vector here to count how much it should move in one pixel to go towards player
                //angle of coordinate change basically
                double magnitude = Math.Sqrt(Math.Pow(vectorEnemyToPlayer.X, 2) + Math.Pow(vectorEnemyToPlayer.Y, 2));

                _delta.X = vectorEnemyToPlayer.X / magnitude;
                _delta.Y = vectorEnemyToPlayer.Y / magnitude;

                _sprite.Rotation = (float)_atkAngle;
                _sprite.Scale = 1f;

                _initialised = true;
            }


        }

        /// <summary>
        /// move the projectile
        /// </summary>
        public void Move()
        {
            if (!_initialised)
            {
                InitialCalculation();
            }

            if (_initialised && _isExist)
            {
                _sprite.X += ((float)_delta.X * _speed);
                _sprite.Y += ((float)_delta.Y * _speed);

            }

        }

        /// <summary>
        /// updates the projectile every cycle
        /// </summary>
        public void Update()
        {
            Move();

            TimeSpan elapsedTime = DateTime.Now - _startTime;
            if (elapsedTime.TotalSeconds >= 1 && _behaviour == ProjectileBehaviour.OctaMove)
            {
                _startTime = DateTime.Now;
                for (int i = 0; i < 8; i++)
                {
                    Projectile magicProjectile = new Projectile(_gamePanel, _sprite.Position, 7, false, ProjectileBehaviour.EightDirection, "magicProjectile2", _damage);
                    _gamePanel.AllProjectiles.AddProjectile(magicProjectile);
                    magicProjectile.AtkAngle += i;

                }
            }
        }

        /// <summary>
        /// destroy if projectile should not exist
        /// </summary>
        public void Destroy()
        {
            _isExist = false;
        }
    }
}