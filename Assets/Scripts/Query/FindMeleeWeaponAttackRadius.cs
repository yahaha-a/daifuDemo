using QFramework;

namespace daifuDemo
{
    public class FindMeleeWeaponAttackRadius : AbstractQuery<float>
    {
        private string _meleeWeaponKey;

        private int _rank;
        
        public FindMeleeWeaponAttackRadius(string meleeWeaponKey, int rank)
        {
            _meleeWeaponKey = meleeWeaponKey;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var attackRadius = weaponSystem.MeleeWeaponInfos[_meleeWeaponKey][_rank].AttackRadius;
            return attackRadius;
        }
    }
}