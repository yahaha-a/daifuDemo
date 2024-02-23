using System;
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
		public string key = BackPackItemConfig.CordageKey;
		
		private PickUpItemState _state = PickUpItemState.FreeTime;
		private void Start()
		{
			var playModel = this.GetModel<IPlayerModel>();

			playModel.State.Register(value =>
			{
				if (value == PlayState.PickUpEd && _state == PickUpItemState.PickUpIng)
				{
					Events.ItemPickUped?.Trigger(this);
					playModel.State.Value = PlayState.Swim;
					this.gameObject.DestroySelf();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			inductionBox.OnTriggerEnter2DEvent(other =>
			{
				if (other.CompareTag("Player"))
				{
					playModel.State.Value = PlayState.PickUp;
					_state = PickUpItemState.PickUpIng;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			inductionBox.OnTriggerExit2DEvent(other =>
			{
				if (other.CompareTag("Player"))
				{
					playModel.State.Value = PlayState.Swim;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
