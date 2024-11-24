using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// Gameplay child class of UI parent class
    /// </summary>
    public class Gameplay : UI
    {
        private int _healthBar;
        private int _coinAmount;
        private int _bossHealthBar;


        public Gameplay() : base()
        {
            _healthBar = 10;
            _coinAmount = 0;
            _bossHealthBar = 0;


        }

        /// <summary>
        /// player healthbar
        /// </summary>
        public void HealthBar()
        {
            SplashKit.DrawBitmap("health", SplashKit.CameraX() + 15, SplashKit.CameraY() + 20);
            Rectangle bar = new Rectangle();
            bar.X = SplashKit.CameraX() + 55;
            bar.Y = SplashKit.CameraY() + 20;
            bar.Width = _healthBar * 15;
            bar.Height = 32;



            SplashKit.FillRectangle(Color.SeaGreen, bar);
            SplashKit.DrawRectangle(Color.ForestGreen, bar);
        }

        /// <summary>
        /// boss health bar
        /// </summary>
        public void BossHealthBar()
        {

            Rectangle bar = new Rectangle();
            bar.X = SplashKit.CameraX() + 300;
            bar.Y = SplashKit.CameraY() + 850;
            bar.Width = _bossHealthBar * 20;
            bar.Height = 32;

            Rectangle bar2 = new Rectangle();
            bar2.X = SplashKit.CameraX() + 300;
            bar2.Y = SplashKit.CameraY() + 850;
            bar2.Width = 1000;
            bar2.Height = 32;

            SplashKit.FillRectangle(Color.DarkSlateGray, bar2);
            SplashKit.DrawRectangle(Color.Black, bar2);

            SplashKit.FillRectangle(Color.DarkRed, bar);
            SplashKit.DrawRectangle(Color.Black, bar);

        }

        /// <summary>
        /// coins the player have
        /// </summary>
        public void CoinDisplay()
        {
            SplashKit.DrawBitmap("coin", SplashKit.CameraX() + 15, SplashKit.CameraY() + 60);
            SplashKit.DrawText(_coinAmount.ToString(), Color.DarkGoldenrod, "barlow", 30, SplashKit.CameraX() + 55, SplashKit.CameraY() + 58);
        }

        public void Check(int playerhealth, int coinAmount)
        {
            _healthBar = playerhealth;
            _coinAmount = coinAmount;


        }

        /// <summary>
        /// checks for boss health
        /// </summary>
        /// <param name="bossHealth">boss health in int</param>
        public void CheckBoss(int bossHealth)
        {
            _bossHealthBar = bossHealth;
        }

        /// <summary>
        /// draw out the ui
        /// </summary>
        public override void Draw()
        {
            HealthBar();
            CoinDisplay();

            if (_bossHealthBar > 0)
            {
                BossHealthBar();
            }
        }
    }
}