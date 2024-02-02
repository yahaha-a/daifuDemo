using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class PlayerIsHitCommand : AbstractCommand
    {
        private float _damage;

        public PlayerIsHitCommand(float damage)
        {
            _damage = damage;
        }
        
        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.PlayerOxygen.Value -= _damage;
            Events.PlayerIsHit?.Trigger();
        }
    }
}