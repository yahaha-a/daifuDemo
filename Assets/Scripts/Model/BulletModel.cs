using QFramework;

namespace daifuDemo
{
    public interface IBulletModel : IModel
    {
        BindableProperty<BulletType> CurrentBulletType { get; }
    }
    
    public class BulletModel : AbstractModel, IBulletModel
    {
        protected override void OnInit()
        {
            
        }


        public BindableProperty<BulletType> CurrentBulletType { get; } =
            new BindableProperty<BulletType>(BulletType.Normal);
    }
}