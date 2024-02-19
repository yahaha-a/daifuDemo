using QFramework;

namespace daifuDemo
{
    public class FindMeleeWeaponAttackFrequency : AbstractQuery<float>
    {
        private string _meleeWeaponKey;

        private int _rank;
        
        public FindMeleeWeaponAttackFrequency(string meleeWeaponKey, int rank)
        {
            _meleeWeaponKey = meleeWeaponKey;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var attackFrequency = weaponSystem.MeleeWeaponInfos[_meleeWeaponKey][_rank].AttackFrequency;
            return attackFrequency;
        }
    }
}