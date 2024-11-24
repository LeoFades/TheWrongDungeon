using System;
using SplashKitSDK;

#nullable disable

namespace DistinctionTask
{
    /// <summary>
    /// player subclass of the character parent class
    /// </summary>
    public class Player : Character
    {
        private Weapon _weapon;
        private double _angleFacing;
        private List<Coin> _coins;

        public Player(Game game, int health, Point2D coordinates, string spriteImage) :
            base(game, health, coordinates, spriteImage)
        {
            _weapon = null;
            _angleFacing = 0;

            _coins = new List<Coin>();
        }

        /// <summary>
        /// returns the amount of coins the player have
        /// </summary>
        /// <value>int</value>
        public int Coin
        {
            get
            {
                return _coins.Count();
            }
        }

        /// <summary>
        /// returns coordinates of player
        /// </summary>
        /// <value>point2d</value>
        public Point2D Coordinates
        {
            get
            {
                return _sprite.Position;
            }
        }

        /// <summary>
        /// returns the angle at which the player is facing
        /// </summary>
        /// <value>double</value>
        public double AngleFacing
        {
            get
            {
                return _angleFacing;
            }
        }

        /// <summary>
        /// property of the weapon the player is holding
        /// </summary>
        /// <value></value>
        public Weapon Weapon
        {
            get
            {
                return _weapon;
            }
            set
            {
                _weapon = value;
            }
        }

        /// <summary>
        /// adds coin into the user's wallet
        /// </summary>
        /// <param name="coin">the coin to be added</param>
        public void AddCoin(Coin coin)
        {
            _coins.Add(coin);
        }

        /// <summary>
        /// remove coins from user wallet
        /// </summary>
        /// <param name="amount">how much money to remove</param>
        public void RemoveCoin(int amount)
        {
            List<Coin> tempCoinList = _coins.ToList();
            for (int i = 0; i < amount; i++)
            {
                _coins.RemoveAt(_coins.Count() - 1);
            }
        }

        /// <summary>
        /// update the user object every cycle
        /// </summary>
        public override void Update()
        {
            Point2D mouseLocation = SplashKit.ToWorld(SplashKit.MousePosition());


            // use vector to find the angle between player and cursor
            Point2D vectorMouseToPlayer;
            vectorMouseToPlayer.X = mouseLocation.X - (_sprite.X + 32); //adds 32 to get to the center of bitmap 
            vectorMouseToPlayer.Y = mouseLocation.Y - (_sprite.Y + 32);

            // math to make player always look at cursor
            double angle = (Math.Atan2(vectorMouseToPlayer.Y, vectorMouseToPlayer.X) * 180) / 3.142;
            _angleFacing = angle;

            //this part i use sprite fr
            _sprite.Rotation = (float)angle;


            SplashKit.SetCameraX(_sprite.X - 800 + 32);
            SplashKit.SetCameraY(_sprite.Y - 450 + 32);

        }

        /// <summary>
        /// checks for necessary stuff, like when user die
        /// </summary>
        public override void Check()
        {
            if (base.Health <= 0)
            {
                Destroy();
            }
        }

        /// <summary>
        /// draw the user
        /// </summary>
        public override void Draw()
        {

            _sprite.Draw();

            //display hitbox
            //SplashKit.DrawCircle(Color.DarkRed, _hitbox);
        }

        /// <summary>
        /// when attacking, this is called
        /// </summary>
        public override void Attack()
        {
            if (_weapon != null)
            {
                _weapon.Attack(_gamePanel.AllEnemies);
            }

        }

        /// <summary>
        /// movement for user.
        /// </summary>
        /// <param name="direction">direction to go</param>
        public void Move(PlayerDirection direction)
        {
            if (direction == PlayerDirection.Up)
            {
                if (!IsIntoWall(_sprite.X, _sprite.Y - 4))
                {
                    _sprite.Y -= 4;
                }
            }
            if (direction == PlayerDirection.Left)
            {
                if (!IsIntoWall(_sprite.X - 4, _sprite.Y))
                {
                    _sprite.X -= 4;
                }

            }
            if (direction == PlayerDirection.Down)
            {
                if (!IsIntoWall(_sprite.X, _sprite.Y + 4))
                {
                    _sprite.Y += 4;
                }

            }
            if (direction == PlayerDirection.Right)
            {
                if (!IsIntoWall(_sprite.X + 4, _sprite.Y))
                {
                    _sprite.X += 4;
                }

            }

            _hitbox = SplashKit.SpriteCollisionCircle(_sprite);
        }

        /// <summary>
        /// destroy the object when die
        /// </summary>
        public override void Destroy()
        {
            _gamePanel.GameState = false;
            if (SplashKit.MusicPlaying())
            {
                SplashKit.FadeMusicOut(2000);

            }
        }
    }
}