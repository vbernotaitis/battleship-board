using System;
using System.Collections.Generic;
using BattleShipBoard.Interfaces;

namespace BattleShip.Shooter.Random
{
    public class ErroneousBattleShipShooter : IBattleShipShooter
    {
        private readonly ShipCoordinates[][] _maps = new[]
        {
            new[]
            {
                new ShipCoordinates('A', 1, 'A', 2)
            }
        };

        public string Name { get; set; }

        public ShipCoordinates[] GetShipsCoordinates()
        {
            return _maps[0];
        }

        public Coordinates Shoot()
        {
            return new Coordinates('A', 1);
        }

        public void Record(Coordinates coordinates, FieldState state)
        {
            throw new Exception("Unexpected error!");
        }
    }
}
