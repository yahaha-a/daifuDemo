using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public class UIGameShipPanelData : UIPanelData
	{
	}
	public partial class UIGameShipPanel : UIPanel, IController
	{
		private IUIGameShipPanelModel _uiGameShipPanelModel;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameShipPanelData ?? new UIGameShipPanelData();
			// please add init code here

			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			
			_uiGameShipPanelModel.IfShipUIPackOpen.Register(value =>
			{
				if (value)
				{
					UIShipUIPackPanel.Show();
				}
				else
				{
					UIShipUIPackPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGameShipPanelModel.IfGoToHomePanelOpen.Register(value =>
			{
				if (value)
				{
					GoToHomePanel.Show();
				}
				else
				{
					GoToHomePanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGameShipPanelModel.IfGotoSeaPanelOpen.Register(value =>
			{
				if (value)
				{
					GoToSeaPanel.Show();
				}
				else
				{
					GoToSeaPanel.Hide();
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
