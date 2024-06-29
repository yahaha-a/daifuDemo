/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public partial class UIHarvestPanel : UIElement, IController
	{
		private IHarvestSystem _harvestSystem;
		private IBackPackSystem _backPackSystem;
		private IUIGamePanelModel _uiGamePanelModel;

		private List<GameObject> _harvestItems = new List<GameObject>();
		
		private void Awake()
		{
			_harvestSystem = this.GetSystem<IHarvestSystem>();
			_backPackSystem = this.GetSystem<IBackPackSystem>();
			_uiGamePanelModel = this.GetModel<IUIGamePanelModel>();
		}

		private void Start()
		{
			CloseButton.onClick.AddListener(() =>
			{
				_uiGamePanelModel.IfBackPackOpen.Value = false;
				UIKit.ClosePanel<UIGamePanel>();
				this.SendCommand<ReloadDataCommand>();
				SceneManager.LoadScene("GameShip");
			});
		}

		private void OnEnable()
		{
			foreach (var (itemKey, itemCount) in _harvestSystem.HarvestItems)
			{
				if (_backPackSystem.BackPackItemInfos[itemKey].ItemType == BackPackItemType.Fish)
				{
					var harvestFishItemTemplate = HarvestFishItemTemplate.InstantiateWithParent(HarvestFishRoot)
						.Self(self =>
						{
							self.Icon.sprite = _backPackSystem.BackPackItemInfos[itemKey].ItemIcon;
							self.Name.text = this.SendQuery(new FindBackPackItemName(itemKey));
							self.Amount.text = itemCount.ToString();
							self.Show();
						});
					_harvestItems.Add(harvestFishItemTemplate.gameObject);
				}

				if (_backPackSystem.BackPackItemInfos[itemKey].ItemType == BackPackItemType.Ingredient ||
				    _backPackSystem.BackPackItemInfos[itemKey].ItemType == BackPackItemType.Seasoning)
				{
					var harvestFoodItemTemplate = HarvestFoodItemTemplate.InstantiateWithParent(HarvestFoodRoot)
						.Self(self =>
						{
							self.Icon.sprite = _backPackSystem.BackPackItemInfos[itemKey].ItemIcon;
							self.Name.text = this.SendQuery(new FindBackPackItemName(itemKey));
							self.Amount.text = itemCount.ToString();
							self.Show();
						});
					_harvestItems.Add(harvestFoodItemTemplate.gameObject);
				}
			}
		}

		protected override void OnBeforeDestroy()
		{
			foreach (var harvestItem in _harvestItems)
			{
				harvestItem.DestroySelf();
			}
			_harvestItems.Clear();
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}