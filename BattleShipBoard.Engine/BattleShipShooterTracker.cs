using System;
using BattleShipBoard.Interfaces;
using Newtonsoft.Json;

namespace BattleShipBoard.Engine
{
    public class BattleShipShooterTracker : IBattleShipShooter
    {
        private readonly IBattleShipShooter _originalShooter;

        public BattleShipShooterTracker(Type shooterType, string name = null)
        {
            try
            {
                _originalShooter = (IBattleShipShooter)Activator.CreateInstance(shooterType);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot initiate shooter {name ?? shooterType.FullName}! Error: {e.InnerException?.Message ?? e.Message}");
            }

            CaptainName = string.IsNullOrEmpty(name) ? _originalShooter.CaptainName : name;
            if (string.IsNullOrEmpty(CaptainName))
            {
                throw new Exception($"Shooter {shooterType.FullName} doesn't have a name!");
            }
        }

        public string CaptainName { get; set; }


        public Ship[] PrepareShipsForNewBattle()
        {
            try
            {
                var ships = _originalShooter.PrepareShipsForNewBattle();
                Console.WriteLine($"{CaptainName} {nameof(PrepareShipsForNewBattle)}: {JsonConvert.SerializeObject(ships)}");
                return ships;
            }
            catch (Exception e)
            {
                throw new Exception($"{CaptainName} failed. {e.Message}");
            }
            
        }

        public Coordinates Shoot()
        {
            try
            {
                var shot = _originalShooter.Shoot();
                Console.WriteLine($"{CaptainName} {nameof(Shoot)}: {JsonConvert.SerializeObject(shot)}");
                return shot;
            }
            catch (Exception e)
            {
                throw new Exception($"{CaptainName} failed. {e.Message}");
            }
        }

        public void ReportLastShotResult(Coordinates coordinates, ShotResult result)
        {
            try
            {
                _originalShooter.ReportLastShotResult(coordinates, result);
                Console.WriteLine($"{CaptainName} {nameof(ReportLastShotResult)}: {JsonConvert.SerializeObject(coordinates)} - {result}");
            }
            catch (Exception e)
            {
                throw new Exception($"{CaptainName} failed. {e.Message}");
            }
        }

        public void ReportOponentsLastShotResult(Coordinates coordinates, ShotResult result)
        {
            try
            {
                _originalShooter.ReportOponentsLastShotResult(coordinates, result);
                Console.WriteLine($"{CaptainName} {nameof(ReportOponentsLastShotResult)}: {JsonConvert.SerializeObject(coordinates)} - {result}");
            }
            catch (Exception e)
            {
                throw new Exception($"{CaptainName} failed. {e.Message}");
            }
        }
    }
}
