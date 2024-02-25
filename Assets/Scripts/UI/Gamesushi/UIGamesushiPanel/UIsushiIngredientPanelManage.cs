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
	public partial class UIsushiIngredientPanelManage : UIElement, IController
	{
		private IBackPackSystem _backPackSystem;

		private List<sushiBackPackItemTemplate> _sushiBackPackItemTemplates = new List<sushiBackPackItemTemplate>();

		private List<(Button, BackPackItemType)> _buttonList = new List<(Button, BackPackItemType)>();
		
		private void Awake()
		{
			_backPackSystem = this.GetSystem<IBackPackSystem>();
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
								});
								self.Show();
								_sushiBackPackItemTemplates.Add(self);
							});
						}
					}
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