using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class TableBoxTemplate : ViewController, IController
	{
		private IBusinessModel _businessModel;
		
		public ITableItemInfo TableItem { get; set; }

		private void Start()
		{
			_businessModel = this.GetModel<IBusinessModel>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("PlayerInteractionBox"))
			{
				_businessModel.CurrentTouchTableItemInfo.Value = TableItem;
			}
		}

		private void Update()
		{
			switch (TableItem.TableState)
			{
				case TableState.Empty:
					Empty();
					break;
				case TableState.HavePerson:
					HavePerson();
					break;
				case TableState.HaveRubbish:
					break;
			}
		}

		private void Empty()
		{
			if (TableItem.CustomerItemInfo != null)
			{
				TableItem.WithTableState(TableState.HavePerson);
			}
		}

		private void HavePerson()
		{
			if (TableItem.CustomerItemInfo.State == CustomerItemState.Leave)
			{
				TableItem.WithCustomerInfo(null);
				TableItem.WithTableState(TableState.Empty);
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
