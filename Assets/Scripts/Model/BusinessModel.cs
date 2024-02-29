using QFramework;

namespace daifuDemo
{
    public interface IBusinessModel : IModel
    {
        BindableProperty<int> MaxCustomerNumber { get; }
        
        BindableProperty<int> CurrentCustomerNumber { get; }
    }
    
    public class BusinessModel : AbstractModel, IBusinessModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<int> MaxCustomerNumber { get; } =
            new BindableProperty<int>(CustomerConfig.MaxCustomerNumber);

        public BindableProperty<int> CurrentCustomerNumber { get; } =
            new BindableProperty<int>(0);
    }
}