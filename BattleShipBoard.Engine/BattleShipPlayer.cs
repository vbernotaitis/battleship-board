using System;
using System.Collections.Generic;
using System.Linq;
using BattleShipBoard.Interfaces;

namespace BattleShipBoard.Engine
{
    public class BattleShipPlayer
    {
        public BattleShipPlayer(IBattleShipShooter shooter)
        {
            Shooter = shooter;
        }

        public IBattleShipShooter Shooter { get; }

        public Field[][] BattleField { get; private set; }

        public void PrepareForNewGame()
        {
            BattleField = BattleFieldFactory.CreateEmpty();
            PrepareShips();
        }

        private void PrepareShips()
        {
            var ships = Shooter.PrepareShipsForNewBattle();
            ValidateShips(ships);
            BattleField.AddShips(ships);
            ValidateShipsPlacement(ships);
        }

        private Ship[] ValidateShips(Ship[] ships)
        {
            if (ships.Length > 10)
            {
                throw new Exception($"{Shooter.CaptainName}, too many ships ({ships.Length}). Must be less than {10}.");
            }
            ValidateShipCountBySize(ships, 1, 4);
            ValidateShipCountBySize(ships, 2, 3);
            ValidateShipCountBySize(ships, 3, 2);
            ValidateShipCountBySize(ships, 4, 1);
            return ships;
        }

        private void ValidateShipsPlacement(Ship[] ships)
        {
            foreach (var ship in ships)
            {
                var fields = new List<Field>();
                if (ship.Start.X == ship.End.X) // Vertical
                {
                    fields.Add(SafelyGetField(ship.Start.Y-1, ship.Start.X));
                    for (var y = ship.Start.Y-1; y <= ship.End.Y+1; y++)
                    {
                        fields.Add(SafelyGetField(y,ship.Start.X-1));
                        fields.Add(SafelyGetField(y, ship.Start.X+1));
                    }
                    fields.Add(SafelyGetField(ship.End.Y+1, ship.End.X));
                }
                else // Horizontal
                {
                    fields.Add(SafelyGetField(ship.Start.Y, ship.Start.X-1));
                    for (var x = ship.Start.X-1; x <= ship.End.X+1; x++)
                    {
                        fields.Add(SafelyGetField(ship.Start.Y-1, x));
                        fields.Add(SafelyGetField(ship.Start.Y+1, x));
                    }
                    fields.Add(SafelyGetField(ship.End.Y, ship.End.X+1));
                }

                if (fields.Any(x => x != null && x.State == FieldState.Ship))
                {
                    throw new ArgumentException($"{Shooter.CaptainName}, ships are too close to each other!");
                }
            }
        }

        private Field SafelyGetField(int y, int x)
        {
            try
            {
                return BattleField[y][x];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void ValidateShipCountBySize(Ship[] ships, int count, int size)
        {
            if (ships.Count(s => s.Size == size) > count)
            {
                throw new Exception($"{Shooter.CaptainName}, too many ships of size {size}. Must be less than {count}.");
            }
        }
    }
}
