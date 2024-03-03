/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class WaitingStaffTemplate : UIElement, IController
	{
		public IstaffItemInfo CurrentStaffItemInfo { get; set; }
		
		private void Awake()
		{
			var staffSystem = this.GetSystem<IStaffSystem>();

			var uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
			
			Name.text = CurrentStaffItemInfo.Name;
			Rank.text = "Lv." + CurrentStaffItemInfo.Rank;
			WalkSpeed.text = "移动速度: " + staffSystem.StaffItemInfos[CurrentStaffItemInfo.Key].RankWithWalkSpeed
				.FirstOrDefault(item => item.Item1 == CurrentStaffItemInfo.Rank).Item2;
			CookSpeed.text = "料理速度: " + staffSystem.StaffItemInfos[CurrentStaffItemInfo.Key].RankWithCookSpeed
				.FirstOrDefault(item => item.Item1 == CurrentStaffItemInfo.Rank).Item2;
			
			this.GetComponent<Button>().onClick.AddListener(() =>
			{
				uiGamesushiPanelModel.SelectStaffKey.Value = CurrentStaffItemInfo.Key;
				
				if (uiGamesushiPanelModel.IfAppointStaffToKitchen.Value ||
				    uiGamesushiPanelModel.IfAppointStaffToLobby.Value)
				{
					uiGamesushiPanelModel.IfReplaceConfirmPanelShow.Value = true;
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