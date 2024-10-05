using QFramework;

namespace daifuDemo
{
    public interface IGameStartModel : IModel
    {
        BindableProperty<string> CurrentSelectMapName { get; }
    }
    
    public class GameStartModel : AbstractModel, IGameStartModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<string> CurrentSelectMapName { get; } = new BindableProperty<string>(null);
    }
}