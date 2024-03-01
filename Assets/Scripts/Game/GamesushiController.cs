using System;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace daifuDemo
{
	public partial class GamesushiController : ViewController, IController
	{
		private float _pressKeyCodeETime = 0;

		private bool _ifPressKeyCodeE;

		private IBusinessModel _businessModel;
		
		private void Awake()
		{
			ResKit.Init();
			UIKit.Root.SetResolution(1920, 1080, 1);
			UIKit.OpenPanel<UIGamesushiPanel>();
		}

		private void Start()
		{
			_businessModel = this.GetModel<IBusinessModel>();
			
			Events.CommencedBusiness.Register(() =>
			{
				_businessModel.IfBusinessStart.Value = true;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FinishBusiness.Register(() =>
			{
				_businessModel.IfBusinessStart.Value = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.SendCommand<OpenOrClosesushiMenuPanel>();
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.SendCommand<OpenOrClosesushiIngredientPanel>();
			}
			
			
			if (Input.GetKeyDown(KeyCode.E))
			{
				_ifPressKeyCodeE = true;
			}

			if (!_businessModel.IfBusinessStart.Value)
			{
				if (_ifPressKeyCodeE)
				{
					_pressKeyCodeETime += Time.deltaTime;
					if (_pressKeyCodeETime >= 2f)
					{
						Events.CommencedBusiness?.Trigger();
						_pressKeyCodeETime = 0;
						_ifPressKeyCodeE = false;
					}
				}
			}
			
			if (Input.GetKeyUp(KeyCode.E))
			{
				_ifPressKeyCodeE = false;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
