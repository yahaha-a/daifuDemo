using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class TableManage : ViewController, IController
	{
		private List<TableBoxTemplate> _tableBoxList = new List<TableBoxTemplate>();

		private ICustomerSystem _customerSystem;

		private void Start()
		{
			_customerSystem = this.GetSystem<ICustomerSystem>();

			Events.CommencedBusiness.Register(() =>
			{
				_customerSystem.CreateTables();
				
				foreach (var customerSystemTableItem in _customerSystem.TableItems)
				{
					TableBoxTemplate.InstantiateWithParent(this).Self(self =>
					{
						self.transform.position = customerSystemTableItem.CurrentPosition;

						self.TableItem = customerSystemTableItem;

						self.Show();
						_tableBoxList.Add(self);
					});
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FinishBusiness.Register(() =>
			{
				foreach (var tableBoxTemplate in _tableBoxList)
				{
					tableBoxTemplate.gameObject.DestroySelf();
				}

				_tableBoxList.Clear();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
