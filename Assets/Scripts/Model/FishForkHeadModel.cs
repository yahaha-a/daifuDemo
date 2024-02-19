using QFramework;

namespace daifuDemo
{
    public interface IFishForkHeadModel : IModel
    {
        int FishForkHeadDirection { get; set; }
    }
    
    public class FishForkHeadModel : AbstractModel, IFishForkHeadModel
    {
        protected override void OnInit()
        {
            
        }

        public int FishForkHeadDirection { get; set; }
    }
}