using QFramework;

namespace daifuDemo
{
    public interface IBulletModel : IModel
    {
        BindableProperty<BulletAttribute> CurrentBulletAttribute { get; }
        
        BindableProperty<int> CurrentBulletRank { get; }
    }
    
    public class BulletModel : AbstractModel, IBulletModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<BulletAttribute> CurrentBulletAttribute { get; } =
            new BindableProperty<BulletAttribute>(BulletAttribute.Normal);

        public BindableProperty<int> CurrentBulletRank { get; } = new BindableProperty<int>(1);
    }
}