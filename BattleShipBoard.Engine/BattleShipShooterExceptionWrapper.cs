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


        public ShipCoordinates[] GetShipsCoordinates()
        {
            try
            {
                return _originalShooter.GetShipsCoordinates();
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

        public void Record(Coordinates coordinates, FieldState state)
        {
            try
            {
                _originalShooter.Record(coordinates, state);
            }
            catch (Exception e)
            {
                throw new Exception($"{Name} failed. {e.Message}");
            }
        }
    }
}
