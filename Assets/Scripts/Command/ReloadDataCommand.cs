using Global;
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
            var mapCreateSystem = this.GetSystem<IMapCreateSystem>();
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            
            playModel.CurrentState.Value = PlayState.Swim;
            playModel.NumberOfFish.Value = Config.NumberOfFish;
            playModel.PlayerOxygen.Value = Config.PlayerOxygen;

            fishForkModel.CurrentFishForkState.Value = FishForkState.Ready;
            
            fishSystem.Reload();
            harvestSystem.Reload();
            mapCreateSystem.Reload();
            weaponSystem.Reload();
        }
    }
}