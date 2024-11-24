using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// Exit child class of Structure parent class
    /// </summary>
    public class Exit : Structure
    {
        public Exit(Game game, Point2D coordinates, string spriteImage) :
            base(game, coordinates, spriteImage)
        {
            _sprite.Scale = 0.125f;
            _noEnemyRange.Radius = 500;
        }

        /// <summary>
        /// when interacted, you exit the level and win, or is it
        /// </summary>
        public override void Interact()
        {
            if (_gamePanel.AllEnemies.Count() == 0 && SplashKit.SpriteCollision(_sprite, _gamePanel.Player.Sprite) && !_gamePanel.IsBossSpawned)
            {
                _gamePanel.BossSpawn();
                SplashKit.PlayMusic("bossTheme");
                SplashKit.SetMusicVolume(0.1f);


            }
            else if (_gamePanel.AllEnemies.Count() == 0 && SplashKit.SpriteCollision(_sprite, _gamePanel.Player.Sprite) &&
                _gamePanel.BossKarl.Health <= 0 && _gamePanel.IsBossSpawned)
            {
                _gamePanel.isWin = true;
                _gamePanel.GameState = false;
                if (SplashKit.MusicPlaying())
                {
                    SplashKit.FadeMusicOut(2000);

                }
            }
        }
    }
}