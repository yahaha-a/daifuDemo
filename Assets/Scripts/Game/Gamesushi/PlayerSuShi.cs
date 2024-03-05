using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class PlayerSuShi : ViewController, IController
	{
		private IBusinessModel _businessModel;

		private IPlayerSuShiModel _iPlayerSuShiModel;

		private IMenuSystem _menuSystem;

		private string _currentHaveMenuKey;
		
		private float _speed = 3f;

		private ITableItemInfo _tableItem;

		private void Start()
		{
			_businessModel = this.GetModel<IBusinessModel>();

			_iPlayerSuShiModel = this.GetModel<IPlayerSuShiModel>();

			_menuSystem = this.GetSystem<IMenuSystem>();

			_businessModel.CurrentTouchTableItemInfo.Register(value =>
			{
				if (value != null && value.CustomerItemInfo != null && value.CustomerItemInfo.CurrentOrderKey != null &&
				    value.CustomerItemInfo.CurrentOrderKey.Value == _currentHaveMenuKey)
				{
					_tableItem = value;
					_iPlayerSuShiModel.IfCanGiveCurrentDish.Value = true;
				}
				else
				{
					_tableItem = null;
					_iPlayerSuShiModel.IfCanGiveCurrentDish.Value = false;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			var inputHorizontal = Input.GetAxis("Horizontal");
			var inputVertical = Input.GetAxis("Vertical");
			
			var direction = new Vector2(inputHorizontal, inputVertical).normalized;
			var playerTargetWalkingSpeed = direction * _speed;
			Rigidbody2d.velocity = playerTargetWalkingSpeed;

			if (_iPlayerSuShiModel.IfCanTakeFinishDish.Value)
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					_currentHaveMenuKey = _menuSystem.TakeAFinishedDish();
					if (_currentHaveMenuKey != null)
					{
						Events.TakeFirstFinishedDish?.Trigger();
					}
				}
			}

			if (_iPlayerSuShiModel.IfCanGiveCurrentDish.Value)
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (_tableItem.CustomerItemInfo.CurrentOrderKey.Value == _currentHaveMenuKey)
					{
						_tableItem.CustomerItemInfo.WithIfReceiveOrderDish(true);
						_currentHaveMenuKey = null;
					}
				}
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
