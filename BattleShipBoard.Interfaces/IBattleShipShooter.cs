namespace BattleShipBoard.Interfaces
{
    public interface IBattleShipShooter
    {
        string Name { get; set; }

        ShipCoordinates[] GetShipsCoordinates();

        Coordinates Shoot();

        void Record(Coordinates coordinates, FieldState state);
    }
}
