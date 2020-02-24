namespace BattleShipBoard.Interfaces
{
    public interface IBattleShipShooter
    {
        string Name { get; set; }

        Ship[] GetShips();

        Coordinates Shoot();

        void RecordLastShot(Coordinates coordinates, FieldState state);
    }
}
