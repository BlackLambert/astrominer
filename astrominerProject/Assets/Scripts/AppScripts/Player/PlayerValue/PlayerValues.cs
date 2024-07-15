using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class PlayerValues
    {
        public MinMax MinMax { get; set; }
        public IReadOnlyDictionary<Player, PlayerValue> Values => _values;

        private readonly Dictionary<Player, PlayerValue> _values = new Dictionary<Player, PlayerValue>();
        private readonly int _bufferSize;

        public PlayerValues(int bufferSize)
        {
            _bufferSize = bufferSize;
        }

        public void AddNewValueFor(Player player)
        {
            _values.Add(player, new PlayerValue(_bufferSize));
        }
    }
}
