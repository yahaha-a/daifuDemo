/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class StaffManagePanel : UIElement, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;
		
		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_uiGamesushiPanelModel.IfStaffRestaurantManagePanelShow.RegisterWithInitValue(value =>
			{
				if (value)
				{
					StaffRestaurantManagePanel.Show();
				}
				else
				{
					StaffRestaurantManagePanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGamesushiPanelModel.IfStaffWaitingRoomManagePanelShow.RegisterWithInitValue(value =>
			{
				if (value)
				{
					StaffWaitingRoomManagePanel.Show();
				}
				else
				{
					StaffWaitingRoomManagePanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			RestaurantButton.onClick.AddListener(() =>
			{
				StaffRestaurantManagePanel.Show();
				StaffWaitingRoomManagePanel.Hide();
			});
			
			WaitingRoomButton.onClick.AddListener(() =>
			{
				StaffRestaurantManagePanel.Hide();
				StaffWaitingRoomManagePanel.Show();
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