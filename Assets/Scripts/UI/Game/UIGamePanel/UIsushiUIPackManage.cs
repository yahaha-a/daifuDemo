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
	public partial class UIsushiUIPackManage : UIElement, IController
	{
		private IBackPackSystem _backPackSystem;

		private Dictionary<IBackPackItemInfo, int> _fishList = new Dictionary<IBackPackItemInfo, int>();
		
		private Dictionary<IBackPackItemInfo, int> _ingredientList = new Dictionary<IBackPackItemInfo, int>();

		private Dictionary<IBackPackItemInfo, int> _seasoningList = new Dictionary<IBackPackItemInfo, int>();

		private Dictionary<IBackPackItemInfo, int> _toolList = new Dictionary<IBackPackItemInfo, int>();

		private List<GameObject> _backPackItemTemplateList = new List<GameObject>();

		private void Awake()
		{
			_backPackSystem = this.GetSystem<IBackPackSystem>();
			
			FishButton.onClick.AddListener(() =>
			{
				foreach (var item in _backPackItemTemplateList)
				{
					item.DestroySelf();
				}
				_backPackItemTemplateList.Clear();

				foreach (var (backPackItemInfo, count) in _fishList)
				{
					BackPackItemTemplate.InstantiateWithParent(BackPackItemListRoot).Self(
						self =>
						{
							self.Name.text = backPackItemInfo.ItemName;
							self.Count.text = count.ToString();
							self.description = backPackItemInfo.ItemDescription;
							self.Show();
							_backPackItemTemplateList.Add(self.gameObject);
						});
				}
			});
			
			IngredientButton.onClick.AddListener(() =>
			{
				foreach (var item in _backPackItemTemplateList)
				{
					item.DestroySelf();
				}
				_backPackItemTemplateList.Clear();

				foreach (var (backPackItemInfo, count) in _ingredientList)
				{
					BackPackItemTemplate.InstantiateWithParent(BackPackItemListRoot).Self(self =>
					{
						self.Name.text = backPackItemInfo.ItemName;
						self.Count.text = count.ToString();
						self.description = backPackItemInfo.ItemDescription;
						self.Show();
						_backPackItemTemplateList.Add(self.gameObject);
					});
				}
			});
			
			SeasoningButton.onClick.AddListener(() =>
			{
				foreach (var item in _backPackItemTemplateList)
				{
					item.DestroySelf();
				}
				_backPackItemTemplateList.Clear();

				foreach (var (backPackItemInfo, count) in _seasoningList)
				{
					BackPackItemTemplate.InstantiateWithParent(BackPackItemListRoot).Self(self =>
					{
						self.Name.text = backPackItemInfo.ItemName;
						self.Count.text = count.ToString();
						self.description = backPackItemInfo.ItemDescription;
						self.Show();
						_backPackItemTemplateList.Add(self.gameObject);
					});
				}
			});
			
			ToolButton.onClick.AddListener(() =>
			{
				foreach (var item in _backPackItemTemplateList)
				{
					item.DestroySelf();
				}
				_backPackItemTemplateList.Clear();

				foreach (var (backPackItemInfo, count) in _toolList)
				{
					BackPackItemTemplate.InstantiateWithParent(BackPackItemListRoot).Self(self =>
					{
						self.Name.text = backPackItemInfo.ItemName;
						self.Count.text = count.ToString();
						self.description = backPackItemInfo.ItemDescription;
						self.Show();
						_backPackItemTemplateList.Add(self.gameObject);
					});
				}
			});
		}

		private void OnEnable()
		{
			foreach (var (key, count) in _backPackSystem.BackPackItemList)
			{
				if (_backPackSystem.BackPackItemInfos[key].ItemType == BackPackItemType.Fish)
				{
					_fishList.Add(_backPackSystem.BackPackItemInfos[key], count);
				}
				else if (_backPackSystem.BackPackItemInfos[key].ItemType == BackPackItemType.Ingredient)
				{
					_ingredientList.Add(_backPackSystem.BackPackItemInfos[key], count);
				}
				else if (_backPackSystem.BackPackItemInfos[key].ItemType == BackPackItemType.Seasoning)
				{
					_seasoningList.Add(_backPackSystem.BackPackItemInfos[key], count);
				}
				else if (_backPackSystem.BackPackItemInfos[key].ItemType == BackPackItemType.Tool)
				{
					_toolList.Add(_backPackSystem.BackPackItemInfos[key], count);
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