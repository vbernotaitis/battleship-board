using BattleShipBoard.Interfaces;
using NUnit.Framework;

namespace BattleShipBoard.Tests
{
    public class CoordinatesTests
    {

        [TestCase('A', 1, "A1")]
        [TestCase('J', 10, "J10")]
        public void CoordinatesReturnsReadableString(char x, int y, string expectedResult)
        {
            var coordinates = new Coordinates(x, y);
            Assert.AreEqual(expectedResult, coordinates.ToString());
        }

        [TestCase(0, 0, "A1")]
        [TestCase(9, 9, "J10")]
        public void CoordinatesReturnsReadableString(int x, int y, string expectedResult)
        {
            var coordinates = new Coordinates(x, y);
            Assert.AreEqual(expectedResult, coordinates.ToString());
        }
    }
}
