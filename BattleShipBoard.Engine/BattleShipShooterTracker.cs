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


        public Ship[] GetShips()
        {
            try
            {
                var ships = _originalShooter.GetShips();
                Console.WriteLine($"{CaptainName}: {JsonConvert.SerializeObject(ships)}");
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
                Console.WriteLine($"{CaptainName}: {JsonConvert.SerializeObject(shot)}");
                return shot;
            }
            catch (Exception e)
            {
                throw new Exception($"{CaptainName} failed. {e.Message}");
            }
        }

        public void RecordLastShot(Coordinates coordinates, ShotResult state)
        {
            try
            {
                _originalShooter.RecordLastShot(coordinates, state);
                Console.WriteLine($"{CaptainName}: {JsonConvert.SerializeObject(coordinates)} - {state}");
            }
            catch (Exception e)
            {
                throw new Exception($"{CaptainName} failed. {e.Message}");
            }
        }
    }
}
