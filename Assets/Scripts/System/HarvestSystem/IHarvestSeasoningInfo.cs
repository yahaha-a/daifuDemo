namespace daifuDemo
{
    public interface IHarvestSeasoningInfo : IHarvestInfo
    {
        (string, int) SeasoningCount { get; }

        IHarvestSeasoningInfo WithSeasoningCount((string, int) seasoningCount);
    }
    
    public class HarvestSeasoningInfo : HarvestInfo, IHarvestSeasoningInfo
    {
        public (string, int) SeasoningCount { get; private set; }
        
        public IHarvestSeasoningInfo WithSeasoningCount((string, int) seasoningCount)
        {
            SeasoningCount = seasoningCount;
            ItemList.Add(SeasoningCount);
            return this;
        }
    }
}