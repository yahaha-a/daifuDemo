using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class KitchenManage : ViewController, IController
	{
		private IStaffSystem _staffSystem;

		private IPlayerSuShiModel _iPlayerSuShiModel;

		private List<CookerTemplate> _cookers = new List<CookerTemplate>();

		private Dictionary<Vector2, bool> _positionList = new Dictionary<Vector2, bool>()
		{
			{
				new Vector2(-1, 0), true
			},
			{
				new Vector2(0, 0), true
			},
			{
				new Vector2(1, 0), true
			}
		};
		
		private void Start()
		{
			_staffSystem = this.GetSystem<IStaffSystem>();

			_iPlayerSuShiModel = this.GetModel<IPlayerSuShiModel>();

			InteractionBox.OnTriggerEnter2DEvent(other =>
			{
				if (other.CompareTag("PlayerInteractionBox"))
				{
					_iPlayerSuShiModel.IfCanTakeFinishDish.Value = true;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			InteractionBox.OnTriggerExit2DEvent(other =>
			{
				if (other.CompareTag("PlayerInteractionBox"))
				{
					_iPlayerSuShiModel.IfCanTakeFinishDish.Value = false;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.CommencedBusiness.Register(() =>
			{
				foreach (var (node, staffKey) in _staffSystem.CurrentCookers)
				{
					if (staffKey != null)
					{
						CookerTemplate.InstantiateWithParent(this).Self(self =>
						{
							self.StaffItem = _staffSystem.CurrentOwnStaffItems[staffKey];
							Vector2 position = _positionList.FirstOrDefault(item => item.Value).Key;
							self.transform.localPosition = position;
							_positionList[position] = false;
							self.Show();
							_cookers.Add(self);
						});
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
