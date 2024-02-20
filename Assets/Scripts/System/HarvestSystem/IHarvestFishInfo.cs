using System.Collections.Generic;

namespace daifuDemo
{
    public interface IHarvestFishInfo
    {
        List<(string, int)> ItemList { get; }
    }

    public class HarvestFishInfo : IHarvestFishInfo
    {
        public List<(string, int)> ItemList { get; } = new List<(string, int)>();
    }
}