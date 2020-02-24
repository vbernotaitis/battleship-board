using System;

namespace BattleShipBoard.Interfaces
{
    public class Ship
    {
        public Ship(int startX, int startY, int endX, int endY)
        {
            if (startX != endX && startY != endY)
            {
                throw new ArgumentException($"Incorrect ship coordinates ({startX}{startY}:{endX}{endY})");
            }

            Start = new Coordinates(startX, startY);
            End = new Coordinates(endX, endY);
            Size = CalculateSize();
        }

        public Ship(char startX, int startY, char endX, int endY)
        {
            
            if (startX != endX && startY != endY)
            {
                throw new ArgumentException($"Incorrect ship coordinates ({startX}{startY}:{endX}{endY})");
            }

            Start = new Coordinates(startX, startY);
            End = new Coordinates(endX, endY);
            Size = CalculateSize();
        }

        public Coordinates Start { get; }

        public Coordinates End { get; }

        public int Size { get; }

        private int CalculateSize()
        {
            return Math.Max(Math.Abs(End.X - Start.X) + 1, Math.Abs(End.Y - Start.Y) + 1);
        }
    }
}
