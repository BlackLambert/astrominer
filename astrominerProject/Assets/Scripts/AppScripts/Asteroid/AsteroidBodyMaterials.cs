namespace SBaier.Astrominer
{
    public class AsteroidBodyMaterials
    {
        public Ores Ores { get; private set; }
        public float Rocks { get; private set; }

        public float Total => Rocks + Ores.GetTotal();
        public float RocksPercentage => Rocks / Total;
        public float OresPercentage => 1 - RocksPercentage;

        public AsteroidBodyMaterials(Ores ores, float rocks)
		{
            Ores = ores;
            Rocks = rocks;
        }
    }
}
