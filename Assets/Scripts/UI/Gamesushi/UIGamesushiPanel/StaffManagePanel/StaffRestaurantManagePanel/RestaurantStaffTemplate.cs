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
	public enum RestaurantStaffTemplateType
	{
		Kitchen,
		Lobby
	}
	
	public partial class RestaurantStaffTemplate : UIElement, IController
	{
		public RestaurantStaffTemplateType TemplateType { get; set; } 
		
		public int Node { get; set; }
		
		public string StaffKey { get; set; }
		
		private void Awake()
		{
			var staffSystem = this.GetSystem<IStaffSystem>();

			var uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
			
			if (TemplateType == RestaurantStaffTemplateType.Kitchen && StaffKey == null)
			{
				AddCookerButton.Show();
				AddWaiterButton.Hide();
				StaffDetail.Hide();
			}
			else if (TemplateType == RestaurantStaffTemplateType.Lobby && StaffKey == null)
			{
				AddCookerButton.Hide();
				AddWaiterButton.Show();
				StaffDetail.Hide();
			}
			else if (StaffKey != null)
			{
				AddCookerButton.Hide();
				AddWaiterButton.Hide();
				StaffDetail.Show();
				Rank.text = "Lv." + staffSystem.CurrentOwnStaffItems[StaffKey].Rank.ToString();
				Name.text = staffSystem.StaffItemInfos[StaffKey].Name;
				WalkSpeed.text = "移动速度: " + staffSystem.StaffItemInfos[StaffKey].RankWithWalkSpeed
					.FirstOrDefault(item => item.Item1 == staffSystem.CurrentOwnStaffItems[StaffKey].Rank).Item2
					.ToString();

				CookSpeed.text = "料理速度: " + staffSystem.StaffItemInfos[StaffKey].RankWithCookSpeed
					.FirstOrDefault(item => item.Item1 == staffSystem.CurrentOwnStaffItems[StaffKey].Rank).Item2
					.ToString();
			}
			
			AddCookerButton.onClick.AddListener(() =>
			{
				uiGamesushiPanelModel.CurrentCookListNode.Value = Node;
				uiGamesushiPanelModel.IfAppointStaffToKitchen.Value = true;
				this.SendCommand<StaffManageButtonTriggerCommand>();
			});
			
			AddWaiterButton.onClick.AddListener(() =>
			{
				uiGamesushiPanelModel.CurrentWaiterListNode.Value = Node;
				uiGamesushiPanelModel.IfAppointStaffToLobby.Value = true;
				this.SendCommand<StaffManageButtonTriggerCommand>();
			});
		}

		private void OnEnable()
		{
			
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