using System.Collections.Generic;
using BattleShipBoard.Interfaces;

namespace BattleShip.Shooter.Random
{
    public class BattleShipShooter : IBattleShipShooter
    {
        private readonly List<Coordinates> _availableShots = GetAvailableShots();

        private readonly ShipCoordinates[][] _maps = new ShipCoordinates[][]
        {
            new[]
            {
                new ShipCoordinates('A', 1, 'C', 1),
                new ShipCoordinates('G', 2, 'H', 2), 
                new ShipCoordinates('J', 3, 'J', 3), 
                new ShipCoordinates('B', 4, 'D', 4), 
                new ShipCoordinates('F', 6, 'F', 6), 
                new ShipCoordinates('H', 6, 'I', 6), 
                new ShipCoordinates('D', 7, 'D', 10), 
                new ShipCoordinates('A', 8, 'A', 8), 
                new ShipCoordinates('F', 10, 'G', 10), 
                new ShipCoordinates('J', 9, 'J', 9) 
            },
            new[]
            {
                new ShipCoordinates('F', 1, 'F', 1),
                new ShipCoordinates('H', 1, 'I', 1), 
                new ShipCoordinates('C', 2, 'C', 3), 
                new ShipCoordinates('B', 5, 'B', 5), 
                new ShipCoordinates('D', 5, 'D', 5), 
                new ShipCoordinates('F', 4, 'F', 7), 
                new ShipCoordinates('B', 7, 'B', 9), 
                new ShipCoordinates('D', 8, 'D', 8), 
                new ShipCoordinates('G', 9, 'I', 9), 
                new ShipCoordinates('I', 4, 'I', 5) 
            }
        };

        public string Name { get; set; } = "Vilimantas";

        public ShipCoordinates[] GetShipsCoordinates()
        {
            var random = new System.Random();
            return _maps[random.Next(0, _maps.Length)];
        }

        public Coordinates Shoot()
        {
            var random = new System.Random();
            var index = random.Next(0, _availableShots.Count);
            var shotCordinates = _availableShots[index];
            _availableShots.RemoveAt(index);
            return shotCordinates;
        }

        public void Record(Coordinates coordinates, FieldState state)
        {

        }

        private static List<Coordinates> GetAvailableShots()
        {
            var result = new List<Coordinates>();
            for (var x = 0; x <= 9; x++)
            {
                for (var y = 0; y <= 9; y++)
                {
                    result.Add(new Coordinates(x, y));
                }
            }
            return result;
        }
    }
}
