using System.Collections.Generic;

namespace daifuDemo
{
    public interface IHarvestNormalFishInfo : IHarvestFishInfo
    {
        
        (string, int) NormalFishPieces { get; }

        IHarvestNormalFishInfo WithNormalFishPieces((string, int) normalFishPieces);
    }

    public class HarvestNormalFishInfo : HarvestFishInfo, IHarvestNormalFishInfo
    {
        public (string, int) NormalFishPieces { get; private set; }
        
        public IHarvestNormalFishInfo WithNormalFishPieces((string, int) normalFishPieces)
        {
            NormalFishPieces = normalFishPieces;
            ItemList.Add(NormalFishPieces);
            return this;
        }
    }
}