using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum TreasureBoxState
	{
		FreeTime,
		Opening,
		Opened
	}
	
	public partial class TreasureBox : ViewController, IController
	{
		public string key = Config.SpiceTreasureChestKey;

		private BackPackItemType _itemType;

		public string backPackItemKey;

		private TreasureBoxState _treasureBoxState;

		private float _openNeedSeconds;
		
		private IPlayerModel _playerModel;

		private ITreasureBoxSystem _treasureBoxSystem;

		private IBackPackSystem _backPackSystem;

		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();

			_treasureBoxSystem = this.GetSystem<ITreasureBoxSystem>();

			_backPackSystem = this.GetSystem<IBackPackSystem>();
			
			_openNeedSeconds = _treasureBoxSystem.FindTreasureItemInfo(key).OpenNeedSeconds;
			
			_itemType = _treasureBoxSystem.FindTreasureItemInfo(key).PossessionItemType;
			
			_playerModel.OpenChestSeconds.Register(seconds =>
			{
				if (_treasureBoxState == TreasureBoxState.Opening)
				{
					if (seconds >= _openNeedSeconds)
					{
						_treasureBoxState = TreasureBoxState.Opened;
						backPackItemKey = _backPackSystem.AccordingItemTypeGetRandomOne(_itemType);
						Events.TreasureBoxOpened?.Trigger(this);
						this.gameObject.DestroySelf();
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			InductionBox.OnTriggerEnter2DEvent(other =>
			{
				if (other.CompareTag("Player") && _treasureBoxState == TreasureBoxState.FreeTime)
				{
					_playerModel.State.Value = PlayState.OpenTreasureChests;
					_treasureBoxState = TreasureBoxState.Opening;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			InductionBox.OnTriggerExit2DEvent(other =>
			{
				if (other.CompareTag("Player") && _treasureBoxState != TreasureBoxState.Opened)
				{
					_playerModel.State.Value = PlayState.Swim;
					_treasureBoxState = TreasureBoxState.FreeTime;
				}
				else if (other.CompareTag("Player") && _treasureBoxState == TreasureBoxState.Opened)
				{
					_playerModel.State.Value = PlayState.Swim;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
