using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// class to handle projectiles
    /// </summary>
    public class ProjectilesHandler
    {
        private Game _gamePanel;
        private List<Projectile> _allProjectiles;
        private List<Projectile> _projectilesQueue;

        public ProjectilesHandler(Game game)
        {
            _gamePanel = game;
            _allProjectiles = new List<Projectile>();
            _projectilesQueue = new List<Projectile>();
        }

        /// <summary>
        /// add a projectile into the list to handle it
        /// </summary>
        /// <param name="p"></param>
        public void AddProjectile(Projectile p)
        {
            _projectilesQueue.Add(p);
        }

        /// <summary>
        /// checks for necessary stuff, eg wall
        /// </summary>
        public void Check()
        {
            List<Projectile> tempProjList = _allProjectiles.ToList();
            foreach (Projectile p in tempProjList)
            {
                foreach (Character c in _gamePanel.AllCharacters)
                {
                    //test for player's projectile on enemy
                    if (SplashKit.SpriteCollision(p.Sprite, c.Sprite) && p.isFromPlayer && c is Enemy)
                    {
                        c.DecreaseHealth(p.Damage);

                        if (!_gamePanel.Player.IsIntoWall((float)(c.Sprite.Position.X + p.Delta.X), (float)(c.Sprite.Position.Y + p.Delta.Y)))
                        {
                            c.Knockback(p.Delta);
                        }

                        p.Destroy();
                        //Console.WriteLine("HIT");
                        _allProjectiles.Remove(p);
                    }

                    if (SplashKit.SpriteCollision(p.Sprite, c.Sprite) && !p.isFromPlayer && c is Player)
                    {
                        c.DecreaseHealth(p.Damage);
                        p.Destroy();
                        //Console.WriteLine(_gamePanel.Player.Health);
                        _allProjectiles.Remove(p);
                    }
                }

                //im just gonna borrow it from player here...
                if (_gamePanel.Player.IsIntoWall((float)p.NextMove().X, (float)p.NextMove().Y))
                {
                    p.Destroy();
                    _allProjectiles.Remove(p);
                }

                //removing if too many projectiles since it lags
                // if (tempProjList.Count() > 70)
                // {
                //     _allProjectiles.RemoveAt(1);
                // }
            }
        }

        /// <summary>
        /// update the projectile
        /// </summary>
        public void Update()
        {
            foreach (Projectile p in _allProjectiles)
            {
                p.Update();
            }

            List<Projectile> tempList = _projectilesQueue.ToList();
            foreach (Projectile p in tempList)
            {
                _allProjectiles.Add(p);
                //need a queue since we are looping through projectiles bro and we cant add in one suddenly
                _projectilesQueue.Remove(p);
            }
        }

        /// <summary>
        /// draws the projectile
        /// </summary>
        public void Draw()
        {
            foreach (Projectile p in _allProjectiles)
            {
                p.Draw();
            }
        }
    }
}