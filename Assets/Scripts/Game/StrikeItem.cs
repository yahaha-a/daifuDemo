using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace daifuDemo
{
	public partial class StrikeItem : ViewController, IController
	{
		private static ResLoader _resLoader = ResLoader.Allocate();

		public GameObject pickUpItemRoot;

		public string key = Config.CopperOreKey;

		private PluckItemType _type;

		private string _dropItemKey;

		private List<(int, int)> _dropAmountWithTimes;

		private BindableProperty<int> _hitStage = new BindableProperty<int>(0);

		private int _needTime;

		private int _dropAmount;

		private void Start()
		{
			var _strikeIemSystem = this.GetSystem<IStrikeItemSystem>();

			_type = _strikeIemSystem.StrikeItemInfos[key].Type;
			_dropItemKey = _strikeIemSystem.StrikeItemInfos[key].DropItemKey;
			_dropAmountWithTimes = _strikeIemSystem.StrikeItemInfos[key].DropAmountWithTimes;

			_hitStage.RegisterWithInitValue(stage =>
			{
				if (stage == _dropAmountWithTimes.Count)
				{
					this.gameObject.DestroySelf();
				}
				else
				{
					_needTime = _dropAmountWithTimes[stage].Item1;
					_dropAmount = _dropAmountWithTimes[stage].Item2;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			InductionBox.OnTriggerEnter2DEvent(other =>
			{
				if (other.CompareTag("MeleeWeaponHitBox"))
				{
					DropItem();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void DropItem()
		{
			_needTime--;
			if (_needTime == 0)
			{
				for (int j = 0; j < _dropAmount; j++)
				{
					var pickupItem = _resLoader.LoadSync<GameObject>("PickUpItem");
					pickupItem.InstantiateWithParent(pickUpItemRoot.transform).Self(self =>
					{
						self.transform.position = this.transform.position + new Vector3(Random.Range(-1f, 1f), 0, 0);
						self.GetComponent<PickUpItem>().key = _dropItemKey;
						self.Show();
					});
				}
				_hitStage.Value++;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
