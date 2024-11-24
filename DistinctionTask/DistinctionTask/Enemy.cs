using System;
using SplashKitSDK;
using System.Collections.Generic;

#nullable disable

namespace DistinctionTask
{
    /// <summary>
    /// enemy subclass of the character parent class
    /// </summary>
    public abstract class Enemy : Character
    {
        protected double _movementSpeed;
        protected bool _playerInSight;
        protected Circle _sightRange;
        private DrawingOptions _drawOptions;
        private bool _isExist;
        protected DateTime _startTime;
        protected bool _attacking;

        public Enemy(Game game, double sightRadius, double movementSpeed, int health, Point2D coordinates, string spriteImage) :
            base(game, health, coordinates, spriteImage)
        {
            _movementSpeed = movementSpeed;
            _playerInSight = false;

            _isExist = true;

            _sightRange = new Circle();
            _sightRange.Radius = sightRadius;
            _sightRange.Center.X = _sprite.X + 32;
            _sightRange.Center.Y = _sprite.Y + 32;

            _drawOptions = SplashKit.OptionScaleBmp(0.5, 0.5);

            _startTime = DateTime.Now;
            _attacking = false;
        }

        /// <summary>
        /// returns if the enemy exist
        /// </summary>
        /// <value>bool</value>
        public bool IsExist
        {
            get
            {
                return _isExist;
            }
        }

        /// <summary>
        /// check if the player is within sight range
        /// </summary>
        /// <param name="playerLocation">where the player is</param>
        public void SeePlayer(Point2D playerLocation)
        {
            Point2D playerCenter;
            playerCenter.X = playerLocation.X + 32;
            playerCenter.Y = playerLocation.Y + 32;
            if (SplashKit.PointInCircle(playerCenter, _sightRange))
            {
                _playerInSight = true;
            }
            else
            {
                _playerInSight = false;
            }
        }

        /// <summary>
        /// the movement for an enemy, can be overriden if enemy moves differently
        /// </summary>
        public virtual void Move()
        {
            SeePlayer(_gamePanel.Player.Coordinates);

            if (_playerInSight)
            {
                Point2D vectorEnemyToPlayer;
                vectorEnemyToPlayer.X = (_gamePanel.Player.Coordinates.X + 32) - (_sprite.X + 32); //adds 32 to get to the center of bitmap 
                vectorEnemyToPlayer.Y = (_gamePanel.Player.Coordinates.Y + 32) - (_sprite.Y + 32);

                // so we use unit vector here to count how much it should move in one pixel to go towards player
                //angle of coordinate change basically
                double magnitude = Math.Sqrt(Math.Pow(vectorEnemyToPlayer.X, 2) + Math.Pow(vectorEnemyToPlayer.Y, 2));

                Point2D unitVector;
                unitVector.X = vectorEnemyToPlayer.X / magnitude;
                unitVector.Y = vectorEnemyToPlayer.Y / magnitude;

                //prevent body collision with player
                Point2D centerCoordinates;
                centerCoordinates.X = _sprite.X + 32;
                centerCoordinates.Y = _sprite.Y + 32;

                //check for collision
                bool collision = false;

                List<Enemy> tempList = _gamePanel.AllEnemies.ToList();
                tempList.Remove(this);

                foreach (Enemy e in tempList)
                {
                    if (SplashKit.SpriteCollision(_gamePanel.Player.Sprite, _sprite) || SplashKit.SpriteCollision(e.Sprite, _sprite))
                    {
                        collision = true;

                    }
                }

                if (!collision && !IsIntoWall(_sprite.X + (float)(unitVector.X * _movementSpeed), _sprite.Y + (float)(unitVector.Y * _movementSpeed)))
                {

                    //now we move by adding the unitvector to coordinates
                    _sprite.X += (float)(unitVector.X * _movementSpeed);
                    _sprite.Y += (float)(unitVector.Y * _movementSpeed);

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
        }

        /// <summary>
        /// override method for enemy attack
        /// </summary>
        public override void Attack()
        {
            _startTime = DateTime.Now;
            _attacking = true;

        }

        /// <summary>
        /// checks for necessary stuff
        /// </summary>
        public override void Check()
        {
            if (_playerInSight && !_attacking)
            {
                Attack();
            }
        }

        /// <summary>
        /// updates the enemy object every cycle
        /// </summary>
        public override void Update()
        {


            //gets player location so they can keep their sights on him while in range
            if (_playerInSight)
            {

                // use unit vector to find the angle between player and cursor
                Point2D vectorEnemyToPlayer;
                vectorEnemyToPlayer.X = (_gamePanel.Player.Coordinates.X + 32) - (_sprite.X + 32); //adds 32 to get to the center of bitmap 
                vectorEnemyToPlayer.Y = (_gamePanel.Player.Coordinates.Y + 32) - (_sprite.Y + 32);

                // math to make player always look at cursor
                double angle = (Math.Atan2(vectorEnemyToPlayer.Y, vectorEnemyToPlayer.X) * 180) / 3.142;

                //drawing options
                // DrawingOptions size = SplashKit.OptionScaleBmp(0.5, 0.5);
                // _drawOptions = SplashKit.OptionRotateBmp(angle, size); // adds the size to the options also

                _sprite.Rotation = (float)angle;

            }
        }

        /// <summary>
        /// draws the enemy object
        /// </summary>
        public override void Draw()
        {
            //display hitbox
            SplashKit.DrawCircle(Color.DarkRed, _hitbox);

            //_spriteBitmap.Draw(_coordinates.X, _coordinates.Y, _drawOptions);
            _sprite.Draw();
            //_sightRange.Draw(Color.Yellow);
        }

        /// <summary>
        /// destroys the enemy object, when they die
        /// </summary>
        public override void Destroy()
        {
            _isExist = false;

            Random itemAmount = new Random();
            Random itemType = new Random();

            int items = itemAmount.Next(1, 5);
            int type = itemType.Next(0, 2);

            for (int i = 0; i < items; i++)
            {
                type = itemType.Next(0, 2);
                switch (type)
                {
                    case 0:
                        _gamePanel.AllItems.Add(new Coin(_gamePanel, _sprite.Position));
                        break;
                    case 1:
                        _gamePanel.AllItems.Add(new Health(_gamePanel, _sprite.Position));
                        break;
                }
            }
        }
    }
}