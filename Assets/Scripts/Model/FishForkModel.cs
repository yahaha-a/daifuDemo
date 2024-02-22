using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum FishForkState
    {
        Ready,
        Aim,
        Revolve,
        Launch
    }
    
    public interface IFishForkModel : IModel
    {
        BindableProperty<string> CurrentFishForkKey { get; }
        
        BindableProperty<int> CurrentRank { get; }
        
        BindableProperty<FishForkState> CurrentFishForkState { get; set; }
        
        BindableProperty<bool> FishForkIfShooting { get; set; }
    }
    
    public class FishForkModel : AbstractModel, IFishForkModel
    {
        protected override void OnInit()
        {
            Events.FishForkHeadDestroy.Register(() =>
            {
                FishForkIfShooting.Value = false;
            });
        }

        public BindableProperty<string> CurrentFishForkKey { get; } = new BindableProperty<string>(Config.FishForkKey);

        public BindableProperty<int> CurrentRank { get; } = new BindableProperty<int>(1);

        public BindableProperty<FishForkState> CurrentFishForkState { get; set; } =
            new BindableProperty<FishForkState>(FishForkState.Ready);

        public BindableProperty<bool> FishForkIfShooting { get; set; } = new BindableProperty<bool>(false);
    }
}