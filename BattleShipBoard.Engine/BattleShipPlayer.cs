﻿using System;
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
            BattleField.AddShips(ValidateShips(Shooter.PrepareShipsForNewBattle()));
        }

        private Ship[] ValidateShips(Ship[] ships)
        {
            if (ships.Length > 10)
            {
                throw new Exception($"Too many ships ({ships.Length}). Must be less than {10}.");
            }
            ValidateShipCountBySize(ships, 1, 4);
            ValidateShipCountBySize(ships, 2, 3);
            ValidateShipCountBySize(ships, 3, 2);
            ValidateShipCountBySize(ships, 4, 1);
            return ships;
        }

        private void ValidateShipCountBySize(Ship[] ships, int count, int size)
        {
            if (ships.Count(s => s.Size == size) > count)
            {
                throw new Exception($"Too many ships of size {size}. Must be less than {count}.");
            }
        }
    }
}
