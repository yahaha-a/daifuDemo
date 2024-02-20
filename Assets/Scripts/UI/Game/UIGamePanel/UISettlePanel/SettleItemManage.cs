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
		public List<SettleItemTemplate> SettleItemTemplateList { get; } = new List<SettleItemTemplate>();

		private IHarvestSystem _harvestSystem;
		
		private void Awake()
		{
			_harvestSystem = this.GetSystem<IHarvestSystem>();

			Events.UISettlePanelShow.Register(() =>
			{
				foreach (var (itemKey, itemCount) in _harvestSystem.HarvestItems)
				{
					var settleItemTemplate = SettleItemTemplate.InstantiateWithParent(this)
						.Self(self =>
						{
							self.Name.text = this.SendQuery(new FindBackPackItemName(itemKey));
							self.Number.text = itemCount.ToString();
							self.Show();
						});
					SettleItemTemplateList.Add(settleItemTemplate);
				}

				var blankGridQuantity = ((SettleItemTemplateList.Count + 10) / 10) * 10 - SettleItemTemplateList.Count;
				
				for (int i = 0; i < blankGridQuantity; i++)
				{
					SettleItemTemplateList.Add(SettleItemTemplate.InstantiateWithParent(this)
						.Self(self => self.Show()));
				}
			});
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