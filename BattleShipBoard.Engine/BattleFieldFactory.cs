using System.Linq;

namespace BattleShipBoard.Engine
{
    public class BattleFieldFactory
    {
        public static Field[][] CreateEmpty()
        {
            var range = Enumerable.Repeat(1, 10).ToArray();
            var battleField = range
                .Select(x => range.Select(b => new Field { State = FieldState.Empty }).ToArray())
                .ToArray();
            return battleField;
        }
    }
}
