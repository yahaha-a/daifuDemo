using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IBusinessModel : IModel
    {
        BindableProperty<int> MaxCustomerNumber { get; }
        
        BindableProperty<int> CurrentCustomerNumber { get; }
        
        BindableProperty<ITableItemInfo> CurrentTouchTableItemInfo { get; }
        
        BindableProperty<bool> IfCustomerOrderPanelShow { get; }
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

        public BindableProperty<ITableItemInfo> CurrentTouchTableItemInfo { get; } =
            new BindableProperty<ITableItemInfo>();

        public BindableProperty<bool> IfCustomerOrderPanelShow { get; } = new BindableProperty<bool>(false);
    }
}