using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public class UIGamePanelData : UIPanelData
	{
		
	}
	public partial class UIGamePanel : UIPanel, IController
	{
		private IPlayerModel _playerModel;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();

			_playerModel = this.GetModel<IPlayerModel>();

			_playerModel.PlayerOxygen.RegisterWithInitValue(value =>
			{
				OxygenValue.text ="氧气\n" + value.ToString("0");
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_playerModel.NumberOfFish.RegisterWithInitValue(value =>
			{
				NumberOfFish.text = "捕获数量:" + value.ToString();
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
