using QFramework;

namespace daifuDemo
{
    public class OpenOrCloseCustomerOrderPanelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var businessModel = this.GetModel<IBusinessModel>();

            businessModel.IfCustomerOrderPanelShow.Value = !businessModel.IfCustomerOrderPanelShow.Value;
        }
    }
}