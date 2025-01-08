using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFishForkModel : IModel
    {
        BindableProperty<FishForkState> CurrentFishForkState { get; set; }
        
        BindableProperty<bool> IfFishForkHeadExist { get; }
    }
    
    public class FishForkModel : AbstractModel, IFishForkModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<FishForkState> CurrentFishForkState { get; set; } =
            new BindableProperty<FishForkState>(FishForkState.Ready);

        public BindableProperty<bool> IfFishForkHeadExist { get; } = new BindableProperty<bool>(false);
    }
}