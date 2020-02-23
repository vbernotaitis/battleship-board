using System.Linq;
using BattleShipBoard.Interfaces;

namespace BattleShipBoard.Engine
{
    public static class CoordinatesTransformer
    {
        public static FieldState[][] ToMatrix(ShipCoordinates[] shipCoordinates)
        {
            var range = Enumerable.Repeat(1, 10).ToArray();
            var matrix = range
                .Select(x => range.Select(b => FieldState.Empty).ToArray())
                .ToArray();

            foreach (var cordinates in shipCoordinates)
            {
                for (var i = cordinates.Start.Y; i <= cordinates.End.Y; i++)
                {
                    matrix[i][cordinates.Start.X] = FieldState.Ship;
                }

                for (var i = cordinates.Start.X; i <= cordinates.End.X; i++)
                {
                    matrix[cordinates.Start.Y][i] = FieldState.Ship;
                }
            }
            return matrix;
        }
    }
}
