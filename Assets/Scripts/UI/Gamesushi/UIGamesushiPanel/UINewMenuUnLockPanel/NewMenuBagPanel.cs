/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class NewMenuBagPanel : UIElement, IController
	{
		private IMenuSystem _menuSystem;

		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private List<MenuItemTemplate> _menuItems = new List<MenuItemTemplate>();
		
		private void Awake()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();

			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
			
			_uiGamesushiPanelModel.CurrentSelectLockMenuItemKey.Register(menuKey =>
			{
				ReFreshShow();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			_uiGamesushiPanelModel.CurrentSelectLockMenuItemKey.Value =
				_menuSystem.CurrentLockMenuItems.FirstOrDefault();
		}

		private void ReFreshShow()
		{
			foreach (var menuItemTemplate in _menuItems)
			{
				menuItemTemplate.gameObject.DestroySelf();
			}
			_menuItems.Clear();

			foreach (var menuKey in _menuSystem.CurrentLockMenuItems)
			{
				MenuItemTemplate.InstantiateWithParent(MenuItemRoot).Self(self =>
				{
					self.Icon.sprite = _menuSystem.MenuItemInfos[menuKey].Icon;
					self.Name.text = _menuSystem.MenuItemInfos[menuKey].Name;
					self.GetComponent<Button>().onClick.AddListener(() =>
					{
						_uiGamesushiPanelModel.CurrentSelectLockMenuItemKey.Value = menuKey;
					});
					self.Show();
					_menuItems.Add(self);
				});
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