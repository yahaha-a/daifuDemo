namespace Global
{
    //TODO
    public enum CreateItemName
    {
        Null,
        
        Clod,
        
        Dave,
        
        NormalFish,
        AggressiveFish,
        
        ToolTreasureChest,
        SpiceTreasureChest,
        
        Kelp,
        Coral,
        Copper,
        
        Cordage,
        Wood
    }
    
    public class Config
    {
        public static float PlayerWalkingRate = 6.0f;

        public static int NumberOfFish = 0;

        public static int PlayerOxygen = 100;

        public static float OxygenIntervalTime = 3f;

        public static float PlayerInvincibleTime = 3f;
        
        
        public static string NormalFishKey = "NormalFish";

        public static string AggressiveFishKey = "AggressiveFish";

        
        public static string ToolTreasureChestKey = "ToolTreasureChest";
        
        public static string SpiceTreasureChestKey = "SpiceTreasureChest";
        
        
        public static string CopperOreKey = "Copper";

        public static string KelpPlantsKey = "Kelp";

        public static string CoralPlantsKey = "Coral";
        

        public static string FishForkKey = "fishfork_weapon";

        public static string DaggerKey = "dagger_weapon";

        public static string RifleKey = "rifle_weapon";

        public static string ShotgunKey = "shotgun_weapon";
        

        public static string CustomerKey = "customer_key";
    }
}