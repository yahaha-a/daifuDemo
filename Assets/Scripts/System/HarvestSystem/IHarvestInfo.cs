using System.Collections.Generic;

namespace daifuDemo
{
    public interface IHarvestInfo
    {
        List<(string, int)> ItemList { get; }
    }

    public class HarvestInfo : IHarvestInfo
    {
        public List<(string, int)> ItemList { get; } = new List<(string, int)>();
    }
}