using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class WaiterManage : ViewController,IController
	{
		private IMenuSystem _menuSystem;

		private IStaffSystem _staffSystem;

		private List<(int, Vector2)> _positionList = new List<(int, Vector2)>()
		{
			(1, new Vector2(2, 1)),
			(2, new Vector2(-3, 1))
		};

		private List<WaiterTemplate> _waiters = new List<WaiterTemplate>();

		private void Start()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();
			_staffSystem = this.GetSystem<IStaffSystem>();

			Events.CommencedBusiness.Register(() =>
			{
				foreach (var (node, staffKey) in _staffSystem.CurrentWaiters)
				{
					WaiterTemplate.InstantiateWithParent(this).Self(self =>
					{
						self.StaffItemInfo = _staffSystem.CurrentOwnStaffItems[staffKey];
						self.StartPosition = _positionList.FirstOrDefault(item => item.Item1 == node).Item2;
						self.Show();
						_waiters.Add(self);
					});
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FinishBusiness.Register(() =>
			{
				foreach (var waiterTemplate in _waiters)
				{
					waiterTemplate.DestroySelf();
				}

				_waiters.Clear();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
