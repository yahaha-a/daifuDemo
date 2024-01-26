using QFramework;

namespace daifuDemo
{
    public interface IPlayerModel : IModel
    {
        BindableProperty<int> NumberOfFish { get; }
    }
    
    public class PlayerModel : AbstractModel, IPlayerModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<int> NumberOfFish { get; } = new BindableProperty<int>(0);
    }
}