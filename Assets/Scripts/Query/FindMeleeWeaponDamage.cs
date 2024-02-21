using QFramework;

namespace daifuDemo
{
    public class FindMeleeWeaponDamage : AbstractQuery<float>
    {
        private string _meleeWeaponKey;

        private int _rank;
        
        public FindMeleeWeaponDamage(string meleeWeaponKey, int rank)
        {
            _meleeWeaponKey = meleeWeaponKey;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var damage = weaponSystem.MeleeWeaponInfos[(_meleeWeaponKey, _rank)].Damage;
            return damage;
        }
    }
}