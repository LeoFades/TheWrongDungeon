using System;
using System.Collections.Generic;
using SplashKitSDK;

#nullable disable

namespace DistinctionTask
{
    /// <summary>
    /// the main clas that hold everything together!!! the goat of the program. Instantiate this for a fresh game.
    /// </summary>
    public class Game
    {
        private List<Character> _allCharacters;
        private List<Enemy> _enemies;
        private Player _player;
        private List<Weapon> _weapons;
        private TileManager _tileManager;
        private InputHandler _input;
        private ProjectilesHandler _projectilesHandler;
        private List<Structure> _structures;
        private List<Item> _items;
        private bool _gameState;
        private bool _isWin;
        private bool _isBossSpawned;
        private Karl _bossKarl;
        private Gameplay _gameUI;


        public Game()
        {
            _allCharacters = new List<Character>();
            _enemies = new List<Enemy>();
            _weapons = new List<Weapon>();
            _items = new List<Item>();
            _structures = new List<Structure>();

            _gameUI = new Gameplay();
            _bossKarl = null;


            //structure spawn
            Point2D spawnCoordinates;
            spawnCoordinates.X = 250;
            spawnCoordinates.Y = 700;
            Spawn playerSpawn = new Spawn(this, spawnCoordinates, "portalBlue");
            _structures.Add(playerSpawn);

            Point2D exitCoordinates;
            exitCoordinates.X = 176 * 16;
            exitCoordinates.Y = 167 * 16;
            Exit playerExit = new Exit(this, exitCoordinates, "portalRed");
            _structures.Add(playerExit);

            Point2D shopCoordinates;
            shopCoordinates.X = 13 * 16;
            shopCoordinates.Y = 90 * 16;
            Shop shop = new Shop(this, shopCoordinates, "shop");
            _structures.Add(shop);

            Point2D shopCoordinates2;
            shopCoordinates2.X = 165 * 16;
            shopCoordinates2.Y = 10 * 16;
            Shop shop2 = new Shop(this, shopCoordinates2, "shop");
            _structures.Add(shop2);

            //player spawn
            _player = new Player(this, 10, playerSpawn.Coordinates, "player");
            _allCharacters.Add(_player);

            //overpowered bow for testing purposes
            //_weapons.Add(new Bow(this, 20, 30, 1, spawnCoordinates, "bow", "bowDrawn"));

            //map
            _tileManager = new TileManager();
            _input = new InputHandler(this);
            _projectilesHandler = new ProjectilesHandler(this);

            //enemy and start weapon 
            InitialSpawn();

            _isWin = false;
            _gameState = true;
            _isBossSpawned = false;
        }

        /// <summary>
        /// property of all the characters present
        /// </summary>
        /// <value>Character list</value>
        public List<Character> AllCharacters
        {
            get
            {
                return _allCharacters;
            }
            set
            {
                _allCharacters = value;
            }
        }

        /// <summary>
        /// property of all enemy present
        /// </summary>
        /// <value>List of enemy</value>
        public List<Enemy> AllEnemies
        {
            get
            {
                return _enemies;
            }
            set
            {
                _enemies = value;
            }
        }

        /// <summary>
        /// property of all waepon present 
        /// </summary>
        /// <value>List of weapons</value>
        public List<Weapon> AllWeapons
        {
            get
            {
                return _weapons;
            }
            set
            {
                _weapons = value;
            }
        }

        /// <summary>
        /// property of all items present
        /// </summary>
        /// <value>List of items</value>
        public List<Item> AllItems
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        /// <summary>
        /// property of all structures present
        /// </summary>
        /// <value>List of structure</value>
        public List<Structure> AllStructures
        {
            get
            {
                return _structures;
            }
            set
            {
                _structures = value;
            }
        }

        /// <summary>
        /// property of the player
        /// </summary>
        /// <value>Player</value>
        public Player Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
            }
        }

        /// <summary>
        /// property of all projectiles present
        /// </summary>
        /// <value>Projectiles handler</value>
        public ProjectilesHandler AllProjectiles
        {
            get
            {
                return _projectilesHandler;
            }
            set
            {
                _projectilesHandler = value;
            }
        }

        /// <summary>
        /// property of the tile manager
        /// </summary>
        /// <value>TileManager</value>
        public TileManager TileManager
        {
            get
            {
                return _tileManager;
            }
            set
            {
                _tileManager = value;
            }
        }

        /// <summary>
        /// property of the game state
        /// </summary>
        /// <value>Boolean</value>
        public bool GameState
        {
            get
            {
                return _gameState;
            }
            set
            {
                _gameState = value;
            }
        }

        /// <summary>
        /// property if the player won
        /// </summary>
        /// <value>boolean</value>
        public bool isWin
        {
            get
            {
                return _isWin;
            }
            set
            {
                _isWin = value;
            }
        }

        /// <summary>
        /// property of the boss object
        /// </summary>
        /// <value>Karl</value>
        public Karl BossKarl
        {
            get
            {
                return _bossKarl;
            }
            set
            {
                _bossKarl = value;
            }
        }

        /// <summary>
        /// property if the boss is spawned
        /// </summary>
        /// <value>bool</value>
        public bool IsBossSpawned
        {
            get
            {
                return _isBossSpawned;
            }
            set
            {
                _isBossSpawned = value;
            }
        }

        /// <summary>
        /// checks for all the objects present 
        /// </summary>
        public void Check()
        {
            _input.Check();
            _projectilesHandler.Check();
            _gameUI.Check(_player.Health, _player.Coin);

            if (_isBossSpawned)
            {
                _gameUI.CheckBoss(_bossKarl.Health);
            }

            foreach (Character c in _allCharacters)
            {
                c.Check();
            }

            foreach (Item i in _items)
            {
                i.Check();
            }

        }

        /// <summary>
        /// update for all the objects present
        /// </summary>
        public void Update()
        {
            _projectilesHandler.Update();

            foreach (Character c in _allCharacters)
            {
                c.Update();
            }

            foreach (Weapon w in _weapons)
            {
                w.Update();
            }

            //enemy status
            List<Enemy> tempEnemyList = _enemies.ToList();
            foreach (Enemy e in tempEnemyList)
            {

                if (e.IsExist)
                {
                    if (e.Health <= 0)
                    {
                        e.Destroy();
                    }
                }
                else
                {
                    _enemies.Remove(e);
                    _allCharacters.Remove(e);
                }


            }

            //item status
            List<Item> tempItemList = _items.ToList();
            foreach (Item i in tempItemList)
            {
                if (!i.IsExist)
                {
                    _items.Remove(i);
                }
            }
        }

        /// <summary>
        /// draw all the objects present
        /// </summary>
        public void Draw()
        {
            SplashKit.ClearScreen(Color.Black);
            _tileManager.Draw();

            foreach (Structure s in _structures)
            {
                s.Draw();
            }

            foreach (Item i in _items)
            {
                i.Draw();
            }

            _player.Draw();
            _projectilesHandler.Draw();

            foreach (Enemy e in _enemies)
            {
                e.Draw();
            }

            foreach (Weapon w in _weapons)
            {
                w.Draw(_player);
            }

            _gameUI.Draw();


        }

        /// <summary>
        /// spawns a boss
        /// </summary>
        public void BossSpawn()
        {
            Point2D bossRoom;
            bossRoom.X = 164 * 16;
            bossRoom.Y = 118 * 16;
            Karl karl = new Karl(this, 2000, 2, 50, bossRoom, "karl");
            _enemies.Add(karl);
            _allCharacters.Add(karl);
            _bossKarl = karl;
            _isBossSpawned = true;
        }

        /// <summary>
        /// spawns all enemy on map
        /// </summary>
        public void InitialSpawn()
        {
            Point2D coordinates;
            coordinates.X = 0;
            coordinates.Y = 0;


            bool startWeapon = false;
            bool enoughEnemy = false;


            for (int row = 0; row < 200; row++)
            {
                for (int col = 0; col < 200; col++)
                {
                    if (_tileManager.MapTile[row, col] == 1)
                    {
                        Random chance = new Random();
                        Random type = new Random();
                        Random weaponSpawn = new Random();
                        int randomType = type.Next(1, 4);

                        bool isInNoEnemyRange = false;
                        foreach (Structure s in _structures)
                        {
                            if (SplashKit.PointInCircle(coordinates, s.NoEnemyRange))
                            {
                                isInNoEnemyRange = true;
                            }

                            if (weaponSpawn.Next(1, 200) == 1 && s is Spawn && isInNoEnemyRange && !startWeapon)
                            {

                                if (randomType == 1)
                                {
                                    _weapons.Add(new Sword(this, 5, 1, coordinates, "sword", "swordSwing"));

                                }

                                if (randomType == 2)
                                {
                                    _weapons.Add(new Bow(this, 10, 3, 1, coordinates, "bow", "bowDrawn"));

                                }

                                if (randomType == 3)
                                {
                                    _weapons.Add(new Staff(this, 1, 1, 2, coordinates, "staff", "staffCast"));

                                }



                                //Console.WriteLine("WEapon SPAWQNED");

                                startWeapon = true;
                            }
                        }

                        if (chance.Next(1, 500) == 1 && !isInNoEnemyRange && _enemies.Count() < 60)
                        {
                            switch (randomType)
                            {
                                case 1:
                                    _enemies.Add(new Knight(this, 500, 1, 10, coordinates, "knight"));
                                    break;
                                case 2:
                                    _enemies.Add(new Bandit(this, 600, 2, 6, coordinates, "bandit"));
                                    break;
                                case 3:
                                    _enemies.Add(new Archer(this, 850, 1, 7, coordinates, "archer"));
                                    break;
                            }
                        }
                    }
                    coordinates.X += 16;
                }

                coordinates.X = 0;
                coordinates.Y += 16;
            }

            //Console.WriteLine(_enemies.Count() + " enemies are here");

            if (_enemies.Count() > 40)
            {
                enoughEnemy = true;
            }

            if (!startWeapon || !enoughEnemy)
            {
                InitialSpawn();
            }

            foreach (Enemy e in _enemies)
            {
                _allCharacters.Add(e);
            }
        }

    }
}