using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class CookerTemplate : ViewController, IController
	{
		public IstaffItemInfo StaffItem { get; set; }

		private IMenuSystem _menuSystem;

		private IStaffSystem _staffSystem;

		public bool _ifStartCook = false;
		
		private bool _isCookCoroutineRunning = false;

		private IBusinessModel _businessModel;

		private void Start()
		{
			_businessModel = this.GetModel<IBusinessModel>();
			
			_menuSystem = this.GetSystem<IMenuSystem>();

			_staffSystem = this.GetSystem<IStaffSystem>();
			
			Events.CommencedBusiness.Register(() =>
			{
				_ifStartCook = true;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FinishBusiness.Register(() =>
			{
				_ifStartCook = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_ifStartCook = true;
		}

		private void Update()
		{
			if (_ifStartCook && !_isCookCoroutineRunning)
			{
				_ifStartCook = false;
				StartCoroutine(Cook());
			}
		}

		IEnumerator Cook()
		{
			_isCookCoroutineRunning = true;

			var cookSpeed = _staffSystem.StaffItemInfos[StaffItem.Key].RankWithCookSpeed
				.FirstOrDefault(rankWithSpeed => rankWithSpeed.Item1 == StaffItem.Rank).Item2;

			yield return _menuSystem.CreatePreparationDishes(cookSpeed);
			_isCookCoroutineRunning = false;
			_ifStartCook = true;
		}
		
		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
