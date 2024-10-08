/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIPackPanel : UIElement, IController
	{
		private IFishSystem _fishSystem;

		private List<GameObject> _backPackItems = new List<GameObject>();
		
		private void Awake()
		{
			_fishSystem = this.GetSystem<IFishSystem>();
		}

		private void OnEnable()
		{
			foreach (var (key, caughtFishInfo) in _fishSystem.CaughtItem)
			{
				UIPackItemTemplate.InstantiateWithParent(UIPackItemListRoot)
					.Self(self =>
					{
						self.key = caughtFishInfo.FishKey;
						self.ItemIcon.sprite = caughtFishInfo.FishIcon;
						self.ItemName.text = caughtFishInfo.FishName;
						if (caughtFishInfo.Star == 0)
						{
							self.ItemStar.text = null;
						}
						else
						{
							self.ItemStar.text = caughtFishInfo.Star.ToString() + " 星";
						}
						self.ItemAmount.text = "× " + caughtFishInfo.Amount.ToString();
						
						_backPackItems.Add(self.gameObject);
						self.Show();
					});
			}
					
			this.GetUtility<IUtils>().AdjustContentHeight(UIPackItemListRoot);
		}

		private void OnDisable()
		{
			foreach (GameObject backPackItem in _backPackItems)
			{
				backPackItem.DestroySelf();
			}
			_backPackItems.Clear();
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