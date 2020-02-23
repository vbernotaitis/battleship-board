using System;
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
            var ships = new[]{ new ShipCoordinates('A', 1, 'A', 1) };
            var coordinates = new Coordinates('B',2);

            player1.Setup(x => x.GetShipsCoordinates()).Returns(ships);
            player2.Setup(x => x.GetShipsCoordinates()).Returns(ships);
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
            var ships = new[]{ new ShipCoordinates('A', 1, 'A', 1) };
            var coordinates = new Coordinates('A',1);

            player1.Setup(x => x.GetShipsCoordinates()).Returns(ships);
            player2.Setup(x => x.GetShipsCoordinates()).Returns(ships);
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
    }
}