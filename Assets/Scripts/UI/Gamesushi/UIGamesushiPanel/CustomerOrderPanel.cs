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

		private Dictionary<Vector2, CustomerOrderTemplate> _customerOrderTemplates =
			new Dictionary<Vector2, CustomerOrderTemplate>();
		
		private void Start()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();

			Events.CreateCustomerOrderMenuIcon.Register((createPosition, menuKey) =>
			{
				if (menuKey != null)
				{
					CustomerOrderTemplate.InstantiateWithParent(CustomerOrderRoot).Self(self =>
					{
						self.Name.text = _menuSystem.MenuItemInfos[menuKey].Name;
						self.Icon.sprite = _menuSystem.MenuItemInfos[menuKey].Icon;
						self.transform.position = createPosition;
						self.Show();
						_customerOrderTemplates.Add(createPosition, self);
					});
				}
				else
				{
					_customerOrderTemplates[createPosition].gameObject.DestroySelf();
					_customerOrderTemplates.Remove(createPosition);
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnDisable()
		{
			foreach (var (position, customerOrderTemplate) in _customerOrderTemplates)
			{
				customerOrderTemplate.DestroySelf();
			}
			_customerOrderTemplates.Clear();
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