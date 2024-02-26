using System.Collections.Generic;

namespace daifuDemo
{
    public interface IHarvestInfo
    {
        List<(string, int, int)> Count { get; }

        IHarvestInfo WithCount(List<(string, int, int)> count);
    }

    public class HarvestInfo : IHarvestInfo
    {
        public List<(string, int, int)> Count { get; private set; }
        
        public IHarvestInfo WithCount(List<(string, int, int)> count)
        {
            Count = count;
            return this;
        }
    }
}