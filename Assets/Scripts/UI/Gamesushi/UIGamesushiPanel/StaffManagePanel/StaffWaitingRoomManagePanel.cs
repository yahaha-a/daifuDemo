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
	public partial class StaffWaitingRoomManagePanel : UIElement, IController
	{
		private IStaffSystem _staffSystem;

		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private List<WaitingStaffTemplate> _waitingStaffs = new List<WaitingStaffTemplate>();
		
		private void Awake()
		{
			_staffSystem = this.GetSystem<IStaffSystem>();

			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_uiGamesushiPanelModel.IfReplaceConfirmPanelShow.Register(value =>
			{
				if (value)
				{
					ReplaceConfirmPanel.Show();
				}
				else
				{
					ReplaceConfirmPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{
			foreach (var (staffKey, staffItemInfo) in _staffSystem.CurrentOwnStaffItems)
			{
				if (staffItemInfo.State == StaffState.Free)
				{
					WaitingStaffTemplate.InstantiateWithParent(WaitingStaffRoot).Self(self =>
					{
						self.CurrentStaffItemInfo = staffItemInfo;
						self.Show();
						_waitingStaffs.Add(self);
					});
				}
			}

			this.GetUtility<IUtils>().AdjustContentHeight(WaitingStaffRoot);
		}

		private void OnDisable()
		{
			foreach (var waitingStaffTemplate in _waitingStaffs)
			{
				waitingStaffTemplate.gameObject.DestroySelf();
			}
			_waitingStaffs.Clear();
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