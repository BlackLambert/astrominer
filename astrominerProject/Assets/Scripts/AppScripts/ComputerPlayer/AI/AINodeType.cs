namespace SBaier.Astrominer
{
    public enum AINodeType
    {
        ActionSet = 0,
        
        // Motivations
        IdentifyAsteroid = 100,
        OccupyAsteroid = 101,
        EarnMoney = 102,
        ReduceCosts = 103,
        IncreaseOreOutput = 104,
        
        // Action
        SendProspectorDrone = 2000,
        FlyToUnidentifiedAsteroid = 2001,
        SellOres = 2002,
        CollectOres = 2003,
        
        FlyToRandomTarget = 2999,
    }
}