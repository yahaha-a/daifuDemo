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
        
        FishForkState CurrentFishForkState { get; set; }
        
        bool FishForkIfShooting { get; set; }
    }
    
    public class FishForkModel : AbstractModel, IFishForkModel
    {
        protected override void OnInit()
        {
            Events.FishForkHeadDestroy.Register(() =>
            {
                FishForkIfShooting = false;
            });
        }

        public BindableProperty<string> CurrentFishForkKey { get; } = new BindableProperty<string>(Config.FishForkKey);

        public BindableProperty<int> CurrentRank { get; } = new BindableProperty<int>(1);
        public FishForkState CurrentFishForkState { get; set; } = FishForkState.Ready;
        
        public bool FishForkIfShooting { get; set; } = false;
    }
}