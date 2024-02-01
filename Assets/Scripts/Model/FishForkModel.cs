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
        float RotationRate { get; set; }
        
        FishForkState FishForkState { get; set; }
        
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

        public float RotationRate { get; set; } = 50f;

        public FishForkState FishForkState { get; set; } = FishForkState.Ready;
        
        public bool FishForkIfShooting { get; set; } = false;
    }
}