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

		private IHarvestSystem _harvestSystem;
		
		private Player _player;
		
		protected override void OnInit(IUIData uiData = null)
		{
			_player = FindObjectOfType<Player>();
			
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
					
					foreach (Transform child in UIPackPanel.ItemListPanel.ItemListRoot.transform)
					{
						Destroy(child.gameObject);
					}

					foreach (var (key, caughtFishInfo) in _fishSystem.CaughtItem)
					{
						UIPackPanel.ItemListPanel.ItemTemplate.InstantiateWithParent(UIPackPanel.ItemListPanel.ItemListRoot)
							.Self(self =>
							{
								self.ItemIcon.sprite = caughtFishInfo.FishIcon;
								self.ItemName.text = caughtFishInfo.FishName;
								if (caughtFishInfo.Star == 0)
								{
									self.ItemStar.text = null;
								}
								else
								{
									self.ItemStar.text = caughtFishInfo.Star.ToString() + " 星";
								}
								self.ItemAmount.text = "× " + caughtFishInfo.Amount.ToString();
								self.Show();
							});
					}
				}
				else
				{
					UIPackPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.GamePass.Register(() =>
			{
				UISettlePanel.Show();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);


			Events.HitFish.Register(fish =>
			{
				_uiGamePanelModel.IfCatchFishPanelShow.Value = true;
				_playerModel.MaxFishingChallengeClicks.Value = fish.GetComponent<IFish>().Clicks;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGamePanelModel.IfUIHarvestPanelShow.Register(value =>
			{
				if (value)
				{
					UIHarvestPanel.Show();
				}
				else
				{
					UIHarvestPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGamePanelModel.IfCatchFishPanelShow.Register(value =>
			{
				if (value)
				{
					Vector3 playerWorldPosition = _player.transform.position;
					Vector2 screenPosition = Camera.main.WorldToScreenPoint(playerWorldPosition);
					Vector3 uiPosition;

					if (_playerModel.IfLeft.Value)
					{
						uiPosition = screenPosition + new Vector2(100, 0);
					}
					else
					{
						uiPosition = screenPosition + new Vector2(-100, 0);
					}
					
					CatchFishPanel.GetComponent<RectTransform>().position = uiPosition;
					
					CatchFishPanel.Show();
				}
				else
				{
					CatchFishPanel.Hide();
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
