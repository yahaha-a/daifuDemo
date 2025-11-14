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
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();
			
			_player = FindObjectOfType<Player>();

			_playerModel = this.GetModel<IPlayerModel>();

			_uiGamePanelModel = this.GetModel<IUIGamePanelModel>();

			_fishSystem = this.GetSystem<IFishSystem>();
			
			Events.LoadMapComplete.Register(() =>
			{
				_player = FindObjectOfType<Player>();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
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

			Events.GamePass.Register(() =>
			{
				UISettlePanel.Show();
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
			
			_playerModel.CurrentPosition.Register(value =>
			{
				Vector3 screenPosition = Camera.main.WorldToScreenPoint(value);
				CounterPanel.GetComponent<RectTransform>().position = screenPosition + new Vector3(-20, 80, 0);
				WeaponUpgradePanel.GetComponent<RectTransform>().position = screenPosition + new Vector3(70, 50, 0);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_uiGamePanelModel.CurrentCounterPanelState.Register(state =>
			{
				switch (state)
				{
					case CounterPanelState.Hide:
						CounterPanel.Hide();
						break;
					case CounterPanelState.CatchFish:
						CounterPanel.Show();
						break;
					case CounterPanelState.OpenTreasure:
						CounterPanel.Show();
						break;
					case CounterPanelState.Reloading:
						CounterPanel.Show();
						break;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			BackPackButton.onClick.AddListener(() =>
			{
				this.SendCommand<OpenOrCloseBackpackCommand>();
			});
			
			CloseButton.onClick.AddListener(() =>
			{
				this.SendCommand<GamePassCommand>();
			});
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
