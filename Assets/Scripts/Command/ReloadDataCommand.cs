using QFramework;

namespace daifuDemo
{
    public class ReloadDataCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playModel = this.GetModel<IPlayerModel>();
            var fishForkModel = this.GetModel<IFishForkModel>();
            var fishSystem = this.GetSystem<IFishSystem>();
            var harvestSystem = this.GetSystem<IHarvestSystem>();
            
            playModel.State.Value = PlayState.Swim;
            playModel.NumberOfFish.Value = Config.NumberOfFish;
            playModel.PlayerOxygen.Value = Config.PlayerOxygen;

            fishForkModel.CurrentFishForkState = FishForkState.Ready;
            fishForkModel.FishForkIfShooting = false;
            
            fishSystem.Reload();
            harvestSystem.Reload();
        }
    }
}