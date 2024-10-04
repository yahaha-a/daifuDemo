using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class PlayBreatheCommand : AbstractCommand

    {
        protected override void OnExecute()
        {
            var playModel = this.GetModel<IPlayerModel>();

            if (playModel.OxygenIntervalTime.Value <= 0f)
            {
                playModel.PlayerOxygen.Value -= 1f;
                playModel.OxygenIntervalTime.Value = Config.OxygenIntervalTime;
            }
            else
            {
                playModel.OxygenIntervalTime.Value -= Time.deltaTime;
            }
        }
    }
}