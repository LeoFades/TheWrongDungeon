using System;
using SplashKitSDK;

#nullable disable

namespace DistinctionTask
{
    /// <summary>
    /// abstract parent class weapon
    /// </summary>
    public abstract class Weapon
    {
        protected Game _gamePanel;
        private Circle _pickupRange;
        protected int _damage;
        protected float _atkDelay;
        private Point2D _coordinates;
        protected bool _equipped;
        protected Sprite _sprite;
        protected Sprite _spriteAttacking;
        protected bool _attacking;
        protected Rectangle _hitbox;
        protected DateTime _startTime; //i keep track of stuff happening for how many secs

        public Weapon(Game game, int damage, float atkDelay, Point2D coordinates, string spriteImage, string spriteAttackingImage)
        {
            _gamePanel = game;

            _damage = damage;
            _atkDelay = atkDelay;

            _coordinates = coordinates;
            _equipped = false;

            _pickupRange = new Circle();
            _pickupRange.Center.X = _coordinates.X + 32;
            _pickupRange.Center.Y = _coordinates.Y + 32;
            _pickupRange.Radius = 20;


            _sprite = SplashKit.CreateSprite(spriteImage);
            _sprite.Scale = 0.5f;
            _sprite.X = (float)coordinates.X;
            _sprite.Y = (float)coordinates.Y;

            _spriteAttacking = SplashKit.CreateSprite(spriteAttackingImage);
            _spriteAttacking.Scale = 1f;
            _spriteAttacking.X = (float)coordinates.X;
            _spriteAttacking.Y = (float)coordinates.Y;


            _attacking = false;

            _hitbox = SplashKit.SpriteCollisionRectangle(_spriteAttacking);

            _startTime = DateTime.Now;

        }

        /// <summary>
        /// returns if the weapon is equipped
        /// </summary>
        /// <value></value>
        public bool IsEquipped
        {
            get
            {
                return _equipped;
            }
        }

        /// <summary>
        /// returns the damage of the weapon
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
        /// returns if the weapon is attacking
        /// for testing purposes, is not used anywhere else. 
        /// </summary>
        /// <value></value>
        public bool Attacking
        {
            get
            {
                return _attacking;
            }
        }

        /// <summary>
        /// check if the player is in range of the weapon
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsInRange(Player player)
        {
            Point2D centerPlayer;
            centerPlayer.X = player.Coordinates.X + 32;
            centerPlayer.Y = player.Coordinates.Y + 32;

            return SplashKit.PointInCircle(centerPlayer, _pickupRange);
        }

        /// <summary>
        /// attack style of weapon, each weapon should be different
        /// </summary>
        /// <param name="enemies"></param>
        public abstract void Attack(List<Enemy> enemies);

        /// <summary>
        /// when the player equips
        /// </summary>
        /// <returns></returns>
        public Weapon Equip()
        {
            _equipped = true;
            return this;
        }

        /// <summary>
        /// unequip said weapon
        /// </summary>
        /// <returns></returns>
        public Weapon Unequip()
        {
            //pickuprange update
            _pickupRange.Center.X = _sprite.X + 32;
            _pickupRange.Center.Y = _sprite.Y + 32;

            _equipped = false;
            return null;
        }

        /// <summary>
        /// update the weapon each cycle
        /// </summary>
        public virtual void Update()
        {
            if (_equipped)
            {
                if (_attacking)
                {
                    _spriteAttacking.X = (float)(_gamePanel.Player.Coordinates.X + 16);
                    _spriteAttacking.Y = (float)(_gamePanel.Player.Coordinates.Y + 16);

                    Point2D rotationPointAttacking;
                    rotationPointAttacking.X = 16;
                    rotationPointAttacking.Y = 16;

                    _spriteAttacking.AnchorPoint = rotationPointAttacking;
                    _spriteAttacking.Rotation = (float)_gamePanel.Player.AngleFacing;

                    _hitbox = SplashKit.SpriteCollisionRectangle(_spriteAttacking);

                }
                else
                {
                    _sprite.X = (float)(_gamePanel.Player.Coordinates.X + 16);
                    _sprite.Y = (float)(_gamePanel.Player.Coordinates.Y + 16);

                    Point2D rotationPoint;
                    rotationPoint.X = 0;
                    rotationPoint.Y = 0;

                    _sprite.AnchorPoint = rotationPoint;
                    _sprite.Rotation = (float)_gamePanel.Player.AngleFacing;

                }
            }
            else
            {

                Point2D rotationPoint;
                rotationPoint.X = 32;
                rotationPoint.Y = 32;

                _sprite.AnchorPoint = rotationPoint;

            }
        }

        /// <summary>
        /// draw the waepon
        /// </summary>
        /// <param name="player"></param>
        public void Draw(Player player)
        {
            if (_equipped)
            {
                if (_attacking)
                {
                    _spriteAttacking.Draw();
                    //SplashKit.DrawRectangle(Color.Purple, _hitbox);
                }
                else
                {
                    _sprite.Draw();
                }

            }
            else
            {

                _pickupRange.Draw(Color.Green);

                _sprite.Draw();

            }
        }

    }
}