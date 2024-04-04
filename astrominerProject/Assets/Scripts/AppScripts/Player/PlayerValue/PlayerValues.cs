using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class PlayerValues
    {
        public MinMax MinMax { get; set; }
        public IReadOnlyDictionary<Player, PlayerValue> Values => _values;

        private Dictionary<Player, PlayerValue> _values = new Dictionary<Player, PlayerValue>();

        public void AddNewValueFor(Player player)
        {
            _values.Add(player, new PlayerValue());
        }
    }
}
