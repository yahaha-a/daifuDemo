using QFramework;

namespace daifuDemo
{
    public interface IGameGlobalModel : IModel
    {
        BindableProperty<IObtainItemsInfo> CurrentObtainItem { get; }
        
        BindableProperty<string> CurrentSelectArchiveName { get; }
        
        BindableProperty<string> CurrentArchiveName { get; }
    }
    
    public class GameGlobalModel : AbstractModel, IGameGlobalModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<IObtainItemsInfo> CurrentObtainItem { get; } =
            new BindableProperty<IObtainItemsInfo>(null);

        public BindableProperty<string> CurrentSelectArchiveName { get; } = new BindableProperty<string>(null);

        public BindableProperty<string> CurrentArchiveName { get; } = new BindableProperty<string>(null);
    }
}