using BattleShipBoard.Engine;
using BattleShipBoard.Interfaces;
using NUnit.Framework;

namespace BattleShipBoard.Tests
{
    public class CordinatesTransformerTests
    {
        [Test]
        public void Test()
        {
            var coordinates = new ShipCoordinates[0];
            var result = CoordinatesTransformer.ToMatrix(coordinates);

        }
    }
}
