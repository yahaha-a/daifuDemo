using QFramework;

namespace daifuDemo
{
    public interface ISingleUseItemsModel : IModel
    {
        BindableProperty<BulletType> RifleBulletType { get; }
        
        BindableProperty<BulletType> ShotgunBulletType { get; }
        
        BindableProperty<int> RifleBulletCount { get; }
        
        BindableProperty<int> ShotgunBulletCount { get; }
    }
    
    public class SingleUseItemsModel : AbstractModel, ISingleUseItemsModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<BulletType> RifleBulletType { get; } =
            new BindableProperty<BulletType>(BulletType.Normal);

        public BindableProperty<BulletType> ShotgunBulletType { get; } =
            new BindableProperty<BulletType>(BulletType.Normal);

        public BindableProperty<int> RifleBulletCount { get; } = new BindableProperty<int>(0);
        
        public BindableProperty<int> ShotgunBulletCount { get; } = new BindableProperty<int>(0);
    }
}