using QFramework;

namespace daifuDemo
{
    public interface IWeaponItemTempleteInfo
    {
        string Key { get; }
        
        string Name { get; }
        
        BindableProperty<EquipWeaponKey> EquipState { get; }
        
        IBulletInfo CurrentBulletInfo { get; }
        
        IWeaponItemTempleteInfo WithKey(string key);

        IWeaponItemTempleteInfo WithName(string name);

        IWeaponItemTempleteInfo WithEquipState(EquipWeaponKey state);

        IWeaponItemTempleteInfo WithBullet(IBulletInfo info);
    }

    public class WeaponItemTempleteInfo : IWeaponItemTempleteInfo
    {
        public string Key { get; private set; }
        
        public string Name { get; private set; }

        public BindableProperty<EquipWeaponKey> EquipState { get; private set; } =
            new BindableProperty<EquipWeaponKey>(EquipWeaponKey.Null);

        public IBulletInfo CurrentBulletInfo { get; private set; }

        public IWeaponItemTempleteInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IWeaponItemTempleteInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IWeaponItemTempleteInfo WithEquipState(EquipWeaponKey state)
        {
            EquipState.Value = state;
            return this;
        }

        public IWeaponItemTempleteInfo WithBullet(IBulletInfo info)
        {
            CurrentBulletInfo = info;
            return this;
        }
    }
}