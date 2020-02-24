using System;
using System.Collections.Generic;
using BattleShipBoard.Interfaces;

namespace BattleShipBoard.Engine
{
    public static class BattleFieldExtensions
    {
        public static void AddShips(this Field[][] battleField, Ship[] ships)
        {
            foreach (var ship in ships)
            {
                var relatedFields = new List<Field>();
                for (var i = ship.Start.Y; i <= ship.End.Y; i++)
                {
                    var field = battleField[i][ship.Start.X];

                    if (field.State == FieldState.Ship)
                    {
                        throw new Exception($"Ship already exists on ({i}{ship.Start.X}) field");
                    }

                    field.State = FieldState.Ship;
                    field.RelatedFields = relatedFields;
                    field.RelatedFields.Add(field);
                }

                for (var i = ship.Start.X; i <= ship.End.X; i++)
                {
                    var field = battleField[ship.Start.Y][i];

                    if (ship.Start.X+1 == i && field.State == FieldState.Ship)
                    {
                        throw new Exception($"Ship already exists on ({i}{ship.Start.X}) field");
                    }

                    field.State = FieldState.Ship;
                    field.RelatedFields = relatedFields;
                    field.RelatedFields.Add(field);
                }
            }
        }
    }
}
