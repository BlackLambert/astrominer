namespace SBaier.Astrominer
{
    public interface OreCarrier : Flyable
    {
        Ores CollectedOres { get; }
        Player Player { get; }
    }
}