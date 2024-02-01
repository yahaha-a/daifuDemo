using QFramework;

namespace daifuDemo
{
    public class ReloadDataCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playModel = this.GetModel<IPlayerModel>();
            var fishForkModel = this.GetModel<IFishForkModel>();
            playModel.State.Value = PlayState.Swim;
            playModel.NumberOfFish.Value = Config.NumberOfFish;
            playModel.PlayerOxygen.Value = Config.PlayerOxygen;

            fishForkModel.FishForkState = FishForkState.Ready;
            fishForkModel.FishForkIfShooting = false;
        }
    }
}