using System;
using System.Linq;
using BattleShipBoard.Interfaces;

namespace BattleShipBoard.Engine
{
    public class BattleShipGame
    {
        public BattleShipPlayer Player1 { get; }
        public BattleShipPlayer Player2 { get; }
        public BattleShipPlayer Attacker { get; private set; }
        public BattleShipPlayer Defender { get; private set; }
        public BattleShipPlayer Winner { get; private set; }

        public BattleShipGame(IBattleShipShooter shooter1, IBattleShipShooter shooter2)
        {
            Player1 = new BattleShipPlayer(shooter1);
            Player2 = new BattleShipPlayer(shooter2);

            Attacker = Player1;
            Defender = Player2;
        }

        public Coordinates Shoot()
        {
            var shot = Attacker.Shooter.Shoot();
            var field = Defender.BattleField[shot.Y][shot.X];

            switch (field.State)
            {
                case FieldState.Empty:
                    field.State = FieldState.Miss;
                    Attacker.Shooter.ReportLastShotResult(shot, ShotResult.Missed);
                    Defender.Shooter.ReportOponentsLastShotResult(shot, ShotResult.Missed);
                    SwitchPlayer();
                    break;
                case FieldState.Ship:
                    field.State = FieldState.Hit;
                    var shotResult = field.RelatedFields.Any(f => f.State == FieldState.Ship)
                        ? ShotResult.Hit
                        : ShotResult.Destroyed;
                    Attacker.Shooter.ReportLastShotResult(shot, shotResult);
                    Defender.Shooter.ReportOponentsLastShotResult(shot, shotResult);
                    break;
            }

            if (AreAllShipsDestroyed())
            {
                Winner = Attacker;
            }

            return shot;
        }

        private void SwitchPlayer()
        {
            Attacker = Attacker == Player1 ? Player2 : Player1;
            Defender = Defender == Player1 ? Player2 : Player1;
        }

        private bool AreAllShipsDestroyed()
        {
            return Defender.BattleField.SelectMany(x => x).All(x => x.State != FieldState.Ship);
        }
    }
}
