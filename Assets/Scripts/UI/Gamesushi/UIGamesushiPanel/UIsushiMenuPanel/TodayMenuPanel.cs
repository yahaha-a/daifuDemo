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
	public partial class TodayMenuPanel : UIElement, IController
	{
		private IMenuSystem _menuSystem;

		private List<GameObject> _todayMenuItems = new List<GameObject>();
		
		private void Awake()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();
		}

		private void Start()
		{
			foreach (var todayMenuItem in _menuSystem.TodayMenuItems)
			{
				TodayMenuTemplate.InstantiateWithParent(TodayMeunListRoot).Self(self =>
				{
					self.ItemInfo = todayMenuItem;
						
					self.Show();
					_todayMenuItems.Add(self.gameObject);
				});
			}

			this.GetUtility<IUtils>().AdjustContentHeight(TodayMeunListRoot);
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