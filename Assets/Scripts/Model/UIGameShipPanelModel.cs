using QFramework;

namespace daifuDemo
{
    public enum EquipWeaponKey
    {
        Null,
        FishFork,
        MeleeWeapon,
        PrimaryWeapon,
        SecondaryWeapons
    }
    
    public interface IUIGameShipPanelModel : IModel
    {
        BindableProperty<bool> IfGoToHomePanelOpen { get; }
        
        BindableProperty<bool> IfGotoSeaPanelOpen { get; }
        
        BindableProperty<bool> IfShipUIPackOpen { get; }
        
        BindableProperty<bool> IfItemInfoShow { get; }
        
        BindableProperty<bool> IfSelectMapPanelShow { get; }
        
        BindableProperty<bool> IfEquipWeaponPanelOpen { get; }
        
        BindableProperty<string> CurrentSelectMapName { get; }
        
        BindableProperty<string> CurrentMapName { get; }
        
        BindableProperty<EquipWeaponKey> CurrentEquipWeaponKey { get; }
        
        BindableProperty<IWeaponItemTempleteInfo> CurrentSelectWeaponInfo { get; }
        
        BindableProperty<IWeaponItemTempleteInfo> CurrentEquipFishFork { get; }
        
        BindableProperty<IWeaponItemTempleteInfo> CurrentEquipMeleeWeapon { get; }
        
        BindableProperty<IWeaponItemTempleteInfo> CurrentEquipPrimaryWeapon { get; }
        
        BindableProperty<IWeaponItemTempleteInfo> CurrentEquipSecondaryWeapons { get; }
        
        BindableProperty<bool> IfCurrentSelectWeaponEquip { get; }
    }
    
    public class UIGameShipPanelModel : AbstractModel, IUIGameShipPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfGoToHomePanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfGotoSeaPanelOpen { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfShipUIPackOpen { get; set; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfItemInfoShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfSelectMapPanelShow { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfEquipWeaponPanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<string> CurrentSelectMapName { get; } = new BindableProperty<string>(null);

        public BindableProperty<string> CurrentMapName { get; } = new BindableProperty<string>(null);

        public BindableProperty<EquipWeaponKey> CurrentEquipWeaponKey { get; } =
            new BindableProperty<EquipWeaponKey>(EquipWeaponKey.Null);

        public BindableProperty<IWeaponItemTempleteInfo> CurrentSelectWeaponInfo { get; } =
            new BindableProperty<IWeaponItemTempleteInfo>(null);

        public BindableProperty<IWeaponItemTempleteInfo> CurrentEquipFishFork { get; } =
            new BindableProperty<IWeaponItemTempleteInfo>(null);

        public BindableProperty<IWeaponItemTempleteInfo> CurrentEquipMeleeWeapon { get; } =
            new BindableProperty<IWeaponItemTempleteInfo>(null);

        public BindableProperty<IWeaponItemTempleteInfo> CurrentEquipPrimaryWeapon { get; } =
            new BindableProperty<IWeaponItemTempleteInfo>(null);

        public BindableProperty<IWeaponItemTempleteInfo> CurrentEquipSecondaryWeapons { get; } =
            new BindableProperty<IWeaponItemTempleteInfo>(null);

        public BindableProperty<bool> IfCurrentSelectWeaponEquip { get; } = new BindableProperty<bool>(false);
    }
}