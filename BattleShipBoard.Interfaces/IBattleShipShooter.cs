namespace BattleShipBoard.Interfaces
{
    public interface IBattleShipShooter
    {
        string CaptainName { get; set; }

        Ship[] GetShips();

        Coordinates Shoot();

        void RecordLastShot(Coordinates coordinates, ShotResult result);
    }
}
