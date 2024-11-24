using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// ui class that handles all the ui in the game
    /// </summary>
    public abstract class UI
    {
        protected bool _gameplay;
        protected bool _gameOver;
        public UI()
        {
            _gameplay = false;
            _gameOver = false;
        }

        /// <summary>
        /// property if the game has started
        /// </summary>
        /// <value></value>
        public bool GameStart
        {
            get
            {
                return _gameplay;
            }
            set
            {
                _gameplay = value;
            }
        }

        /// <summary>
        /// property of if the player has lost
        /// </summary>
        /// <value></value>
        public bool isGameOver
        {
            get
            {
                return _gameOver;
            }
            set
            {
                _gameOver = value;
            }
        }

        /// <summary>
        /// draw the ui out
        /// </summary>
        public abstract void Draw();
    }
}