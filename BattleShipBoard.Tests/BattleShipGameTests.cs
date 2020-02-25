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

            Assert.AreEqual(game.Attacker.Shooter, player1.Object);
            var shot = game.Shoot();
            Assert.AreEqual(game.Attacker.Shooter, player2.Object);
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

            Assert.AreEqual(game.Attacker.Shooter, player1.Object);
            var shot = game.Shoot();
            Assert.AreEqual(game.Attacker.Shooter, player1.Object);
        }

        [Test]
        public void ThrowsErrorWhenShotIsOutOfRange()
        {
            var player1 = new Mock<IBattleShipShooter>();
            player1.Setup(x => x.Shoot()).Returns(() => new Coordinates('W',3));

            var game = new BattleShipGame(player1.Object, player1.Object);

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

            Assert.Throws<Exception>(() => new BattleShipGame(player1.Object, player1.Object));
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

            player2.Setup(x => x.PrepareShipsForNewBattle()).Returns(ships.Where(s => s.Size == shipSize).ToArray);
            player1.Setup(x => x.Shoot()).Returns(coordinates);

            var game = new BattleShipGame(player1.Object, player2.Object);
            game.Shoot();

            player1.Verify(x => x.ReportLastShotResult(coordinates, expectedShotResult));
        }
    }
}