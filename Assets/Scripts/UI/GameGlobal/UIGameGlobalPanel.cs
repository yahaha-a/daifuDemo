using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public class UIGameGlobalPanelData : UIPanelData
	{
	}
	public partial class UIGameGlobalPanel : UIPanel, IController
	{
		private IUIGameGlobalPanelModel _uiGameGlobalPanelModel;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameGlobalPanelData ?? new UIGameGlobalPanelData();
			// please add init code here

			_uiGameGlobalPanelModel = this.GetModel<IUIGameGlobalPanelModel>();
			
			OpenTaskSelectButton.onClick.AddListener(() =>
			{
				UITaskSelectPanel.Show();
			});
			
			TaskShowButton.onClick.AddListener(() =>
			{
				this.SendCommand(new OpenOrCloseTaskPanel());
			});

			_uiGameGlobalPanelModel.IfTaskPanelShow.RegisterWithInitValue(value =>
			{
				if (value)
				{
					UITaskPanel.Show();
					TaskShowButtonName.text = "收起";
				}
				else
				{
					UITaskPanel.Hide();
					TaskShowButtonName.text = "任务";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
