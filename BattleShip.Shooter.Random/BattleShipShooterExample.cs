﻿using BattleShipBoard.Interfaces;

namespace BattleShip.Shooter.Random
{
    public class BattleShipShooterExample : IBattleShipShooter
    {
        public string CaptainName { get; set; } = "John";

        public Ship[] PrepareShipsForNewBattle()
        {
            return new[]
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
        }

        public Coordinates Shoot()
        {
            var random = new System.Random();
            var validCoordinatesExamples = new[]
            {
                // Coordinates can be defined by two formats,
                // under the hood everything is operated in integers from 0 to 9
                new Coordinates(0, 0),
                new Coordinates('J', 1),
                new Coordinates('A', 10),
                new Coordinates(9, 9)
            };
            return validCoordinatesExamples[random.Next(0, validCoordinatesExamples.Length)];
        }

        public void ReportLastShotResult(Coordinates coordinates, ShotResult result)
        {
        }

        public void ReportOponentsLastShotResult(Coordinates coordinates, ShotResult result)
        {
        }
    }
}
