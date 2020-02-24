using System;
using System.Collections.Generic;
using BattleShipBoard.Interfaces;

namespace BattleShip.Shooter.Random
{
    public class ErroneousBattleShipShooter : IBattleShipShooter
    {
        private readonly Ship[][] _maps = new[]
        {
            new[]
            {
                new Ship('A', 1, 'A', 4),
                new Ship('B', 1, 'B', 4),
            }
        };

        public string CaptainName { get; set; }

        public Ship[] GetShips()
        {
            return _maps[0];
        }

        public Coordinates Shoot()
        {
            return new Coordinates('A', 1);
        }

        public void RecordLastShot(Coordinates coordinates, ShotResult state)
        {
            throw new Exception("Unexpected error!");
        }
    }
}
