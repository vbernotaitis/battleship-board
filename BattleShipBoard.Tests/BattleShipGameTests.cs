using System;
using System.Drawing;
using System.Linq;
using BattleShipBoard.Engine;
using BattleShipBoard.Interfaces;
using Moq;
using NUnit.Framework;

namespace BattleShipBoard.Tests
{
    public partial class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitiateNewGame()
        {
            var player1 = new Mock<IBattleShipShooter>();
            var game = new BattleShipGame(player1.Object, player1.Object);
            game.StarNewGame();

            Assert.IsNotNull(game);
        }

        [Test]
        public void SwitchesAttackerAfterMiss()
        {
            var player1 = new Mock<IBattleShipShooter>();
            var player2 = new Mock<IBattleShipShooter>();
            var ships = new[]{ new Ship('A', 1, 'A', 1) };
            var coordinates = new Coordinates('B',2);

            player1.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships);
            player2.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships);
            player1.Setup(x => x.Shoot()).Returns(coordinates);
            player2.Setup(x => x.Shoot()).Returns(coordinates);

            var game = new BattleShipGame(player1.Object, player2.Object);
            game.StarNewGame();

            var attackerBefore = game.Attacker.Shooter;
            game.Shoot();
            Assert.AreNotEqual(attackerBefore, game.Attacker.Shooter);
        }

        [Test]
        public void KeepsAttackerAfterHit()
        {
            var player1 = new Mock<IBattleShipShooter>();
            var player2 = new Mock<IBattleShipShooter>();
            var ships = new[]{ new Ship('A', 1, 'A', 1) };
            var coordinates = new Coordinates('A',1);

            player1.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships);
            player2.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships);
            player1.Setup(x => x.Shoot()).Returns(coordinates);
            player2.Setup(x => x.Shoot()).Returns(coordinates);

            var game = new BattleShipGame(player1.Object, player2.Object);
            game.StarNewGame();

            var attackerBefore = game.Attacker.Shooter;
            game.Shoot();
            Assert.AreEqual(attackerBefore, game.Attacker.Shooter);
        }

        [Test]
        public void ThrowsErrorWhenShotIsOutOfRange()
        {
            var player1 = new Mock<IBattleShipShooter>();
            player1.Setup(x => x.Shoot()).Returns(() => new Coordinates('W',3));

            var game = new BattleShipGame(player1.Object, player1.Object);
            game.StarNewGame();

            Assert.Throws<ArgumentException>(() => game.Shoot());
        }

        [TestCase(2, 4)]
        [TestCase(3, 3)]
        [TestCase(4, 2)]
        [TestCase(5, 1)]
        public void ThrowsErrorWhenTooMuchShipsBySize(int count, int size)
        {
            var player1 = new Mock<IBattleShipShooter>();
            var ships = new[]
            {
                new Ship('A', 1, 'A', size),
                new Ship('B', 1, 'B', size),
                new Ship('C', 1, 'C', size),
                new Ship('D', 1, 'D', size),
                new Ship('F', 1, 'F', size)
            };

            player1.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships.Take(count).ToArray);
            var game = new BattleShipGame(player1.Object, player1.Object);

            Assert.Throws<Exception>(() => game.StarNewGame());
        }

        [TestCase(1, ShotResult.Destroyed)]
        [TestCase(2, ShotResult.Hit)]
        public void RecordsLastShotCorrectly(int shipSize, ShotResult expectedShotResult)
        {
            var player1 = new Mock<IBattleShipShooter>();
            var player2 = new Mock<IBattleShipShooter>();
            var ships = new[]
            {
                new Ship('A', 1, 'A', 1),
                new Ship('A', 1, 'A', 2),
            };
            var coordinates = new Coordinates('A',1);

            player1.Setup(x => x.Shoot()).Returns(coordinates);
            player2.Setup(x => x.Shoot()).Returns(coordinates);
            player1.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships.Where(s => s.Size == shipSize).ToArray);
            player2.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships.Where(s => s.Size == shipSize).ToArray);

            var game = new BattleShipGame(player1.Object, player2.Object);
            game.StarNewGame();
            game.Shoot();

            if (game.Attacker.Shooter == player1.Object)
            {
                player1.Verify(x => x.ReportLastShotResult(coordinates, expectedShotResult));
                player2.Verify(x => x.ReportOponentsLastShotResult(coordinates, expectedShotResult));
            }
            else
            {
                player2.Verify(x => x.ReportLastShotResult(coordinates, expectedShotResult));
                player1.Verify(x => x.ReportOponentsLastShotResult(coordinates, expectedShotResult));
            }
        }

        [TestCase(0, 0, 0, 0, 1, 1, 1, 1)]
        [TestCase(1, 1, 1, 3, 1, 4, 2, 4)]
        [TestCase(1, 1, 2, 1, 3, 1, 3, 2)]

        public void ThrowsAnExceptionWhenShipsAreTooCloseToEachOther(int startX1, int startY1, int endX1, int endY1, int startX2, int startY2, int endX2, int endY2)
        {
            var player1 = new Mock<IBattleShipShooter>();
            var ships = new[]
            {
                new Ship(startX1, startY1, endX1, endY1),
                new Ship(startX2, startY2, endX2, endY2)
            };

            player1.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships);

            var game = new BattleShipGame(player1.Object, player1.Object);

            Assert.That(() => game.StarNewGame(),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo(", ships are too close to each other!"));
        }

        [Test]
        public void DoesntThrowAnExceptionWhenShipsPlacementIsValid()
        {
            var player1 = new Mock<IBattleShipShooter>();
            var ships = new[]
            {
                new Ship('A', 1, 'C', 1),
                new Ship('G', 2, 'H', 2),
                new Ship('J', 3, 'J', 3),
                new Ship('B', 4, 'D', 4),
                new Ship('F', 6, 'F', 6),
                new Ship('H', 6, 'I', 6),
                new Ship('D', 7, 'D', 10),
                new Ship('A', 8, 'A', 8),
                new Ship('F', 10, 'G', 10),
                new Ship('J', 9, 'J', 9)
            };

            player1.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships);

            var game = new BattleShipGame(player1.Object, player1.Object);

            Assert.That(() => game.StarNewGame(), Throws.Nothing);
        }
    }
}