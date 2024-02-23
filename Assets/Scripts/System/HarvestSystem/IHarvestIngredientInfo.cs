namespace daifuDemo
{
    public interface IHarvestIngredientInfo : IHarvestInfo
    {
        (string, int) IngredientCount { get; }

        IHarvestIngredientInfo WithIngredientCount((string, int) seasoningCount);
    }
    
    public class HarvestIngredientInfo : HarvestInfo, IHarvestIngredientInfo
    {
        public (string, int) IngredientCount { get; private set; }
        
        public IHarvestIngredientInfo WithIngredientCount((string, int) ingredientCount)
        {
            IngredientCount = ingredientCount;
            ItemList.Add(IngredientCount);
            return this;
        }
    }
}