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
				foreach (var (itemName, itemCount) in _harvestSystem.HarvestItems)
				{
					var settleItemTemplate = SettleItemTemplate.InstantiateWithParent(this)
						.Self(self =>
						{
							self.Name.text = itemName;
							self.Number.text = itemCount.ToString();
							self.Show();
						});
					SettleItemTemplateList.Add(settleItemTemplate);
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