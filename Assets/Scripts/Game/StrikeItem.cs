using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum MineralType
	{
		Copper,
	}
	
	public partial class StrikeItem : ViewController, IController
	{
		private static ResLoader _resLoader = ResLoader.Allocate();

		public GameObject pickUpItemRoot;
		
		public MineralType type = MineralType.Copper;

		private int _requiredTimes = 3;

		private void Start()
		{
			
			
			InductionBox.OnTriggerEnter2DEvent(other =>
			{
				if (other.CompareTag("MeleeWeaponHitBox"))
				{
					_requiredTimes--;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (_requiredTimes == 0)
			{
				var pickupItem = _resLoader.LoadSync<GameObject>("PickUpItem");

				pickupItem.InstantiateWithParent(pickUpItemRoot.transform).Self(self =>
				{
					self.transform.position = this.transform.position;
					self.GetComponent<PickUpItem>().key = BackPackItemConfig.KelpKey;
					self.Show();
				});
				
				this.gameObject.DestroySelf();
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
