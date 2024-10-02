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
			Icon.sprite = this.SendQuery(new FindStrikeItemIcon(key));
			
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
