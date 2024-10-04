using Global;
using QFramework;

namespace daifuDemo
{
    public interface IGunModel : IModel
    {
        BindableProperty<string> CurrentGunKey { get; }
        
        BindableProperty<int> CurrentGunRank { get; }
        
        BindableProperty<GunState> CurrentGunState { get; }
    }
    
    public class GunModel : AbstractModel, IGunModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<string> CurrentGunKey { get; } = new BindableProperty<string>(Config.RifleKey);
        
        public BindableProperty<int> CurrentGunRank { get; } = new BindableProperty<int>(1);

        public BindableProperty<GunState> CurrentGunState { get; } = new BindableProperty<GunState>(GunState.Ready);
    }
}