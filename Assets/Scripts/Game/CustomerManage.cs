using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class CustomerManage : ViewController, IController
	{
		private ICustomerSystem _customerSystem;

		private List<GameObject> _customerInstances = new List<GameObject>();

		private float _customerCreateInterval = 4f;

		private float Timer = 0f;

		private bool _ifCreateStart = false;
		
		private void Start()
		{
			_customerSystem = this.GetSystem<ICustomerSystem>();
			
			Events.CommencedBusiness.Register(() =>
			{
				_customerSystem.CreateCustomers();
				
				_ifCreateStart = true;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (_ifCreateStart)
			{
				CreateCustomerTimer();
				
				_customerInstances.RemoveAll(item => item == null);
			}
		}

		private void CreateCustomerTimer()
		{
			Timer += Time.deltaTime;

			if (Timer >= _customerCreateInterval)
			{
				CreateCustomer();
				Timer = 0;
			}
		}

		private void CreateCustomer()
		{
			var random = new System.Random();

			var tableItem = _customerSystem.TableItems.Where(tableItem => tableItem.TableState == TableState.Empty)
				.OrderBy(x => random.Next()).FirstOrDefault();

			var customerItemInfo =
				_customerSystem.CustomerItems.FirstOrDefault(item => item.State == CustomerItemState.Free);
			
			if (tableItem != null && customerItemInfo != null)
			{
				CustomerTemplate.InstantiateWithParent(this).Self(self =>
				{
					self.CustomerItemInfo = customerItemInfo;
					customerItemInfo.WithState(CustomerItemState.Walk);

					self.StartPosition = new Vector2(-8, tableItem.CurrentPosition.y);
					self.TargetPosition = tableItem.CurrentPosition;
					
					tableItem.WithCustomerInfo(customerItemInfo);

					self.transform.position = self.StartPosition;
					self.Show();
					_customerInstances.Add(self.gameObject);
				});
			}
			else if (customerItemInfo == null && _customerInstances.Count == 0)
			{
				_ifCreateStart = false;
				Events.FinishBusiness?.Trigger();
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
