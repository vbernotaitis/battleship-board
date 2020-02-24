using System.Collections.Generic;
using BattleShipBoard.Interfaces;

namespace BattleShipBoard.Engine
{
    public class Field
    {
        public FieldState State { get; set; }

        public List<Field> RelatedFields { get; set; }
    }
}
