using QFramework;

namespace daifuDemo
{
    public class OpenOrCloseTaskPanel : AbstractCommand
    {
        protected override void OnExecute()
        {
            var _uiGameGlobalPanelModel = this.GetModel<IUIGameGlobalPanelModel>();
            _uiGameGlobalPanelModel.IfTaskPanelShow.Value = !_uiGameGlobalPanelModel.IfTaskPanelShow.Value;
        }
    }
}