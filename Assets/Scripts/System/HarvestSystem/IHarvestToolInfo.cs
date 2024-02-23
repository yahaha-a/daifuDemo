namespace daifuDemo
{
    public interface IHarvestToolInfo : IHarvestInfo
    {
        (string, int) ToolCount { get; }

        IHarvestToolInfo WithToolCount((string, int) seasoningCount);
    }
    
    public class HarvestToolInfo : HarvestInfo, IHarvestToolInfo
    {
        public (string, int) ToolCount { get; private set; }
        
        public IHarvestToolInfo WithToolCount((string, int) seasoningCount)
        {
            ToolCount = seasoningCount;
            ItemList.Add(ToolCount);
            return this;
        }
    }
}