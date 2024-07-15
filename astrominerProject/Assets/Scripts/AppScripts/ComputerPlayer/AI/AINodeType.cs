namespace SBaier.Astrominer
{
    public enum AINodeType
    {
        ActionSet = 0,
        
        // Motivations
        IdentifyAsteroid = 100,
        OccupyAsteroid = 101,
        EarnMoney = 102,
        TakeExploiter = 103,
        SellMachine = 104,
        IncreaseOreOutput = 105,
        
        // Action
        SendProspectorDrone = 2000,
        FlyToUnidentifiedAsteroid = 2001,
        SellOres = 2002,
        CollectOres = 2003,
        
        FlyToRandomTarget = 2999,
    }
}