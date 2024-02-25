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
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamesushiPanelData ?? new UIGamesushiPanelData();
			// please add init code here

			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

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
