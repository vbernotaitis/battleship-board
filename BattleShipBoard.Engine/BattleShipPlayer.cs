using BattleShipBoard.Interfaces;

namespace BattleShipBoard.Engine
{
    public class BattleShipPlayer
    {
        public BattleShipPlayer(IBattleShipShooter shooter)
        {
            Shooter = shooter;
            Name = shooter.Name;
            BattleField = CoordinatesTransformer.ToMatrix(shooter.GetShipsCoordinates());
        }

        public string Name { get; }

        public IBattleShipShooter Shooter { get; }

        public FieldState[][] BattleField { get; }
    }
}
