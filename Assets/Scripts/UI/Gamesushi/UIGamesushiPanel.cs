using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Unity.VisualScripting;

namespace daifuDemo
{
	public class UIGamesushiPanelData : UIPanelData
	{
	}
	public partial class UIGamesushiPanel : UIPanel, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private IBusinessModel _businessModel;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamesushiPanelData ?? new UIGamesushiPanelData();
			// please add init code here

			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_businessModel = this.GetModel<IBusinessModel>();

			Events.CommencedBusiness.Register(this.SendCommand<OpenOrCloseCustomerOrderPanelCommand>)
				.UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FinishBusiness.Register(this.SendCommand<OpenOrCloseCustomerOrderPanelCommand>)
				.UnRegisterWhenGameObjectDestroyed(gameObject);

			CustomerOrderPanel.Show();

			_uiGamesushiPanelModel.IfUIsushiIngredientPanelOpen.Register(value =>
			{
				if (value)
				{
					UIsushiIngredientPanel.Show();
				}
				else
				{
					UIsushiIngredientPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGamesushiPanelModel.IfUIStaffManagePanelOpen.Register(value =>
			{
				if (value)
				{
					StaffManagePanel.Show();
				}
				else
				{
					StaffManagePanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGamesushiPanelModel.IfUisushiMenuPanelOpen.Register(value =>
			{
				if (value)
				{
					UIsushiMenuPanel.Show();
				}
				else
				{
					UIsushiMenuPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_businessModel.IfCustomerOrderPanelShow.Register(value =>
			{
				if (value)
				{
					CustomerOrderPanel.Show();
				}
				else
				{
					CustomerOrderPanel.Hide();
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
