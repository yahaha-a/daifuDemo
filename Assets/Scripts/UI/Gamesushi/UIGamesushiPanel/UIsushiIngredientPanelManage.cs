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
	public partial class UIsushiIngredientPanelManage : UIElement, IController
	{
		private IBackPackSystem _backPackSystem;

		private IMenuSystem _menuSystem;

		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private List<sushiBackPackItemTemplate> _sushiBackPackItemTemplates = new List<sushiBackPackItemTemplate>();

		private List<(Button, BackPackItemType)> _buttonList = new List<(Button, BackPackItemType)>();

		private List<OptionMenuItemTemplate> _optionMenuItemTemplates = new List<OptionMenuItemTemplate>();
		
		private void Awake()
		{
			_backPackSystem = this.GetSystem<IBackPackSystem>();

			_menuSystem = this.GetSystem<IMenuSystem>();

			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_uiGamesushiPanelModel.CurrentBackPackItemType.Register(value =>
			{
				UpdateShow(value);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGamesushiPanelModel.CurrentSelectsushiBackPackItemKey.Register(value =>
			{
				UpgradeOptionMenu();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Start()
		{
			_buttonList.Add((FishButton, BackPackItemType.Fish));
			_buttonList.Add((IngredientButton, BackPackItemType.Ingredient));
			_buttonList.Add((SeasoningButton, BackPackItemType.Seasoning));
			
			foreach (var (button, backPackItemType) in _buttonList)
			{
				button.onClick.AddListener(() =>
				{
					_uiGamesushiPanelModel.CurrentBackPackItemType.Value = backPackItemType;
				});
			}
		}

		void UpdateShow(BackPackItemType backPackItemType)
		{
			if (_sushiBackPackItemTemplates != null)
			{
				foreach (var backPackItemTemplate in _sushiBackPackItemTemplates)
				{
					backPackItemTemplate.gameObject.DestroySelf();
				}
				
				_sushiBackPackItemTemplates.Clear();
			}

			int times = 0;

			foreach (var (key, count) in _backPackSystem.SuShiBackPackItemList)
			{
				if (_backPackSystem.BackPackItemInfos[key].ItemType == backPackItemType)
				{
					if (times == 0)
					{
						_uiGamesushiPanelModel.CurrentSelectsushiBackPackItemKey.Value = key;
						ItemName.text = _backPackSystem.BackPackItemInfos[key].ItemName;
						ItemIcon.sprite = _backPackSystem.BackPackItemInfos[key].ItemIcon;
						ItemCount.text = "目前拥有数量: " + count;
						ItemDescription.text = _backPackSystem.BackPackItemInfos[key].ItemDescription;
					}

					times++;
					sushiBackPackItemTemplate.InstantiateWithParent(BackPackFishItemListRoot).Self(self =>
					{
						self.Name.text = _backPackSystem.BackPackItemInfos[key].ItemName;
						self.Icon.sprite = _backPackSystem.BackPackItemInfos[key].ItemIcon;
						self.Count.text = count.ToString();
						self.GetComponent<Button>().onClick.AddListener(() =>
						{
							ItemName.text = _backPackSystem.BackPackItemInfos[key].ItemName;
							ItemIcon.sprite = _backPackSystem.BackPackItemInfos[key].ItemIcon;
							ItemCount.text = "目前拥有数量: " + count;
							ItemDescription.text = _backPackSystem.BackPackItemInfos[key].ItemDescription;
							_uiGamesushiPanelModel.CurrentSelectsushiBackPackItemKey.Value = key;
						});
						self.Show();
						_sushiBackPackItemTemplates.Add(self);
					});
				}
			}
		}

		private void OnEnable()
		{
			UpdateShow(_uiGamesushiPanelModel.CurrentBackPackItemType.Value);
		}

		void UpgradeOptionMenu()
		{
			if (_optionMenuItemTemplates != null)
			{
				foreach (var optionMenuItemTemplate in _optionMenuItemTemplates)
				{
					optionMenuItemTemplate.gameObject.DestroySelf();
				}
				
				_optionMenuItemTemplates.Clear();
			}
			
			var key = _uiGamesushiPanelModel.CurrentSelectsushiBackPackItemKey.Value;
			
			foreach (var (menuKey, menuItemInfo) in _menuSystem.MenuItemInfos)
			{
				foreach (var (backPackKey, amount) in menuItemInfo.RequiredIngredientsAmount)
				{
					if (key == backPackKey && _menuSystem.CurrentOwnMenuItems.ContainsKey(menuKey))
					{
						OptionMenuItemTemplate.InstantiateWithParent(OptionMenuRoot).Self(self =>
						{
							self.Icon.sprite = menuItemInfo.Icon;
							self.Name.text = _menuSystem.MenuItemInfos[menuKey].Name;
							self.Rank.text = "Lv." + _menuSystem.CurrentOwnMenuItems[menuKey].Rank.ToString();
							self.Money.text = "$ " + _menuSystem.MenuItemInfos[menuKey].RankWithCost
								.FirstOrDefault(item => item.Item1 == _menuSystem.CurrentOwnMenuItems[menuKey].Rank.Value)
								.Item2;
							self.Show();
							_optionMenuItemTemplates.Add(self);
						});
					}
					else if (key == backPackKey)
					{
						OptionMenuItemTemplate.InstantiateWithParent(OptionMenuRoot).Self(self =>
						{
							self.Icon.sprite = menuItemInfo.Icon;
							self.Name.text = "???????";
							self.Rank.text = "Lv.??";
							self.Money.text = "$ ???";
							self.Show();
							_optionMenuItemTemplates.Add(self);
						});
					}
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