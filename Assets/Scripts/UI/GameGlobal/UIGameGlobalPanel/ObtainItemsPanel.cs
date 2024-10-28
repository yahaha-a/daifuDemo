/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class ObtainItemsPanel : UIElement, IController
	{
		private static ResLoader _resLoader = ResLoader.Allocate();
		
		private IGameGlobalModel _gameGlobalModel;
		private IUIGameGlobalPanelModel _gameGlobalPanelModel;

		private Queue<IObtainItemsInfo> ObtainItems = new Queue<IObtainItemsInfo>();

		private void Start()
		{
			_gameGlobalModel = this.GetModel<IGameGlobalModel>();
			_gameGlobalPanelModel = this.GetModel<IUIGameGlobalPanelModel>();

			_gameGlobalModel.CurrentObtainItem.Register(item =>
			{
				if (item != null)
				{
					ObtainItems.Enqueue(item);
					
					ObtainItemsIconTemplete.InstantiateWithParent(ObtainItemsIconPanel).Self(self =>
					{
						self.transform.position = Camera.main.WorldToScreenPoint(item.StartPosition);
						self.icon = _resLoader.LoadSync<Sprite>(item.IconKey);
						self.Show();
					});
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (ObtainItems.Count > 0 && _gameGlobalPanelModel.CurrentShowObtainItemsCount.Value < 4)
			{
				IObtainItemsInfo item = ObtainItems.Dequeue();

				if (item != null)
				{
					ObtainItemsTextTemplete.InstantiateWithParent(ObtainItemsTextPanel).Self(self =>
					{
						self.obtainItemName = item.Name + " +" + item.Number;
						self.Show();
						_gameGlobalPanelModel.CurrentShowObtainItemsCount.Value++;
					});
				}
			}
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}