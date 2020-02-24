using System;
using BattleShipBoard.Interfaces;

namespace BattleShipBoard.Engine
{
    public class BattleShipShooterExceptionWrapper : IBattleShipShooter
    {
        private readonly IBattleShipShooter _originalShooter;

        public BattleShipShooterExceptionWrapper(Type shooterType, string name = null)
        {
            try
            {
                _originalShooter = (IBattleShipShooter)Activator.CreateInstance(shooterType);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot initiate shooter {name ?? shooterType.FullName}! Error: {e.InnerException?.Message ?? e.Message}");
            }

            Name = string.IsNullOrEmpty(name) ? _originalShooter.Name : name;
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception($"Shooter {shooterType.FullName} doesn't have a name!");
            }
        }

        public string Name { get; set; }


        public Ship[] GetShips()
        {
            try
            {
                return _originalShooter.GetShips();
            }
            catch (Exception e)
            {
                throw new Exception($"{Name} failed. {e.Message}");
            }
            
        }

        public Coordinates Shoot()
        {
            try
            {
                return _originalShooter.Shoot();
            }
            catch (Exception e)
            {
                throw new Exception($"{Name} failed. {e.Message}");
            }
        }

        public void RecordLastShot(Coordinates coordinates, FieldState state)
        {
            try
            {
                _originalShooter.RecordLastShot(coordinates, state);
            }
            catch (Exception e)
            {
                throw new Exception($"{Name} failed. {e.Message}");
            }
        }
    }
}
