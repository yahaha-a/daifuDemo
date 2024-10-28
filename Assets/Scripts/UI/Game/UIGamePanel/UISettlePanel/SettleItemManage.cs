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
	public partial class SettleItemManage : UIElement, IController
	{
		private static ResLoader _resLoader = ResLoader.Allocate();
		
		public List<SettleItemTemplate> SettleItemTemplateList { get; } = new List<SettleItemTemplate>();

		private IHarvestSystem _harvestSystem;

		private IBackPackSystem _backPackSystem;
		
		private IUtils _utils;
		
		private void Awake()
		{
			_utils = this.GetUtility<IUtils>();
			_harvestSystem = this.GetSystem<IHarvestSystem>();
			_backPackSystem = this.GetSystem<IBackPackSystem>();
		}

		private void OnEnable()
		{
			foreach (var (itemKey, itemCount) in _harvestSystem.HarvestItems)
			{
				if (_backPackSystem.BackPackItemInfos[itemKey].ItemType == BackPackItemType.Tool)
				{
					var settleItemTemplate = SettleItemTemplate.InstantiateWithParent(this)
						.Self(self =>
						{
							self.Icon.sprite = _utils.AdjustSprite(_resLoader.LoadSync<Texture2D>(itemKey));
							self.Name.text = this.SendQuery(new FindBackPackItemName(itemKey));
							self.Number.text = itemCount.ToString();
							self.Show();
						});
					SettleItemTemplateList.Add(settleItemTemplate);
				}
			}
		}

		protected override void OnBeforeDestroy()
		{
			foreach (var settleItemTemplate in SettleItemTemplateList)
			{
				settleItemTemplate.gameObject.DestroySelf();
			}
			
			SettleItemTemplate.Clear();
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}