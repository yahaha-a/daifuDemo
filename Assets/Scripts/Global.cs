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
            this.RegisterModel<IPlayerModel>(new PlayerModel());
        }
    }
}