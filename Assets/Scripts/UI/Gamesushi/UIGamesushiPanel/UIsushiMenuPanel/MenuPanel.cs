/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class MenuPanel : UIElement, IController
	{
		private IMenuSystem _menuSystem;

		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private List<GameObject> _menuItems = new List<GameObject>();

		private void Awake()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_uiGamesushiPanelModel.SelectedMenuItemKey.Value = _menuSystem.CurrentOwnMenuItems.Keys.FirstOrDefault();
			
			CloseButton.onClick.AddListener(() =>
			{
				_uiGamesushiPanelModel.IfUIMenuPanelShow.Value = false;
			});

			Events.UpgradeMenu.Register(() =>
			{
				UpdatePanel();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{
			foreach (var (key, currentOwnMenuItem) in _menuSystem.CurrentOwnMenuItems)
			{
				if (currentOwnMenuItem.Unlock)
				{
					MenuTemplate.InstantiateWithParent(MenuListRoot).Self(self =>
					{
						self.Icon.sprite = _menuSystem.MenuItemInfos[key].Icon;
						self.Rank.text = "Lv." + currentOwnMenuItem.Rank.ToString();
						self.Amount.text = currentOwnMenuItem.CanMakeNumber.ToString();
						string currentKey = key;
						self.GetComponent<Button>().onClick.AddListener(() =>
						{
							_uiGamesushiPanelModel.SelectedMenuItemKey.Value = currentKey;
						});
						self.Show();
						_menuItems.Add(self.gameObject);
					});
				}
			}
		}

		private void OnDisable()
		{
			foreach (var menuItem in _menuItems)
			{
				menuItem.DestroySelf();
			}
			_menuItems.Clear();
		}
		
		void UpdatePanel()
		{
			this.gameObject.Hide();
			this.gameObject.Show();
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