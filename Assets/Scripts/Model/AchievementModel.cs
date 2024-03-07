using QFramework;

namespace daifuDemo
{
    public interface IAchievementModel : IModel
    {
        int TotalSellNormalFishsushiAmount { get; set; }
    }
    
    public class AchievementModel : AbstractModel, IAchievementModel
    {
        protected override void OnInit()
        {
            
        }

        public int TotalSellNormalFishsushiAmount { get; set; }
    }
}