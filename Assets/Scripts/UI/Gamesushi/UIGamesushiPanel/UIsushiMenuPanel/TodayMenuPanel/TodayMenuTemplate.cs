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
	public enum TodayMenuTemplateType
	{
		Add,
		Menu
	}
	
	public partial class TodayMenuTemplate : UIElement, IController
	{
		public ITodayMenuItemInfo ItemInfo { get; set; }

		private TodayMenuTemplateType _type;
		
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private IMenuSystem _menuSystem;
		
		private void Start()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_menuSystem = this.GetSystem<IMenuSystem>();
			
			ItemInfo.Key.RegisterWithInitValue(key =>
			{
				if (key == null)
				{
					_type = TodayMenuTemplateType.Add;
				}
				else
				{
					_type = TodayMenuTemplateType.Menu;
				}
				
				RefreshShow();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			ItemInfo.Amount.Register(amount =>
			{
				RefreshShow();
			});
			
			GetComponent<Button>().onClick.AddListener(() =>
			{
				if (_type == TodayMenuTemplateType.Add)
				{
					_uiGamesushiPanelModel.IfUIMenuPanelShow.Value = true;
				}
				else if (_type == TodayMenuTemplateType.Menu)
				{
					_uiGamesushiPanelModel.IfUIMenuPanelShow.Value = false;
				}
				_uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Value = ItemInfo.Node;
			});

			_uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Register(node =>
			{
				if (ItemInfo.Key != null && node == ItemInfo.Node)
				{
					Options.Show();
				}
				else
				{
					Options.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			AddButton.onClick.AddListener(() =>
			{
				_menuSystem.AddTodayMenuItemAmount();
			});
			
			ReduceButton.onClick.AddListener(() =>
			{
				_menuSystem.ReduceTodayMenuItemAmount();
			});
			
			AutoButton.onClick.AddListener(() =>
			{
				_menuSystem.WhenTodayMenuItemAmountWithOneThenAutoSupply();
			});
						
			RemoveButton.onClick.AddListener(() =>
			{
				_menuSystem.RemoveTodayMenuItem();
			});
		}

		private void RefreshShow()
		{
			if (_type == TodayMenuTemplateType.Add)
			{
				AddMenuMessage.Show();
				MenuMessage.Hide();
			}
			else if (_type == TodayMenuTemplateType.Menu)
			{
				AddMenuMessage.Hide();
				MenuMessage.Show();
				Icon.sprite = _menuSystem.MenuItemInfos[ItemInfo.Key.Value].Icon;
				Name.text = _menuSystem.MenuItemInfos[ItemInfo.Key.Value].Name;
				Rank.text = "Lv." + _menuSystem.CurrentOwnMenuItems[ItemInfo.Key.Value].Rank;
				Amount.text = "×" + ItemInfo.Amount;
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