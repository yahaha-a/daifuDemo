using System.Collections.Generic;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IGunModel : IModel
    {
        BindableProperty<GunState> CurrentGunState { get; }
        
        BindableProperty<bool> IfLeft { get; }
    }
    
    public class GunModel : AbstractModel, IGunModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<GunState> CurrentGunState { get; } = new BindableProperty<GunState>(GunState.Ready);
        
        public BindableProperty<bool> IfLeft { get; } = new BindableProperty<bool>(false);
    }
}