using QFramework;

namespace daifuDemo
{
    public interface IFishForkHeadModel : IModel
    {
        float Speed { get; set; }

        float FishForkLength { get; set; }
        
        int FishForkHeadDirection { get; set; }
    }
    
    public class FishForkHeadModel : AbstractModel, IFishForkHeadModel
    {
        protected override void OnInit()
        {
            
        }

        public float Speed { get; set; } = 30f;

        public float FishForkLength { get; set; } = 10f;
        
        public int FishForkHeadDirection { get; set; }
    }
}