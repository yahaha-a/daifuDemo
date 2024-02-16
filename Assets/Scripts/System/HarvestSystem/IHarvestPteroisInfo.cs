using System.Collections.Generic;

namespace daifuDemo
{
    public interface IHarvestPteroisInfo : IHarvestFishInfo
    {
        Dictionary<string, int> PteroisPieces { get; }

        IHarvestPteroisInfo WithPteroisPieces((string, int) pteroisPieces);
    }

    public class HarvestPteroisInfo : HarvestFishInfo, IHarvestPteroisInfo
    {
        public Dictionary<string, int> PteroisPieces { get; private set; } = new Dictionary<string, int>();
        
        public IHarvestPteroisInfo WithPteroisPieces((string, int) pteroisPieces)
        {
            PteroisPieces.Add(pteroisPieces.Item1, pteroisPieces.Item2);
            ItemList.Add(PteroisPieces);
            return this;
        }
    }
}