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

		private void Start()
		{
			_gameGlobalModel = this.GetModel<IGameGlobalModel>();

			_gameGlobalModel.CurrentObtainItem.Register(item =>
			{
				if (item != null)
				{
					ObtainItemsTextTemplete.InstantiateWithParent(ObtainItemsTextPanel).Self(self =>
					{
						self.obtainItemName = item.Name + " +" + item.Number;
						self.Show();
					});
					ObtainItemsIconTemplete.InstantiateWithParent(ObtainItemsIconPanel).Self(self =>
					{
						self.transform.position = Camera.main.WorldToScreenPoint(item.StartPosition);
						self.icon = _resLoader.LoadSync<Sprite>(item.IconKey);
						self.Show();
					});
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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