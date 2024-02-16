using System.Collections.Generic;

namespace daifuDemo
{
    public interface IHarvestNormalFishInfo : IHarvestFishInfo
    {
        
        Dictionary<string, int> NormalFishPieces { get; }

        IHarvestNormalFishInfo WithNormalFishPieces((string, int) normalFishPieces);
    }

    public class HarvestNormalFishInfo : HarvestFishInfo, IHarvestNormalFishInfo
    {
        public Dictionary<string, int> NormalFishPieces { get; private set; } = new Dictionary<string, int>();
        
        public IHarvestNormalFishInfo WithNormalFishPieces((string, int) normalFishPieces)
        {
            NormalFishPieces.Add(normalFishPieces.Item1, normalFishPieces.Item2);
            ItemList.Add(NormalFishPieces);
            return this;
        }
    }
}