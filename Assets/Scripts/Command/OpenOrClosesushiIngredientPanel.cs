using System.Windows.Input;
using QFramework;

namespace daifuDemo
{
    public class OpenOrClosesushiIngredientPanel : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGameShipPanelModel = this.GetModel<IUIGamesushiPanelModel>();

            uiGameShipPanelModel.IfUIsushiIngredientPanelOpen.Value = !uiGameShipPanelModel.IfUIsushiIngredientPanelOpen.Value;
        }
    }
}