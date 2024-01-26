using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class FishCountAddOneCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playModel = this.GetModel<IPlayerModel>();
            playModel.NumberOfFish.Value += 1;
        }
    }
}