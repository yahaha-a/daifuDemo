using System.Collections.Generic;

namespace daifuDemo
{
    public interface IHarvestFishInfo
    {
        List<Dictionary<string, int>> ItemList { get; }
    }

    public class HarvestFishInfo : IHarvestFishInfo
    {
        public List<Dictionary<string, int>> ItemList { get; } = new List<Dictionary<string, int>>();
    }
}