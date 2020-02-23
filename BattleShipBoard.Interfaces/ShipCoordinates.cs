using System;

namespace BattleShipBoard.Interfaces
{
    public class ShipCoordinates
    {
        public ShipCoordinates(int startX, int startY, int endX, int endY)
        {
            if (startX != endX && startY != endY)
            {
                throw new ArgumentException($"Incorrect ship coordinates ({startX}{startY}:{endX}{endY})");
            }

            Start = new Coordinates(startX, startY);
            End = new Coordinates(endX, endY);
        }

        public ShipCoordinates(char startX, int startY, char endX, int endY)
        {
            if (startX != endX && startY != endY)
            {
                throw new ArgumentException($"Incorrect ship coordinates ({startX}{startY}:{endX}{endY})");
            }

            Start = new Coordinates(startX, startY);
            End = new Coordinates(endX, endY);
        }

        public Coordinates Start { get; }
        public Coordinates End { get; }
    }
}
