using System.Collections.Generic;
using BattleShipBoard.Interfaces;

namespace BattleShip.Shooter.Random
{
    public class BattleShipShooter : IBattleShipShooter
    {
        private readonly List<Coordinates> _availableShots = GetAvailableShots();

        private readonly Ship[][] _maps = new Ship[][]
        {
            new[]
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
            },
            new[]
            {
                new Ship('F', 1, 'F', 1),
                new Ship('H', 1, 'I', 1), 
                new Ship('C', 2, 'C', 3), 
                new Ship('B', 5, 'B', 5), 
                new Ship('D', 5, 'D', 5), 
                new Ship('F', 4, 'F', 7), 
                new Ship('B', 7, 'B', 9), 
                new Ship('D', 8, 'D', 8), 
                new Ship('G', 9, 'I', 9), 
                new Ship('I', 4, 'I', 5) 
            }
        };

        public string CaptainName { get; set; } = "Vilimantas";

        public Ship[] GetShips()
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

        public void RecordLastShot(Coordinates coordinates, ShotResult result)
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
