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
            RegisterSystem<IFishSystem>(new FishSystem());
            RegisterSystem<IHarvestSystem>(new HarvestSystem());
            RegisterSystem<IWeaponSystem>(new WeaponSystem());
            RegisterSystem<IBulletSystem>(new BulletSystem());
            RegisterSystem<IBackPackSystem>(new BackPackSystem());
            RegisterModel<IPlayerModel>(new PlayerModel());
            RegisterModel<IFishForkModel>(new FishForkModel());
            RegisterModel<IFishForkHeadModel>(new FishForkHeadModel());
            RegisterModel<IUIGamePanelModel>(new UIGamePanelModel());
            RegisterModel<IGunModel>(new GunModel());
            RegisterModel<IBulletModel>(new BulletModel());
            RegisterModel<IMeleeWeaponModel>(new MeleeWeaponModel());
            RegisterModel<IUIGameShipPanelModel>(new UIGameShipPanelModel());
        }
    }
}