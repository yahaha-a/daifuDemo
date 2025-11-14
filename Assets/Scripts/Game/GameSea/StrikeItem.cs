using System;
using System.Collections.Generic;
using Global;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace daifuDemo
{
	public partial class StrikeItem : ViewController, IController
	{
		private static ResLoader _resLoader = ResLoader.Allocate();

		public GameObject dropsRoot;

		public string key;

		private PluckItemType _type;

		private string _dropItemKey;

		private List<(int, int)> _dropAmountWithTimes;

		private BindableProperty<int> _hitStage = new BindableProperty<int>(0);

		private int _needTime;

		private int _dropAmount;

		private void Start()
		{
			var _strikeIemSystem = this.GetSystem<IStrikeItemSystem>();

			dropsRoot = GameObject.FindGameObjectWithTag("DropsRoot");
			
			Icon.sprite = _strikeIemSystem.StrikeItemInfos[key].Icon;
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
					var pickupItem = _resLoader.LoadSync<GameObject>("DropItem");
					pickupItem.InstantiateWithParent(dropsRoot.transform).Self(self =>
					{
						self.transform.position = this.transform.position;

						var rb = self.GetComponent<Rigidbody2D>();

						if (rb == null)
						{
							rb = self.AddComponent<Rigidbody2D>();
						}

						float randomX = Random.Range(-1f, 1f);
						float upwardForce = Random.Range(3f, 5f);

						rb.linearVelocity = new Vector2(randomX, upwardForce);

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
