/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class CustomerOrderPanel : UIElement, IController
	{
		private IMenuSystem _menuSystem;
		
		private void Start()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();

			Events.CreateCustomerOrderMenuIcon.Register((createPosition, menuKey) =>
			{
				CustomerOrderTemplate.InstantiateWithParent(CustomerOrderRoot).Self(self =>
				{
					self.Name.text = _menuSystem.MenuItemInfos[menuKey].Name;
					self.Icon.sprite = _menuSystem.MenuItemInfos[menuKey].Icon;
					self.transform.position = createPosition;
					self.Show();
				});
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