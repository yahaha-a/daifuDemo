using System;
using Global;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum PickUpItemState
	{
		FreeTime,
		PickUpIng,
		PickUpEd
	}
	
	public partial class PickUpItem : ViewController, IController
	{
		private static ResLoader _resLoader = ResLoader.Allocate();
		
		public string key;

		private IBackPackSystem _backPackSystem;
		
		private PickUpItemState _state = PickUpItemState.FreeTime;
		private void Start()
		{
			_backPackSystem = this.GetSystem<IBackPackSystem>();

			var iconName = _backPackSystem.BackPackItemInfos[key].ItemKey;
			Icon.sprite = _resLoader.LoadSync<Sprite>(iconName);
			
			var playModel = this.GetModel<IPlayerModel>();

			playModel.CurrentState.Register(value =>
			{
				if (value == PlayState.PickingUp && _state == PickUpItemState.PickUpIng)
				{
					Events.ItemPickUped?.Trigger(this);
					this.gameObject.DestroySelf();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			inductionBox.OnTriggerEnter2DEvent(other =>
			{
				if (other.CompareTag("Player"))
				{
					playModel.IfCanPickUp.Value = true;
					_state = PickUpItemState.PickUpIng;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			inductionBox.OnTriggerExit2DEvent(other =>
			{
				if (other.CompareTag("Player"))
				{
					playModel.IfCanPickUp.Value = false;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
