using QFramework;

namespace daifuDemo
{
    public interface IFishMode : IModel
    {
        string NormalFishKey { get; }
    }
    
    public class FishModel : AbstractModel, IFishMode
    {
        protected override void OnInit()
        {
            
        }

        public string NormalFishKey { get; } = "normal_fish";
    }
}