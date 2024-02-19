using QFramework;

namespace daifuDemo
{
    public interface IMeleeWeaponModel : IModel
    {
        BindableProperty<string> CurrentMeleeWeaponKey { get; }
        
        BindableProperty<int> CurrentMeleeWeaponRank { get; }
    }
    
    public class MeleeWeaponModel : AbstractModel, IMeleeWeaponModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<string> CurrentMeleeWeaponKey { get; } = new BindableProperty<string>(Config.DaggerKey);

        public BindableProperty<int> CurrentMeleeWeaponRank { get; } = new BindableProperty<int>(1);
    }
}