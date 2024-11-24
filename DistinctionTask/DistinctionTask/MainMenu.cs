using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// main menu subclass for the UI parent class
    /// </summary>
    public class MainMenu : UI
    {
        private bool _main;
        private bool _win;
        private Rectangle _playRect;
        private Rectangle _quitRect;
        private Sprite _menuImage;

        public MainMenu() : base()
        {
            _main = true;
            _win = false;

            Rectangle _playRect = new Rectangle();
            _playRect.X = 710;
            _playRect.Y = 400;
            _playRect.Height = 80;
            _playRect.Width = 140;

            Rectangle _quitRect = new Rectangle();
            _quitRect.X = 710;
            _quitRect.Y = 500;
            _quitRect.Height = 80;
            _quitRect.Width = 140;

            _menuImage = SplashKit.CreateSprite("castle");
            _menuImage.Scale = 0.5f;
            _menuImage.X = 530;
            _menuImage.Y = -50;

        }

        /// <summary>
        /// property of the player is currently at mainmenu
        /// </summary>
        /// <value>boolean</value>
        public bool AtMain
        {
            get
            {
                return _main;
            }
            set
            {
                _main = value;
            }
        }

        /// <summary>
        /// property if the player has won
        /// </summary>
        /// <value>boolean</value>
        public bool Win
        {
            get
            {
                return _win;
            }
            set
            {
                _win = value;
            }
        }

        /// <summary>
        /// call this when the player loses
        /// </summary>
        public void GameOver()
        {
            _gameOver = true;
            _gameplay = false;
        }

        /// <summary>
        /// call this when player win
        /// </summary>
        public void PlayerWin()
        {
            _gameplay = false;
            _win = true;
        }

        /// <summary>
        /// main title ui
        /// </summary>
        public void TitleMain()
        {
            _menuImage.Draw();
            SplashKit.DrawText("The Wrong Dungeon", Color.LightGray, "zcool", 100, 360, 300);

        }

        /// <summary>
        /// made by someone ui
        /// </summary>
        public void CreditMain()
        {
            SplashKit.DrawText("A game by Leo", Color.DimGray, "barlow", 20, 716, 430);

        }

        /// <summary>
        /// instructions on how to play the game ui
        /// </summary>
        public void InstructionsMain()
        {
            SplashKit.DrawText("WASD to move, E to equip, F to interact, Left Mouse Button to attack", Color.DimGray, "barlow", 20, 500, 870);

        }

        /// <summary>
        /// gameover ui
        /// </summary>
        public void TitleGameOver()
        {
            SplashKit.DrawText("GAME OVER", Color.LightGray, "zcool", 100, 500, 200);
        }

        /// <summary>
        /// win ui
        /// </summary>
        public void TitleWin()
        {
            SplashKit.DrawText("YOU WIN", Color.LightGray, "zcool", 100, 570, 200);

        }

        /// <summary>
        /// play button
        /// </summary>
        public void Play()
        {
            SplashKit.DrawText("PLAY", Color.LightGray, "barlow", 60, 710, 500);
            _playRect.X = 710;
            _playRect.Y = 500;
            _playRect.Height = 80;
            _playRect.Width = 140;
            //SplashKit.DrawRectangle(Color.Yellow, _playRect);
        }

        /// <summary>
        /// back to menu button
        /// </summary>
        public void PlayAgain()
        {
            SplashKit.DrawText("MENU", Color.LightGray, "barlow", 60, 700, 500);
            _playRect.X = 710;
            _playRect.Y = 500;
            _playRect.Height = 80;
            _playRect.Width = 140;
            //SplashKit.DrawRectangle(Color.Yellow, _playRect);
        }

        /// <summary>
        /// quit the game button
        /// </summary>
        public void Quit()
        {
            SplashKit.DrawText("QUIT", Color.LightGray, "barlow", 60, 720, 600);
            _quitRect.X = 710;
            _quitRect.Y = 600;
            _quitRect.Height = 80;
            _quitRect.Width = 140;
            //SplashKit.DrawRectangle(Color.Yellow, _quitRect);
        }

        /// <summary>
        /// check for user inputs at main menu
        /// </summary>
        public void CheckMain()
        {
            Point2D mousePosition = SplashKit.MousePosition();
            if (SplashKit.PointInRectangle(mousePosition, _playRect) && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _main = false;
                _gameplay = true;

            }
            if (SplashKit.PointInRectangle(mousePosition, _quitRect) && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                SplashKit.CloseWindow("The Wrong Dungeon");

            }
        }

        /// <summary>
        /// checks for when user win or lose
        /// </summary>
        /// <param name="gameplay"></param>
        /// <param name="isWin"></param>
        public void CheckGamePlay(bool gameplay, bool isWin)
        {
            if (!gameplay && !isWin)
            {
                GameOver();
            }
            else if (!gameplay && isWin)
            {
                PlayerWin();
            }
        }

        /// <summary>
        /// check for input when user lose
        /// </summary>
        public void CheckGameOver()
        {
            Point2D mousePosition = SplashKit.MousePosition();
            if (SplashKit.PointInRectangle(mousePosition, _playRect) && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _main = true;
                _gameOver = false;

            }
            if (SplashKit.PointInRectangle(mousePosition, _quitRect) && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                SplashKit.CloseWindow("The Wrong Dungeon");

            }
        }

        /// <summary>
        /// checks for input when user win
        /// </summary>
        public void CheckGameWin()
        {
            Point2D mousePosition = SplashKit.MousePosition();
            if (SplashKit.PointInRectangle(mousePosition, _playRect) && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _main = true;
                _win = false;

            }
            if (SplashKit.PointInRectangle(mousePosition, _quitRect) && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                SplashKit.CloseWindow("The Wrong Dungeon");

            }
        }

        /// <summary>
        /// draw out all ui
        /// </summary>
        public override void Draw()
        {
            if (_main)
            {
                TitleMain();
                CreditMain();
                InstructionsMain();
                Play();
                Quit();
            }

            if (_gameOver)
            {
                TitleGameOver();
                PlayAgain();
                Quit();
            }

            if (_win)
            {
                TitleWin();
                PlayAgain();
                Quit();
            }

        }
    }
}