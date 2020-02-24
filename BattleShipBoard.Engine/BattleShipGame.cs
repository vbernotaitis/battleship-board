using System.Linq;
using BattleShipBoard.Interfaces;

namespace BattleShipBoard.Engine
{
    public class BattleShipGame
    {
        public BattleShipPlayer Player1 { get; set; }
        public BattleShipPlayer Player2 { get; set; }
        public BattleShipPlayer Attacker { get; set; }
        public BattleShipPlayer Defender { get; set; }
        public BattleShipPlayer Winner { get; set; }

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
            switch (Defender.BattleField[shot.X][shot.Y])
            {
                case FieldState.Empty:
                    Defender.BattleField[shot.X][shot.Y] = FieldState.Miss;
                    SwitchPlayer();
                    break;
                case FieldState.Ship:
                    Defender.BattleField[shot.X][shot.Y] = FieldState.Hit;
                    break;
            }

            Attacker.Shooter.RecordLastShot(shot, Defender.BattleField[shot.X][shot.Y]);

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
            return Defender.BattleField.SelectMany(x => x).All(x => x != FieldState.Ship);
        }
    }
}
