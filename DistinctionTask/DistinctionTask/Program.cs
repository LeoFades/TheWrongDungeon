using System;
using SplashKitSDK;

namespace DistinctionTask
{
    //Eternal Ascension
    public class Program
    {
        public static void Main(string[] args)
        {
            Window gameWindow = new Window("The Wrong Dungeon", 1600, 900);
            Bitmap gameIcon = SplashKit.LoadBitmap("gameIcon", "sprites/gameIcon.png");

            SplashKit.WindowSetIcon(gameWindow, gameIcon);

            //load pictures
            Bitmap bowImage = SplashKit.LoadBitmap("bow", "sprites/bow.png");
            Bitmap bowDrawnImage = SplashKit.LoadBitmap("bowDrawn", "sprites/bowDrawn.png");
            Bitmap playerImage = SplashKit.LoadBitmap("player", "sprites/player.png");
            Bitmap swordImage = SplashKit.LoadBitmap("sword", "sprites/sword.png");
            Bitmap swordSwingImage = SplashKit.LoadBitmap("swordSwing", "sprites/swordSwing.png");
            Bitmap arrowImage = SplashKit.LoadBitmap("arrow", "sprites/arrow.png");
            Bitmap staffImage = SplashKit.LoadBitmap("staff", "sprites/staff.png");
            Bitmap staffCastImage = SplashKit.LoadBitmap("staffCast", "sprites/staffCast.png");
            Bitmap magicProjectileImage = SplashKit.LoadBitmap("magicProjectile", "sprites/magicProjectile.png");
            Bitmap magicProjectileImage2 = SplashKit.LoadBitmap("magicProjectile2", "sprites/magicProjectile2.png");
            Bitmap banditImage = SplashKit.LoadBitmap("bandit", "sprites/bandit.png");
            Bitmap archerImage = SplashKit.LoadBitmap("archer", "sprites/archer.png");
            Bitmap knightImage = SplashKit.LoadBitmap("knight", "sprites/knight.png");
            Bitmap stoneFloorImage = SplashKit.LoadBitmap("stoneFloor", "sprites/stoneFloor.png");
            Bitmap voidImage = SplashKit.LoadBitmap("void", "sprites/void.png");
            Bitmap woodFloorImage = SplashKit.LoadBitmap("woodFloor", "sprites/woodFloor.png");
            Bitmap earthImage = SplashKit.LoadBitmap("earth", "sprites/earth.png");
            Bitmap portalBlueImage = SplashKit.LoadBitmap("portalBlue", "sprites/portalBlue.png");
            Bitmap portalGreenImage = SplashKit.LoadBitmap("portalGreen", "sprites/portalGreen.png");
            Bitmap portalPurpleImage = SplashKit.LoadBitmap("portalPurple", "sprites/portalPurple.png");
            Bitmap portalRedImage = SplashKit.LoadBitmap("portalRed", "sprites/portalRed.png");
            Bitmap healthImage = SplashKit.LoadBitmap("health", "sprites/health.png");
            Bitmap coinImage = SplashKit.LoadBitmap("coin", "sprites/coin.png");
            Bitmap shopImage = SplashKit.LoadBitmap("shop", "sprites/shop.png");
            Bitmap karlImage = SplashKit.LoadBitmap("karl", "sprites/karl.png");
            Bitmap castleImage = SplashKit.LoadBitmap("castle", "sprites/castle.png");

            //load music
            Music bossThemeMusic = SplashKit.LoadMusic("bossTheme", "audio/bossTheme.mp3");

            //load fonts
            Font barlowFont = SplashKit.LoadFont("barlow", "font/Barlow-Regular.ttf");
            Font zcoolFont = SplashKit.LoadFont("zcool", "font/ZCOOLXiaoWei-Regular.ttf");

            Point2D origin;
            origin.X = 0;
            origin.Y = 0;

            MainMenu menu = new MainMenu();
            Game game = new Game();
            bool initialised = true;


            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.Black);

                //main menu
                if (menu.AtMain)
                {
                    menu.Draw();
                    menu.CheckMain();
                    //to load fresh game, set initialise to false when leaving menu
                    initialised = menu.AtMain;
                }

                //play a game
                if (menu.GameStart)
                {
                    if (!initialised)
                    {
                        //loads fresh game
                        game = new Game();
                        initialised = true;

                    }

                    game.Check();
                    game.Update();
                    game.Draw();
                    menu.CheckGamePlay(game.GameState, game.isWin);

                }

                //lose
                if (menu.isGameOver)
                {
                    SplashKit.SetCameraPosition(origin);
                    menu.Draw();
                    menu.CheckGameOver();
                }

                //win
                if (menu.Win)
                {
                    SplashKit.SetCameraPosition(origin);
                    menu.Draw();
                    menu.CheckGameWin();
                }


                SplashKit.RefreshScreen(60);

            } while (!SplashKit.WindowCloseRequested("The Wrong Dungeon"));

            SplashKit.FreeAllBitmaps();
        }
    }
}
