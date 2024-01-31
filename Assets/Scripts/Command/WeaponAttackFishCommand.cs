using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class WeaponAttackFishCommand : AbstractCommand
    {
        private float _damage;

        private GameObject _gameObject;

        public WeaponAttackFishCommand(float damage, GameObject gameObject)
        {
            _damage = damage;
            _gameObject = gameObject;
        }
        
        protected override void OnExecute()
        {
            Events.WeaponAttackFish?.Trigger(_damage, _gameObject);
        }
    }
}