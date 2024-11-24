using System;
using System.Collections.Generic;
using SplashKitSDK;
using NUnit.Framework;

namespace DistinctionTask
{
    [TestFixture]
    public class CharacterTest
    {
        [Test]
        public void AttackTest()
        {
            Game game = new Game();
            SplashKit.LoadBitmap("player", "sprites/player.png");
            Point2D coordinates;
            coordinates.X = 10;
            coordinates.Y = 10;
            Player p = new Player(game, 10, coordinates, "player");
            Bow bow = new Bow(game, 20, 30, 1, coordinates, "bow", "bowDrawn");

            p.Weapon = bow.Equip();

            Assert.IsTrue(bow.IsEquipped);

            p.Attack();

            Assert.IsTrue(p.Weapon.Attacking);
        }

        [Test]
        public void CheckTest()
        {
            Game game = new Game();
            SplashKit.LoadBitmap("player", "sprites/player.png");
            Point2D coordinates;
            coordinates.X = 10;
            coordinates.Y = 10;
            Player p = new Player(game, 10, coordinates, "player");
            Bow bow = new Bow(game, 20, 30, 1, coordinates, "bow", "bowDrawn");
            p.Weapon = bow;

            //the check calls another method so im testing for what that other method does
            p.Check();

            Assert.IsTrue(game.GameState);

            p.DecreaseHealth(10);

            p.Check();

            Assert.IsFalse(game.GameState);
        }

        [Test]
        public void DestroyTestPlayer()
        {
            Game game = new Game();
            SplashKit.LoadBitmap("player", "sprites/player.png");
            Point2D coordinates;
            coordinates.X = 10;
            coordinates.Y = 10;
            Player p = new Player(game, 10, coordinates, "player");
            Bow bow = new Bow(game, 20, 30, 1, coordinates, "bow", "bowDrawn");
            p.Weapon = bow;

            //the check calls another method so im testing for what that other method does
            p.Check();

            Assert.IsTrue(game.GameState);

            p.DecreaseHealth(10);

            p.Check();

            Assert.IsFalse(game.GameState);
        }

        [Test]
        public void DestroyTestArcher()
        {
            Game game = new Game();
            SplashKit.LoadBitmap("archer", "sprites/archer.png");
            Point2D coordinates;
            coordinates.X = 10;
            coordinates.Y = 10;
            Archer archer = new Archer(game, 100, 1, 10, coordinates, "archer");

            Assert.IsTrue(archer.IsExist);

            archer.DecreaseHealth(10);

            archer.Destroy();

            Assert.IsFalse(archer.IsExist);
        }
    }
}