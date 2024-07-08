namespace SBaier.Astrominer
{
    public enum AINodeType
    {
        ActionSet = 0,
        
        // Motivations
        IdentifyAsteroid = 100,
        OccupyAsteroid = 101,
        ReduceCosts = 102,
        GatherAndSellOres = 103,
        IncreaseOreOutput = 104,
        
        // Action
        SendProspectorDrone = 2000,
        FlyToUnidentifiedAsteroid = 2001,
        FlyToRandomTarget = 2999,
    }
}