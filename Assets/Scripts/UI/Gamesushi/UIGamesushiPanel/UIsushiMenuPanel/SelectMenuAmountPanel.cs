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
	public partial class SelectMenuAmountPanel : UIElement, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private IMenuSystem _menuSystem;

		private IBackPackSystem _backPackSystem;

		private List<GameObject> _selectAmountNeedFoodItems = new List<GameObject>();
		
		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_menuSystem = this.GetSystem<IMenuSystem>();

			_backPackSystem = this.GetSystem<IBackPackSystem>();
			
			CloseButton.onClick.AddListener(() =>
			{
				_uiGamesushiPanelModel.IfUISelectMenuAmountPanelShow.Value = false;
			});
			
			ConfirmButton.onClick.AddListener(() =>
			{
				if (_menuSystem.IfCanMakeTodayMenu(_uiGamesushiPanelModel.SelectedMenuItemKey.Value) &&
				    _uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Value != 0)
				{
					_menuSystem.MakeTodayMenu(_uiGamesushiPanelModel.SelectedMenuItemKey.Value);
					_menuSystem.UpdateTodayMenuItems(_uiGamesushiPanelModel.SelectedMenuItemKey.Value,
						_uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Value,
						_uiGamesushiPanelModel.CurrentSelectMenuAmount.Value);
					
					_uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Value = 0;
					_uiGamesushiPanelModel.IfUISelectMenuAmountPanelShow.Value = false;
					Events.UpgradeMenu?.Trigger();
				}
			});
			
			Reduce.onClick.AddListener(() =>
			{
				if (_uiGamesushiPanelModel.CurrentSelectMenuAmount.Value > 1)
				{
					_uiGamesushiPanelModel.CurrentSelectMenuAmount.Value--;
				}
			});
			
			Increase.onClick.AddListener(() =>
			{
				if (_uiGamesushiPanelModel.CurrentSelectMenuAmount.Value < 99)
				{
					_uiGamesushiPanelModel.CurrentSelectMenuAmount.Value++;
				}
			});

			_uiGamesushiPanelModel.CurrentSelectMenuAmount.Register(value =>
			{
				Amount.text = value.ToString();
				foreach (var selectAmountNeedFoodItem in _selectAmountNeedFoodItems)
				{
					var item = selectAmountNeedFoodItem.GetComponent<SelectAmountNeedFooditemTemplte>();

					item.NeedAndOwnAmount.text = value * item.needAmount + "/" +
					                             _backPackSystem.SuShiBackPackItemList[item.backPackKey];
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{

			foreach (var (backPackKey, amount) in _menuSystem
				         .MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].RequiredIngredientsAmount)
			{
				SelectAmountNeedFooditemTemplte.InstantiateWithParent(NeeedFoodListRoot).Self(self =>
				{
					self.Icon.sprite = _backPackSystem.BackPackItemInfos[backPackKey].ItemIcon;
					self.Name.text = _backPackSystem.BackPackItemInfos[backPackKey].ItemName;
					self.NeedAndOwnAmount.text = amount + "/" + _backPackSystem.SuShiBackPackItemList[backPackKey];
					self.needAmount = amount;
					self.backPackKey = backPackKey;
					self.Show();
					_selectAmountNeedFoodItems.Add(self.gameObject);
				});
			}

			Amount.text = _uiGamesushiPanelModel.CurrentSelectMenuAmount.Value.ToString();

			Icon.sprite = _menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Icon;
			Name.text = _menuSystem.MenuItemInfos[_uiGamesushiPanelModel.SelectedMenuItemKey.Value].Name;
		}

		private void OnDisable()
		{
			_uiGamesushiPanelModel.CurrentSelectMenuAmount.Value = 1;

			foreach (var selectAmountNeedFoodItem in _selectAmountNeedFoodItems)
			{
				selectAmountNeedFoodItem.DestroySelf();
			}
			_selectAmountNeedFoodItems.Clear();
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