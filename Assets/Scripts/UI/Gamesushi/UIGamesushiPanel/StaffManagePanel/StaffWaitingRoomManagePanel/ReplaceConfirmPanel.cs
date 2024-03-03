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
	public partial class ReplaceConfirmPanel : UIElement, IController
	{
		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private IStaffSystem _staffSystem;

		private void Awake()
		{
			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
			_staffSystem = this.GetSystem<IStaffSystem>();
			
			ConfirmButton.onClick.AddListener(() =>
			{
				if (_uiGamesushiPanelModel.IfAppointStaffToKitchen.Value)
				{
					_staffSystem.AddCooker(_uiGamesushiPanelModel.CurrentCookListNode.Value,
						_uiGamesushiPanelModel.SelectStaffKey.Value);
					
					this.SendCommand<StaffManageButtonTriggerCommand>();
				}

				if (_uiGamesushiPanelModel.IfAppointStaffToLobby.Value)
				{
					_staffSystem.AddWaiter(_uiGamesushiPanelModel.CurrentWaiterListNode.Value,
						_uiGamesushiPanelModel.SelectStaffKey.Value);
					
					this.SendCommand<StaffManageButtonTriggerCommand>();
				}
				_uiGamesushiPanelModel.IfReplaceConfirmPanelShow.Value = false;
			});
			
			CancelButton.onClick.AddListener(() =>
			{
				_uiGamesushiPanelModel.IfReplaceConfirmPanelShow.Value = false;
			});
		}

		private void OnEnable()
		{
			var name = _staffSystem.StaffItemInfos[_uiGamesushiPanelModel.SelectStaffKey.Value].Name;
			string destinationName = "";
			if (_uiGamesushiPanelModel.IfAppointStaffToKitchen.Value)
			{
				destinationName = "厨房";
			}

			if (_uiGamesushiPanelModel.IfAppointStaffToLobby.Value)
			{
				destinationName = "大堂";
			}

			Content.text = "分配" + name + "到" + destinationName;
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