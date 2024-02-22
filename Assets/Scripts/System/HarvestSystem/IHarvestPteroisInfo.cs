using System.Collections.Generic;

namespace daifuDemo
{
    public interface IHarvestPteroisInfo : IHarvestInfo
    {
        (string, int) PteroisPieces { get; }

        IHarvestPteroisInfo WithPteroisPieces((string, int) pteroisPieces);
    }

    public class HarvestPteroisInfo : HarvestInfo, IHarvestPteroisInfo
    {
        public (string, int) PteroisPieces { get; private set; }
        
        public IHarvestPteroisInfo WithPteroisPieces((string, int) pteroisPieces)
        {
            PteroisPieces = pteroisPieces;
            ItemList.Add(PteroisPieces);
            return this;
        }
    }
}