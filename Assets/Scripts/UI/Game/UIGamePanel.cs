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

		private IUIGamePanelModel _uiGamePanelModel;

		private IFishSystem _fishSystem;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();

			_playerModel = this.GetModel<IPlayerModel>();

			_uiGamePanelModel = this.GetModel<IUIGamePanelModel>();

			_fishSystem = this.GetSystem<IFishSystem>();

			_playerModel.PlayerOxygen.RegisterWithInitValue(value =>
			{
				OxygenValue.text ="氧气\n" + value.ToString("0");
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_playerModel.NumberOfFish.RegisterWithInitValue(value =>
			{
				NumberOfFish.text = "捕获数量:" + value.ToString();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGamePanelModel.IfBackPackOpen.Register(value =>
			{
				if (value)
				{
					UIPackPanel.Show();
				}
				else
				{
					UIPackPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.CatchFish.Register(fish =>
			{
				foreach (Transform child in UIPackPanel.ItemListPanel.ItemListRoot.transform)
				{
					Destroy(child.gameObject);
				}

				foreach (var (key, value) in _fishSystem.CaughtFish)
				{
					UIPackPanel.ItemListPanel.ItemTemplate.InstantiateWithParent(UIPackPanel.ItemListPanel.ItemListRoot)
						.Self(self =>
						{
							self.ItemIcon.sprite = value.FishIcon;
							self.ItemName.text = value.FishName;
							self.ItemStar.text = value.Star.ToString() + " 星";
							self.ItemAmount.text = "× " + value.Amount.ToString();
							self.Show();
						});
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
