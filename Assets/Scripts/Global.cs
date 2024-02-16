using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class Global : Architecture<Global>
    {
        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            ResKit.Init();
            UIKit.Root.SetResolution(1920, 1080, 1);
        }
        
        protected override void Init()
        {
            this.RegisterSystem<IFishSystem>(new FishSystem());
            this.RegisterSystem<IHarvestSystem>(new HarvestSystem());
            this.RegisterModel<IPlayerModel>(new PlayerModel());
            this.RegisterModel<IFishForkModel>(new FishForkModel());
            this.RegisterModel<IFishForkHeadModel>(new FishForkHeadModel());
            this.RegisterModel<IUIGamePanelModel>(new UIGamePanelModel());
        }
    }
}