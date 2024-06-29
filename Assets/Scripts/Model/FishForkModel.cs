using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFishForkModel : IModel
    {
        BindableProperty<string> CurrentFishForkKey { get; }
        
        BindableProperty<int> CurrentRank { get; }
        
        BindableProperty<FishForkState> CurrentFishForkState { get; set; }
        
        BindableProperty<bool> IfFishForkHeadExist { get; }
    }
    
    public class FishForkModel : AbstractModel, IFishForkModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<string> CurrentFishForkKey { get; } = new BindableProperty<string>(Config.FishForkKey);

        public BindableProperty<int> CurrentRank { get; } = new BindableProperty<int>(1);

        public BindableProperty<FishForkState> CurrentFishForkState { get; set; } =
            new BindableProperty<FishForkState>(FishForkState.Ready);

        public BindableProperty<bool> IfFishForkHeadExist { get; } = new BindableProperty<bool>(false);
    }
}