using QFramework;

namespace daifuDemo
{
    public interface IFishMode : IModel
    {
        
    }
    
    public class FishModel : AbstractModel, IFishMode
    {
        protected override void OnInit()
        {
            
        }

    }
}