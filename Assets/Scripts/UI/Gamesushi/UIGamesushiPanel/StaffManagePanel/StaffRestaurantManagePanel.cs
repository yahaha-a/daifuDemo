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
	public partial class StaffRestaurantManagePanel : UIElement, IController
	{
		private IStaffSystem _staffSystem;

		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private List<RestaurantStaffTemplate> _restaurantStaffs = new List<RestaurantStaffTemplate>();

		private void Awake()
		{
			_staffSystem = this.GetSystem<IStaffSystem>();
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
		}

		private void OnEnable()
		{
			_uiGamesushiPanelModel.IfAppointStaffToKitchen.Value = false;
			_uiGamesushiPanelModel.IfAppointStaffToLobby.Value = false;

			foreach (var (node, staffKey) in _staffSystem.CurrentCookers)
			{
				RestaurantStaffTemplate.InstantiateWithParent(KitchenStaffRoot).Self(self =>
				{
					self.TemplateType = RestaurantStaffTemplateType.Kitchen;
					self.Node = node;
					self.StaffKey = staffKey;
					self.Show();
					_restaurantStaffs.Add(self);
				});
			}

			foreach (var (node, staffKey) in _staffSystem.CurrentWaiters)
			{
				RestaurantStaffTemplate.InstantiateWithParent(LobbyStaffRoot).Self(self =>
				{
					self.TemplateType = RestaurantStaffTemplateType.Lobby;
					self.Node = node;
					self.StaffKey = staffKey;
					self.Show();
					_restaurantStaffs.Add(self);
				});
			}
		}

		private void OnDisable()
		{
			foreach (var restaurantStaffTemplate in _restaurantStaffs)
			{
				restaurantStaffTemplate.gameObject.DestroySelf();
			}
			_restaurantStaffs.Clear();
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