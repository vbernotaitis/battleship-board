namespace BattleShipBoard.Interfaces
{
    public interface IBattleShipShooter
    {
        /// <summary>
        /// Name of the captain
        /// </summary>
        string CaptainName { get; set; }

        /// <summary>
        /// Generates all ships with their coordinates.
        /// Fleet should consist of 10 ships: 1 of size 4, 2 of size 3, 3 of size 2, 4 of size 1.
        /// </summary>
        /// <returns>Fleet of ships</returns>
        Ship[] PrepareShipsForNewBattle();

        /// <summary>
        /// Shoots to the oponents fleet by the determined coordinates.
        /// </summary>
        /// <returns>Coordinates of the shot</returns>
        Coordinates Shoot();

        /// <summary>
        /// Reports result of the last shot.
        /// </summary>
        /// <param name="coordinates">Coordinates of the last shot</param>
        /// <param name="result">Shot result</param>
        void ReportLastShotResult(Coordinates coordinates, ShotResult result);

        /// <summary>
        /// Reports result of the last oponent's shot.
        /// </summary>
        /// <param name="coordinates">Coordinates of the oponent's last shot</param>
        /// <param name="result">Shot result</param>
        void ReportOponentsLastShotResult(Coordinates coordinates, ShotResult result);
    }
}
