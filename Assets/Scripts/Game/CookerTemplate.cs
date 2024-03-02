using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class CookerTemplate : ViewController, IController
	{
		public IStaffItemInfo StaffItemInfo { get; set; }

		private IMenuSystem _menuSystem;

		private bool _ifStartCook = false;

		private IBusinessModel _businessModel;

		private void Start()
		{
			_businessModel = this.GetModel<IBusinessModel>();
			
			_menuSystem = this.GetSystem<IMenuSystem>();
			
			Events.CommencedBusiness.Register(() =>
			{
				_ifStartCook = true;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FinishBusiness.Register(() =>
			{
				_ifStartCook = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (_ifStartCook)
			{
				_menuSystem.CreatePreparationDishes(StaffItemInfo.CookSpeed);
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
